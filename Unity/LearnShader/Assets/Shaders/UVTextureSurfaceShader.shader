Shader "Custom/UVTextureSurfaceShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2 ("Albedo (RGB)", 2D) = "white" {}
        _Speed("Speed", Float) = 0.0
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

        float _Speed;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //IN.uv_MainTex.x += _SinTime.w;
            fixed4 c = tex2D (_MainTex, float2(IN.uv_MainTex.x + _SinTime.w * _Speed, IN.uv_MainTex.y));
            fixed4 d = tex2D (_MainTex2, IN.uv_MainTex2);

            //o.Albedo = lerp(c.rgb, d.rgb, IN.uv_MainTex.x);
            o.Albedo = c.rgb;
            //o.Emission = IN.uv_MainTex.x;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
