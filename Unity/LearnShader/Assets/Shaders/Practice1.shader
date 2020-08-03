Shader "Custom/Practice1"
{
    Properties
    {   
        _Red("Red", Range(0, 1)) = 0.0
        _Green("Green", Range(0, 1)) = 0.0
        _Blue("Blue", Range(0, 1)) = 0.0

        _BrightDark("Brightness $ Darkeness", Range(-1, 1)) = 0.0

        _Emission("Emission", Range(0, 1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        //sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };
    
        float _Red;
        float _Green;
        float _Blue;
        float _BrightDark;
        float _Emission;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = float3(_Red, _Green, _Blue) + _BrightDark;
            o.Emission = float3(_Emission, _Emission, _Emission);
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
