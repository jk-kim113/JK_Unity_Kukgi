Shader "Custom/HoloSurfaceShader"
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
        #pragma surface surf Lambert noambient alpha:fade

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

            o.Emission = _RimColor.rgb;
            o.Alpha = rim;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
