Shader "Custom/RimLambertSurfaceShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpTex("Normal Map", 2D) = "bump" {}

        _RimColor("Rim Color", color) = (1, 1, 1, 1)
        _RimPower("Rim Power", Range(1, 10)) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        sampler2D _BumpTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpTex;
            float3 viewDir; // 카메라가 바라보는 것
        };

        fixed4 _RimColor;
        float _RimPower;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed3 n = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
            float rim = dot(o.Normal, IN.viewDir);

            o.Albedo = c.rgb;
            //o.Albedo = pow(1 - rim, _RimPower) * _RimColor.rgb;
            o.Normal = n;
            o.Emission = pow(1 - rim, _RimPower) * _RimColor.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
