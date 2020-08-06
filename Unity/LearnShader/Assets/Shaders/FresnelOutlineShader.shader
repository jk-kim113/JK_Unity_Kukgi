Shader "Custom/FresnelOutlineShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpTex ("Normal Map", 2D) = "bump" {}

        _Depth("Depth", Range(0, 1)) = 0.1
        _OutLineColor("OutLine Color", color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Toon noambient

        sampler2D _MainTex;
        sampler2D _BumpTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpTex;
        };

        float _Depth;
        fixed4 _OutLineColor;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            fixed3 n = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));

            o.Albedo = c.rgb;
            o.Normal = n;
            o.Alpha = c.a;
        }

        float4 LightingToon(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
        {
            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
            ndotl = ndotl * 5;
            ndotl = ceil(ndotl) / 5;

            float4 final;

            float rim = abs(dot(s.Normal, viewDir));
            if (rim > _Depth)
                final.rgb = ndotl * s.Albedo * _LightColor0.rgb;
            else
                final.rgb = ndotl * s.Albedo * _LightColor0.rgb * _OutLineColor;
             
            final.a = s.Alpha;
            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
