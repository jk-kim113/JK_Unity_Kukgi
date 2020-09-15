Shader "Custom/01_IceShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpTex("Normal Map", 2D) = "bump" {}

        _Glossiness ("Glossiness", Range(0,1)) = 0.5

        _RimPower("RimPower", Range(0, 15)) = 0.5
        _Opacity("Opacity", Range(0, 1)) = 0.1

        _Color("Color", color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        zwrite off

        GrabPass{}

        CGPROGRAM
        #pragma surface surf Test noambient novertexlights
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpTex;
        sampler2D _GrabTexture;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpTex;
            float3 viewDir;
            float4 screenPos;
        };

        fixed4 _Color;

        half _Glossiness;

        float _RimPower;
        float _Opacity;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed3 n = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));

            o.Albedo = c.rgb;
            o.Normal = n;
            o.Gloss = _Glossiness;
            o.Alpha = c.a;

            float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
            float3 mapping = tex2D(_GrabTexture, screenUV);

            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1 - rim, _RimPower);

            o.Emission = mapping * (1 - _Opacity) + _Color * rim;
        }

        float4 LightingTest(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
        {
            float ndot = saturate(dot(s.Normal, lightDir));
            float4 final;
            final.rgb = ndot * s.Albedo * _LightColor0.rgb * atten;
            final.a = s.Alpha;
            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
