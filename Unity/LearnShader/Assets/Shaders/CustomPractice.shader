Shader "Custom/CustomPractice"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("NormalMap", 2D) = "bump" {}

        _SubTex1("VertexRed (RGB)", 2D) = "white" {}
        _SubTex2("VertexGreen (RGB)", 2D) = "white" {}
        _SubTex3("VertexBlue (RGB)", 2D) = "white" {}

        _Sub3Color("Sub3_Color", color) = (1, 1, 1, 1)

        _Color("Color", Color) = (1,1,1,1)
        _Power("Power", Range(10, 200)) = 100

        _Gloss("Gloss Value", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Test
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;

        sampler2D _SubTex1;
        sampler2D _SubTex2;
        sampler2D _SubTex3;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float4 color:COLOR;
            float2 uv_SubTex1;
            float2 uv_SubTex2;
            float2 uv_SubTex3;
        };

        fixed4 _Sub3Color;
        fixed4 _Color;
        float _Power;
        float _Gloss;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 s1 = tex2D(_SubTex1, IN.uv_SubTex1);
            fixed4 s2 = tex2D(_SubTex2, float2(IN.uv_SubTex2.x + _Time.x, IN.uv_SubTex2.y - _Time.x));
            fixed4 s3 = tex2D(_SubTex3, IN.uv_SubTex3) * _Sub3Color;
            fixed3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

            /*o.Albedo = lerp(c.rgb, s1.rgb, IN.color.r);
            o.Albedo = lerp(o.Albedo, s2.rgb, IN.color.g);
            o.Albedo = lerp(o.Albedo, s3.rgb, IN.color.b);*/
            
            o.Emission = lerp(c.rgb, s1.rgb, IN.color.r);
            o.Emission = lerp(o.Emission, s2.rgb, IN.color.g);
            o.Emission = lerp(o.Emission, s3.rgb, IN.color.b);

            o.Normal = lerp(n, s1.rgb, IN.color.r);
            o.Normal = lerp(o.Normal, s2.rgb, IN.color.g);
            o.Normal = lerp(o.Normal, s3.rgb, IN.color.b);

            o.Gloss = _Gloss * IN.color.r;
            o.Alpha = c.a;
        }
        float4 LightingTest(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
        {
            //BlinnPhong
            float3 DiffColor;
            float ndotl = saturate(dot(s.Normal, lightDir));
            DiffColor = ndotl * s.Albedo * _LightColor0.rgb * atten;

            float3 h = normalize(lightDir + viewDir);
            float spec = saturate(dot(h, s.Normal));
            spec = pow(spec, _Power);

            float4 final;
            final.rgb = DiffColor.rgb + (spec * _Color.rgb) + s.Gloss;
            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
