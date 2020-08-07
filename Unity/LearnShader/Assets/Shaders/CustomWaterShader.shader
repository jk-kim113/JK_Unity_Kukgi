Shader "Custom/CustomWaterShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("NormalMap", 2D) = "bump" {}

        _DistortionMap("DistortionMap", 2D) = "white"{}

        _SPColor("SP Color", color) = (1, 1, 1, 1)

        _Power("Power", Range(10, 200)) = 100
        _Gloss("Gloss Value", Range(0, 1)) = 0.5

        _WaveHeight("Wave_Height", Range(0.05, 0.5)) = 0.12
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        GrabPass{}

        CGPROGRAM
        #pragma surface surf Test noambient vertex:vert noshadow
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _DistortionMap;
        sampler2D _GrabTexture;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_DistortionMap;

            float3 viewDir;
            float4 screenPos; // 화면 좌표를 받아 올 때 사용

            float3 worldRefl;
            INTERNAL_DATA
        };

        fixed4 _SPColor;

        float _Power;
        float _Gloss;

        float _WaveHeight;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutput o)
        {
            float3 normal1 = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap + _Time.x * 0.1));
            float3 normal2 = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap - _Time.x * 0.1));
            o.Normal = (normal1 + normal2) / 2;

            float3 refcolor = tex2D(_MainTex, IN.uv_MainTex);

            float3 screenUV = IN.screenPos.rgb / IN.screenPos.a;
            float3 refraction = tex2D(_GrabTexture, (screenUV.xy + o.Normal.xy * 0.1));

            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1 - rim, 1.5);

            o.Emission = (refcolor * rim + refraction) * 0.5;
            o.Alpha = 1;

        }
        float4 LightingTest(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
        {
            float3 H = normalize(lightDir + viewDir);
            float spec = saturate(dot(H, s.Normal));
            spec = pow(spec, _Power);

            float4 finalColor;
            finalColor.rgb = spec * _SPColor.rgb * _Gloss;
            finalColor.a = s.Alpha;

            return finalColor;
        }

        //void surf (Input IN, inout SurfaceOutput o)
        //{
        //    fixed4 c = tex2D (_MainTex, float2(IN.uv_MainTex.x + _Time.x * 2.0f, IN.uv_MainTex.y + _Time.x));
        //    
        //    fixed3 nL = UnpackNormal(tex2D(_BumpMap, float2(IN.uv_BumpMap.x + _Time.x, IN.uv_BumpMap.y)));
        //    fixed3 nR = UnpackNormal(tex2D(_BumpMap, float2(IN.uv_BumpMap.x - _Time.x, IN.uv_BumpMap.y)));
        //    fixed3 nT = UnpackNormal(tex2D(_BumpMap, float2(IN.uv_BumpMap.x, IN.uv_BumpMap.y + _Time.x)));
        //    fixed3 nB = UnpackNormal(tex2D(_BumpMap, float2(IN.uv_BumpMap.x, IN.uv_BumpMap.y - _Time.x)));

        //    o.Normal = (nL + nR + nT + nB) / 4;

        //    /*float3 fWorldReflectionVector = WorldReflectionVector(IN, o.Normal).xyz;
        //    float3 fReflection = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, fWorldReflectionVector).rgb * unity_SpecCube0_HDR.r;

        //    float4 fDistortion = tex2D(_DistortionMap, IN.uv_DistortionMap + _Time.y * 0.05f);

        //    float3 scrPos = IN.screenPos.xyz / (IN.screenPos.w + 0.00001f);
        //    float4 fGrab = tex2D(_GrabTexture, scrPos.xy + (fDistortion.r * 0.2f));

        //    float fNDotV = dot(o.Normal, IN.viewDir);
        //    float fRim = saturate(pow(1 - fNDotV + 0.1f, 1));*/

        //    o.Albedo = c.rgb;
        //    
        //    //o.Emission = lerp(fGrab.rgb, fReflection, fRim);
        //    o.Gloss = _Gloss;
        //    o.Alpha = c.a;
        //}
        //float4 LightingTest(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
        //{
        //    //BlinnPhong
        //    float3 DiffColor;
        //    float ndotl = saturate(dot(s.Normal, lightDir));
        //    DiffColor = ndotl * s.Albedo * _LightColor0.rgb * atten;

        //    float3 h = normalize(lightDir + viewDir);
        //    float spec = saturate(dot(h, s.Normal));
        //    spec = pow(spec, _Power);

        //    float4 final;
        //    final.rgb = DiffColor.rgb + spec + s.Gloss;
        //    final.a = s.Alpha;

        //    return final;
        //}

        void vert(inout appdata_full v)
        {
            v.vertex.y += sin((abs(v.texcoord.x * 2.0f - 1.0f) * 10.0f) + _Time.y * 0.8f) * _WaveHeight
                            + sin((abs(v.texcoord.y * 2.0f - 1.0f) * 10.0f) + _Time.y * 0.8f) * _WaveHeight;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
