// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:2,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33336,y:32954,varname:node_3138,prsc:2|diff-6064-OUT;n:type:ShaderForge.SFN_TexCoord,id:3676,x:31901,y:32436,varname:node_3676,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:241,x:32097,y:32436,varname:node_241,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-3676-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:8038,x:32294,y:32436,varname:node_8038,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-241-OUT;n:type:ShaderForge.SFN_Length,id:3688,x:32505,y:32436,varname:node_3688,prsc:2|IN-8038-OUT;n:type:ShaderForge.SFN_Abs,id:9619,x:32505,y:32571,varname:node_9619,prsc:2|IN-8038-OUT;n:type:ShaderForge.SFN_ArcTan2,id:9636,x:32505,y:32793,varname:node_9636,prsc:2,attp:2|A-8038-R,B-8038-G;n:type:ShaderForge.SFN_OneMinus,id:4009,x:32698,y:32436,varname:node_4009,prsc:2|IN-3688-OUT;n:type:ShaderForge.SFN_Desaturate,id:2008,x:32698,y:32571,varname:node_2008,prsc:2|COL-9619-OUT,DES-5020-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5020,x:32505,y:32726,ptovrint:False,ptlb:Value,ptin:_Value,varname:node_5020,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Set,id:9138,x:32872,y:32436,varname:Length,prsc:2|IN-4009-OUT;n:type:ShaderForge.SFN_Set,id:889,x:32872,y:32794,varname:ArcTan2,prsc:2|IN-9636-OUT;n:type:ShaderForge.SFN_Set,id:5573,x:32872,y:32571,varname:ABS,prsc:2|IN-2008-OUT;n:type:ShaderForge.SFN_Color,id:8949,x:31901,y:32655,ptovrint:False,ptlb:ColorA,ptin:_ColorA,varname:node_8949,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:977,x:31901,y:32829,ptovrint:False,ptlb:ColorB,ptin:_ColorB,varname:node_977,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.3,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:5357,x:32128,y:32786,varname:node_5357,prsc:2|A-8949-RGB,B-977-RGB,T-5009-OUT;n:type:ShaderForge.SFN_Lerp,id:5283,x:32128,y:32655,varname:node_5283,prsc:2|A-8949-RGB,B-977-RGB,T-8785-OUT;n:type:ShaderForge.SFN_Get,id:8785,x:31880,y:32979,varname:node_8785,prsc:2|IN-9138-OUT;n:type:ShaderForge.SFN_Get,id:5009,x:31880,y:33031,varname:node_5009,prsc:2|IN-5573-OUT;n:type:ShaderForge.SFN_Get,id:7469,x:31880,y:33082,varname:node_7469,prsc:2|IN-889-OUT;n:type:ShaderForge.SFN_Lerp,id:6064,x:32128,y:32929,varname:node_6064,prsc:2|A-8949-RGB,B-977-RGB,T-7469-OUT;proporder:8949-977-5020;pass:END;sub:END;*/

Shader "Shader Forge/Gradient" {
    Properties {
        _ColorA ("ColorA", Color) = (1,0,0,1)
        _ColorB ("ColorB", Color) = (0,0.3,1,1)
        _Value ("Value", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _ColorA;
            uniform float4 _ColorB;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float2 node_8038 = (i.uv0*2.0+-1.0).rg;
                float ArcTan2 = ((atan2(node_8038.r,node_8038.g)/6.28318530718)+0.5);
                float node_7469 = ArcTan2;
                float3 diffuseColor = lerp(_ColorA.rgb,_ColorB.rgb,node_7469);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _ColorA;
            uniform float4 _ColorB;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float2 node_8038 = (i.uv0*2.0+-1.0).rg;
                float ArcTan2 = ((atan2(node_8038.r,node_8038.g)/6.28318530718)+0.5);
                float node_7469 = ArcTan2;
                float3 diffuseColor = lerp(_ColorA.rgb,_ColorB.rgb,node_7469);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
