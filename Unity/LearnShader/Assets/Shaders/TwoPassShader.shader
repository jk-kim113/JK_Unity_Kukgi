Shader "Custom/TwoPassShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpTex ("Normal Map", 2D) = "bump" {}

        _Depth("Depth", Range(0.001, 0.05)) = 0.01
        _OutLineColor("OutLine Color", color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        cull front
        LOD 200

        CGPROGRAM
        #pragma surface surf Nolight noambient vertex:vert noshadow

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float4 color:COLOR;
        };

        float _Depth;
        fixed4 _OutLineColor;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            /*fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;*/
        }

        float4 LightingNolight(SurfaceOutput s, float3 lightDir, float atten)
        {
            return _OutLineColor;
        }

        void vert(inout appdata_full v)
        {
            v.vertex.xyz = v.vertex.xyz + v.normal.xyz * _Depth;
        }
        ENDCG

        cull back

        CGPROGRAM
        #pragma surface surf Toon noambient

        sampler2D _MainTex;
        sampler2D _BumpTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpTex;
        };

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

        float4 LightingToon(SurfaceOutput s, float3 lightDir, float atten)
        {
            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
            ndotl = ndotl * 5;
            ndotl = ceil(ndotl) / 5;
            
            float3 DiffColor = ndotl * s.Albedo * _LightColor0.rgb * atten;
            float4 final;
            final.rgb = DiffColor.rgb;
            final.a = s.Alpha;
            /*if (ndotl > 0.7)
            {
                ndotl = 1;
            }
            else if (ndotl > 0.4)
            {
                ndotl = 0.3;
            }
            else
            {
                ndotl = 0;
            }*/
            return final;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
