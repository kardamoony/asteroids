Shader "Kardamoony/Toon"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
    	
        [Space]
		[HDR] _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        [HDR] _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        [HDR] _Glossiness("Glossiness", Float) = 32
		
    	[Space]
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
    }

    SubShader
    {
        Tags 
        {
            "Queue" = "Geometry"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            ZWrite On

            HLSLPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma exclude_renderers nomrt

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct Varyings
            {
                float4 pos : SV_POSITION;
				float3 worldNormal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
            };
  
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                half4 _Color;
			    half4 _AmbientColor;

			    half4 _SpecularColor;
			    half _Glossiness;		

			    half4 _RimColor;
			    half _RimAmount;
			    float _RimThreshold;
            CBUFFER_END
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                
                OUT.pos = TransformObjectToHClip(IN.vertex.xyz);
                OUT.worldNormal = TransformObjectToWorldNormal(IN.normal);		
				OUT.viewDir = GetWorldSpaceViewDir(IN.vertex.xyz);
				OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                
                return OUT;
            }
            
            half4 frag(Varyings IN) : SV_Target
            {
                float3 normal = normalize(IN.worldNormal);
				float3 viewDir = normalize(IN.viewDir);

				float NdotL = dot(_MainLightPosition.xyz, normal);
				float lightIntensity = smoothstep(0, 0.5, NdotL);	
				float4 light = lightIntensity * _MainLightColor;
            	
				float3 halfVector = normalize(_MainLightPosition.xyz + viewDir);
				float NdotH = dot(normal, halfVector);

				float specularIntensity = pow(abs(NdotH * lightIntensity), _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;				

				float rimDot = 1 - dot(viewDir, normal);

				float rimIntensity = rimDot * pow(abs(NdotL), _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.5, _RimAmount + 0.5, rimIntensity);
				float4 rim = rimIntensity * _RimColor;

				float4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);

				return (light + _AmbientColor + specular + rim) * _Color * tex;
            }

            ENDHLSL
        }
    }
}