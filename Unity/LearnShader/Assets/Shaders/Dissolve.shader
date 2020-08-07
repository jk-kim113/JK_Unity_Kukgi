Shader "Custom/Dissolve"
{
    Properties
    {   
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("Normal Map", 2D) = "bump" {}
        _NoiseTex("Noise Tex", 2D) = "white"{}
        _Cut("Alpha Cut", Range(0, 1)) = 0
        [HDR]_OutColor("OutColor", Color) = (1, 1, 1, 1)
        _OutThickness("OutThickness", Range(1, 1.15)) = 1.15
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha:fade

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _NoiseTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_NoiseTex;
        };

        float _Cut;
        float4 _OutColor;
        float _OutThickness;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            fixed4 noise = tex2D(_NoiseTex, IN.uv_NoiseTex);

            o.Albedo = c.rgb;
            float alpha;
            if (noise.r >= _Cut)
                alpha = 1;
            else
                alpha = 0;

            float outline;
            if (noise.r >= _Cut * _OutThickness)
                outline = 0;
            else
                outline = 1;

            o.Emission = outline * _OutColor.rgb;
            o.Alpha = alpha;
        }
        ENDCG
    }
    FallBack "Transparent"
}
