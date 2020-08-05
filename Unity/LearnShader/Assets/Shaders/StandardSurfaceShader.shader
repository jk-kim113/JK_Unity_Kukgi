Shader "Custom/StandardSurfaceShader"
{
    Properties
    {
        _Color ("Color", color) = (1, 1, 1, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpTex("NormalMap", 2D) = "bump" {}

        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

            //Dissolve properties
            _DissolveTexture("Dissolve Texutre", 2D) = "white" {}
            _Amount("Amount", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
                Cull Off //Fast way to turn your material double-sided

        CGPROGRAM
        #pragma surface surf Standard
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        
        //Dissolve properties
        sampler2D _DissolveTexture;
        half _Amount;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //Dissolve function
            half dissolve_value = tex2D(_DissolveTexture, IN.uv_MainTex).r;
            clip(dissolve_value - _Amount);

            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed3 n = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));

            o.Albedo = c.rgb;
            o.Normal = n;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
