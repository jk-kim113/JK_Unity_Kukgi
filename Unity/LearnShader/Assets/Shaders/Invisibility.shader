Shader "Custom/Invisibility"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Opacity("Opacity", Range(0, 1)) = 0.1
        _DeformIntensity("Deform Normal Intensity", Range(0, 3)) = 1
        _RimPower("Rim Power", Range(1, 10)) = 3 
        _RimColor("Rim Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        zwrite off

        GrabPass{}

        CGPROGRAM
        #pragma surface surf Invisibility noambient novertexlights noforwarded

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _GrabTexture;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float3 viewDir;
            float4 screenPos;
        };

        float _Opacity;
        float _DeformIntensity;
        float _RimPower;
        float3 _RimColor;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            float4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));

            float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
            float3 mapping = tex2D(_GrabTexture, screenUV);

            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1 - rim, _RimPower);

            o.Emission = mapping * (1 - _Opacity) + _RimColor * rim;
            o.Albedo = c.rgb;
        }
        float4 LightingInvisibility(SurfaceOutput s, float lightDir, float atten)
        {
            return fixed4(s.Albedo * _LightColor0 * _Opacity, 1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
