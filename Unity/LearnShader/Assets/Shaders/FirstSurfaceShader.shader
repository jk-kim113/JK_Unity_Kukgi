Shader "Custom/FirstSurfaceShader"
{
    Properties
    {
        // 변수명("display name", 자료형) = 값
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        //_Glossiness ("Smoothness", Range(0,1)) = 0.5
        //_Metallic ("Metallic", Range(0,1)) = 0.0
        _EmissionRed("EmissonRed", Range(0,1)) = 0.0
        _EmissionGreen("EmissonGreen", Range(0,1)) = 0.0
        _EmissionBlue("EmissonBlue", Range(0,1)) = 0.0

        _BrightDark("Brightness $ Darkeness", Range(-1, 1)) = 0.0
        /*_int("Int", int) = 1
        _float("Float", Float) = 0.9
        _Vector("Vector", Vector) = (1,1,1,1)
        _3D("3D", 3D) = "white" {}
        _Cube("Cube", Cube) = "white" {}
        _Rect("Rect", Rect) = "white" {}*/
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM // 실제 연산 부분
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        //sampler2D _MainTex; // 텍스처를 받아서 저장할 변수

        struct Input
        {
            float2 uv_MainTex;
        };

        //half _Glossiness;
        //half _Metallic;

        float _EmissionRed;
        float _EmissionGreen;
        float _EmissionBlue;
        
        fixed4 _Color; // 컬러는 음수가 없기 때문에 fixed4를 쓴다.

        float _BrightDark;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //// Albedo comes from a texture tinted by color
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //fixed4 test = float4(1, 1, 0, 1);
            //o.Albedo = test * _Color;
            //o.Albedo = c +float3(1, 0, 0); // c.rgb;
            //o.Emission = float3(1, 0, 0); // 음영이 사라짐
            //// Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            o.Albedo = _Color + _BrightDark;
            o.Emission = float3(_EmissionRed, _EmissionGreen, _EmissionBlue);
            o.Alpha = 1; //c.a;

            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
