Shader "Unlit/asdasdasd"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LightPoint ("Light Point Postion", Vector) = (0, 0, 0, 0)
        _AmbientCoefficient ("Ambient Coefficient", Float) = 1.0
        _DiffuseCoefficient ("Diffuse Coefficient", Float) = 1.0
        _SpecularCoefficient ("Specular Coefficient", Float) = 1.0
    }
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
                UNITY_FOG_COORDS(1)
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

            v2f vert (appdata input)
            {
                v2f o;
                o.normal = UnityObjectToWorldNormal(input.normal);
                o.vertex = UnityObjectToClipPos(input.vertex);
                o.worldPosition = mul(unity_ObjectToWorld, input.vertex);
                o.uv = TRANSFORM_TEX(input.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            /**
             *
             *
             *
             *
             *
             *
             */
            fixed4 frag (v2f output) : SV_Target
            {
                float3 L = normalize(output.worldPosition - _LightPoint.xyz);
                float3 N = normalize(output.normal);
                float attenuation = 1/pow(length(L), 2);

                // Ambient Color
                float3 AmbientIntensity = UNITY_LIGHTMODEL_AMBIENT.rgb;
                float3 AmbientColor = AmbientIntensity * _AmbientCoefficient;
                // Diffuse Color
                float DiffuseColor = attenuation * _DiffuseCoefficient * dot(L, N);
                // Specular
                float3 V = normalize(-output.worldPosition);
                float3 R = reflect(-L, N);
                float SpecularColor = attenuation * _SpecularCoefficient * max(0.0, dot(V, R)); 

                float ColorSum = AmbientColor + DiffuseColor + SpecularColor;
                
                fixed4 col = ColorSum * tex2D(_MainTex, output.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
