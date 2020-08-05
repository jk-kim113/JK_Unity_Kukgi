Shader "Custom/HoloCustomSurfaceShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimPower("Rim Power", Range(1, 10)) = 3.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Test noambient alpha:fade

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
        };

        fixed4 _RimColor;
        float _RimPower;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1 - rim, _RimPower);

            //o.Albedo
            o.Emission = _RimColor.rgb;
            o.Alpha = rim;
        }

        float4 LightingTest(SurfaceOutput s, float3 lightDir, float atten)
        {
            float ndot = saturate(dot(s.Normal, lightDir));
            float4 final;
            final.rgb = ndot * s.Albedo * _LightColor0.rgb * atten;
            final.a = s.Alpha;
            return final; //ndot; // +0.5;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
