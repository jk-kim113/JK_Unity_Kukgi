Shader "Custom/VertexColorShader"
{
    Properties
    {   
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("NormalMap", 2D) = "bump" {}
        _Occlusion("Occlusion", 2D) = "white" {}
        
        _SubTex1 ("VertexRed (RGB)", 2D) = "white" {}
        _SubTex2 ("VertexGreen (RGB)", 2D) = "white" {}
        _SubTex3 ("VertexBlue (RGB)", 2D) = "white" {}

        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _Occlusion;

        sampler2D _SubTex1;
        sampler2D _SubTex2;
        sampler2D _SubTex3;
        

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_Occlusion;
            float4 color:COLOR; // Vertex의 color값을 가져 오는 방법
            float2 uv_SubTex1;
            float2 uv_SubTex2;
            float2 uv_SubTex3;
        };

        half _Glossiness;
        half _Metallic;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            fixed4 s1 = tex2D (_SubTex1, IN.uv_SubTex1);
            fixed4 s2 = tex2D (_SubTex2, IN.uv_SubTex2);
            fixed4 s3 = tex2D (_SubTex3, IN.uv_SubTex3);

            fixed3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            fixed4 occ = tex2D(_Occlusion, IN.uv_Occlusion);

            o.Albedo = lerp(c.rgb, s1.rgb, IN.color.r);
            o.Albedo = lerp(o.Albedo, s2.rgb, IN.color.g);
            o.Albedo = lerp(o.Albedo, s3.rgb, IN.color.b);

            /*o.Emission = lerp(c.rgb, s1.rgb, IN.color.r);
            o.Emission = lerp(o.Emission, s2.rgb, IN.color.g);
            o.Emission = lerp(o.Emission, s3.rgb, IN.color.b);*/

            o.Normal = lerp(n, s1.rgb, IN.color.r);
            o.Normal = lerp(o.Normal, s2.rgb, IN.color.g);
            o.Normal = lerp(o.Normal, s3.rgb, IN.color.b);

            o.Occlusion = lerp(occ, s1.rgb, IN.color.r);
            o.Occlusion = lerp(o.Occlusion, s2.rgb, IN.color.g);
            o.Occlusion = lerp(o.Occlusion, s3.rgb, IN.color.b);

            //o.Metallic = _Metallic * IN.color.g;
            //o.Smoothness = _Glossiness * IN.color.g;

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
