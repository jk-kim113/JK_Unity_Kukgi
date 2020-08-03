Shader "Custom/FireSurfaceShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _SubTex("Back", 2D) = "black" {}
        _DotTex("Cot", 2D) = "black" {}
        _SlingVal("SlingValue", Range(1, 5)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard alpha:fade

        sampler2D _MainTex;
        sampler2D _SubTex;
        sampler2D _DotTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_SubTex;
            float2 uv_DotTex;
        };

        float _SlingVal;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 e = tex2D(_DotTex, IN.uv_DotTex);//float2(IN.uv_DotTex.x, IN.uv_DotTex.y - _Time.x));
            fixed4 d = tex2D(_SubTex, float2(IN.uv_SubTex.x, IN.uv_SubTex.y - _Time.y) + e.r);
            
            //fixed4 d = tex2D(_SubTex, IN.uv_SubTex);
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex + (d.r * _SlingVal));
            
            o.Emission = c.rgb * d.rgb;
            o.Alpha = c.a * d.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
