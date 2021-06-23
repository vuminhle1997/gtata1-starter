// Taken from the Unlit template shader 
Shader "Exercise3/PhongShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightPoint ("Light Point Postion", Vector) = (0, 0, 0, 0)
        _AmbientCoefficient ("Ambient Coefficient", Float) = 1.0
        _DiffuseCoefficient ("Diffuse Coefficient", Float) = 1.0
        _SpecularCoefficient ("Specular Coefficient", Float) = -0.02
        _Shininess ("Shininess", Float) = 10.0 // n
        
        _LightColor ("Light Color", Vector) = (255.0, 255.0, 255.0, 1.0)
        _AmbientColor ("Ambient Color", Vector) = (255.0, 255.0, 255.0, 1.0)
        _DiffuseColor ("Diffuse Color", Vector) = (145.0, 145.0, 145.0, 1.0)
        _SpecularColor ("Specular Color", Vector) = (255.0, 255.0, 255.0, 1.0)
    }
    // Sources:
    // - https://docs.unity3d.com/Manual/SL-VertexFragmentShaderExamples.html
    // - https://docs.unity3d.com/Manual/SL-ShaderPrograms.html
    // - https://docs.unity3d.com/Manual/SL-UnityShaderVariables.html
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                // UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 worldPosition: TEXCOORD2; 
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LightPoint;
            float _AmbientCoefficient;
            float _DiffuseCoefficient;
            float _SpecularCoefficient;
            float _Shininess;

            float4 _LightColor;
            float4 _AmbientColor;
            float4 _DiffuseColor;
            float4 _SpecularColor;

            // - https://de.wikipedia.org/wiki/Phong-Beleuchtungsmodell
            // - https://docs.unity3d.com/Manual/SL-UnityShaderVariables.html
            v2f vert (appdata input)
            {
                v2f o;
                o.normal = UnityObjectToWorldNormal(input.normal);
                o.vertex = UnityObjectToClipPos(input.vertex);
                o.worldPosition = mul(unity_ObjectToWorld, input.vertex);
                o.uv = TRANSFORM_TEX(input.uv, _MainTex);
                // UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            /**
             * 
             * Sources:
             * - Dr. David Strippgen Lecture (SoSe 20) from a slide
             * - http://www.cs.toronto.edu/~jacobson/phong-demo/
             * - https://www.mathematik.uni-marburg.de/~thormae/lectures/graphics1/code/WebGLShaderLightMat/ShaderLightMat.html
             */
            fixed4 frag (v2f output) : SV_Target
            {
                float3 L = normalize(output.worldPosition - _LightPoint.xyz);
                float3 N = normalize(output.normal);
                float attenuation = 1/pow(length(L), 2);

                // Ambient Color
                // TODO: fix this
                float3 AmbientIntensity = UNITY_LIGHTMODEL_AMBIENT.rgb;
                float AmbientColor = length(AmbientIntensity * _AmbientCoefficient);
                // Diffuse Color
                float phongDiffuse = length(_DiffuseColor.rgb);
                float DiffuseColor = attenuation * phongDiffuse * _DiffuseCoefficient * dot(L, N);
                // Specular Color
                float3 V = normalize(-output.worldPosition);
                float3 R = reflect(-L, N);
                float phongSpec = length(_SpecularColor.rgb);
                float SpecularColor = attenuation * _SpecularCoefficient * phongSpec * pow(max(0.0, dot(V, R)), _Shininess); 

                // minus one -> to make the light direction in a valid position (see inspector)
                float ColorSum = -1 * (AmbientColor + DiffuseColor + SpecularColor);

                // tex2D(...) contains the material color, so the parameter O for each terms of Phong's formula
                // is not needed 
                fixed4 col = ColorSum * tex2D(_MainTex, output.uv);
                // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
