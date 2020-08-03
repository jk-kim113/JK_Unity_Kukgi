Shader "Custom/TextureSurfaceShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainTex2("Albedo (RGB)", 2D) = "black" {}
        _MainTex3("Albedo (RGB)", 2D) = "white" {}
        _Combine("Combine", Range(0, 1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _MainTex2;
        sampler2D _MainTex3;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MainTex2;
            float2 uv_MainTex3;
        };

        float _Combine;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex); // 내부적으로 16비트
            fixed4 d = tex2D (_MainTex2, IN.uv_MainTex2);
            fixed4 e = tex2D (_MainTex3, IN.uv_MainTex3);
            
            //o.Albedo = (c.r + c.g + c.b) / 3;//c.rgb;
            //o.Albedo = float3((c.r + c.g + c.b) / 3, (c.r + c.g + c.b) / 3, (c.r + c.g + c.b) / 3);
            o.Albedo = lerp(c.rgb, d.rgb, e.a - _Combine);
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
