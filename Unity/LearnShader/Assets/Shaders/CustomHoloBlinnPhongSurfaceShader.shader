Shader "Custom/CustomHoloBlinnPhongSurfaceShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Color("Color", color) = (1, 1, 1, 1)
        _Power("Power", Range(10, 200)) = 100

        _GlossTex("GlossTex", 2D) = "gray"{}

        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimPower("Rim Power", Range(1, 10)) = 3.0
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent"  }
        LOD 200

        CGPROGRAM
        #pragma surface surf Test noambient alpha:fade

        sampler2D _MainTex;
        sampler2D _GlossTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_GlossTex;
            float3 viewDir;
        };

        fixed4 _Color;
        float _Power;

        fixed4 _RimColor;
        float _RimPower;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 g = tex2D(_GlossTex, IN.uv_GlossTex);
            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1 - rim, _RimPower);

            o.Albedo = c.rgb;
            o.Gloss = g.a;
            o.Emission = _RimColor.rgb;
            o.Alpha = rim;
        }
        //float4 LightingTest(SurfaceOutput s, float3 lightDir, float atten)
        //{
        //    //Lambert
        //    float3 DiffColor;
        //    float ndotl = saturate(dot(s.Normal, lightDir));
        //    DiffColor = ndotl * s.Albedo * _LightColor0.rgb * atten;
        //    float4 final;
        //    final.rgb = DiffColor.rgb;
        //    final.a = s.Alpha;
        //    return final;
        //}
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
            final.rgb = DiffColor.rgb + (spec * _Color.rgb * s.Gloss);
            final.a = s.Alpha;

            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
