Shader "Custom/CustomWaterInIceShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpTex ("Normal Map", 2D) = "bump" {}
        _SubTex ("Sub Tex", 2D) = "white" {}

        _Color("Color", color) = (1, 1, 1, 1)
        _Power("Power", Range(10, 200)) = 100
        _Glow("Glow",Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent"  }
        LOD 200

        CGPROGRAM
        #pragma surface surf Test alpha:fade
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpTex;
        sampler2D _SubTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpTex;
            float2 uv_SubTex;
            float4 color:COLOR;
            float3 viewDir;
        };

        fixed4 _Color;
        float _Power;
        float _Glow;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed3 n = UnpackNormal(tex2D (_BumpTex, IN.uv_BumpTex));
            fixed4 d = tex2D (_SubTex, IN.uv_SubTex + _Time.x);
            
            o.Albedo = c.rgb;
            o.Albedo = lerp(o.Albedo, d.rgb + c.rgb, IN.color.r);
            o.Albedo = lerp(o.Albedo, d.rgb + c.rgb, IN.color.g);
            o.Albedo = lerp(o.Albedo, d.rgb + c.rgb, IN.color.b);

            o.Normal = n;

            o.Gloss = _Glow;
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
            final.rgb = DiffColor.rgb + (spec * _Color * s.Gloss);
            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
