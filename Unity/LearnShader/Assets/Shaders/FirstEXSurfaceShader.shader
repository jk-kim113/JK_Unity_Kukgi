Shader "Custom/FirstEXSurfaceShader"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        
        _MainTex2("Albedo (RGB)", 2D) = "white" {}
        _Color2("Color", Color) = (1,1,1,1)
        
        _EmissionRed("Red", Range(0, 1)) = 0.5
        _EmissionGreen("Green", Range(0, 1)) = 0.5
        _EmissionBlue("Blue", Range(0, 1)) = 0.5

        _SpeedU("Speed_U", Range(0, 3)) = 1.5
        _SpeedV("Speed_V", Range(0, 3)) = 1.5

        _BrightDark("Brightness $ Darkeness", Range(-1, 1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _MainTex2;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
        };

        fixed4 _Color;
        fixed4 _Color2;

        float _EmissionRed;
        float _EmissionGreen;
        float _EmissionBlue;

        float _SpeedU;
        float _SpeedV;

        float _BrightDark;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, float2(IN.uv_MainTex.x + _Time.y * _SpeedU, IN.uv_MainTex.y + _Time.y * _SpeedV)) * _Color;
            fixed4 d = tex2D (_MainTex2, float2(IN.uv_MainTex2.x + _Time.y * _SpeedU, IN.uv_MainTex2.y + _Time.y * _SpeedV)) * _Color2;

            o.Albedo = (lerp(c.rgb, d.rgb, IN.uv_MainTex.x) * lerp(c.rgb, d.rgb, IN.uv_MainTex.y)) + _BrightDark;
            o.Emission = float3(_EmissionRed, _EmissionGreen, _EmissionBlue);
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
