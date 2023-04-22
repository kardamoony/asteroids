Shader "Kardamoony/Hull Outline" 
{
    Properties
    {
        _Thickness("Thickness", Float) = 1
        _Color("Color", Color) = (1, 1, 1, 1)
        
        [Toggle(USE_PRECALCULATED_OUTLINE_NORMALS)] _PrecalculateNormals("Use weighted normals", Float) = 0
        
        [Enum(UnityEngine.Rendering.CompareFunction)] _zTest ("ZTest", Int) = 0
        [Enum(Front,0,Back,1)] _Cull ("Culling Mode", Int) = 1
        
        [Space(10)]
        _Stencil("Stencil", Integer) = 16
        
        [Space(20)]
        [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Src Blend", float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("Dst Blend", float) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }
        Cull [_Cull]
        ZTest [_zTest]
        
        Stencil 
        {
            Ref [_Stencil]
            Comp NotEqual
            Pass Replace
        }

        Pass 
        {
            Blend [_SrcBlend] [_DstBlend]
            Name "HullOutline"

            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            #pragma shader_feature USE_PRECALCULATED_OUTLINE_NORMALS

            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float2 uv : TEXCOORD0;
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                #ifdef USE_PRECALCULATED_OUTLINE_NORMALS
                float3 smoothNormalOS : TEXCOORD3;
                #endif
            };

            struct VertexOutput
            {
                float2 uv : TEXCOORD0;
                float4 positionCS : SV_POSITION;
                float4 screenPos : TEXCOORD1;
                half distAlpha : COLOR0;
            };

            CBUFFER_START(UnityPerMaterial)
                float _Thickness;
                float4 _Color;
            CBUFFER_END
            
            float Remap(float value, float2 fromRange, float2 toRange)
            {
                return (toRange.y - toRange.x) * ((value - fromRange.x) / (fromRange.y - fromRange.x)) + toRange.x;
            }

            VertexOutput Vertex(Attributes input)
            {
                VertexOutput output = (VertexOutput)0;

                float3 normalOS;
                
                #ifdef USE_PRECALCULATED_OUTLINE_NORMALS
                normalOS = input.smoothNormalOS;
                #else
                normalOS = input.normalOS;
                #endif
                
                float3 posOS = input.positionOS.xyz + normalOS * _Thickness;
                output.positionCS = GetVertexPositionInputs(posOS).positionCS;
                output.screenPos = ComputeScreenPos(output.positionCS);
                output.uv = input.uv;
                
                return output;
            }

            half4 Fragment(VertexOutput input) : SV_Target
            {
                return _Color;
            }

            ENDHLSL
        }
    }
}