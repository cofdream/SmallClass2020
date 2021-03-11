Shader "Master/GPUInstancing"
{
    Properties
    {
        _Diffuse("Diffuse",Color) = (1,1,1,1)

        _OutLineWidth("OutLineWidth",float) = 0.02
    }
    SubShader
    {
        Pass
        {
            Tags { "LightMode"  = "ForwardBase"}

            Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            #pragma multi_compile_instancing

            struct appdata
            {
               float4 vertex : POSITION;
               float3 normal : NORMAL;

               UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f 
            {
                float3 worldNormal : TEXCOORD0;
                float4 pos : SV_POSITION;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            float4 _Diffuse;

            float _OutLineWidth;

            v2f vert (appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v)
                // Fragment 如果不访问 Instancing 相关的数据，则可以不加此行
                UNITY_TRANSFER_INSTANCE_ID(v,o); 

                float3 normal = v.normal;

                v.vertex.xyz += normal * _OutLineWidth;

                
                o.pos = UnityObjectToClipPos(v.vertex);
                

                o.worldNormal = normalize(mul(v.normal,(float3x3)unity_WorldToObject));

                return o;
			}

            float4 frag (v2f i) : SV_Target
            {
                float3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

                fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);

                float NdotL = 0.5f + 0.5f * dot(i.worldNormal,worldLight);

                fixed3 diffuse = _LightColor0.rgba * _Diffuse.rgba * NdotL;

                // return fixed4(ambient + diffuse,1.0);
                return fixed4(0,0,0,1);
            }

            ENDCG
        }
        

        Pass
        {
            Tags { "LightMode" = "ForwardBase"}

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            #pragma multi_compile_instancing

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal:NORMAL;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                // float4 pos: SV_POSITION; 
                // float3 color : Color;
                float3 worldNormal : TEXCOORD0;
                float4 pos : SV_POSITION;

                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            float4 _Diffuse;

            v2f vert (appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v)
                // Fragment 如果不访问 Instancing 相关的数据，则可以不加此行
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                // o.pos = UnityObjectToClipPos(v.vertex);
                // 
                // fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                // 
                // fixed3 worldNormal = normalize(mul(v.normal,(float3x3)unity_WorldToObject));
                // 
                // fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
                // 
                // float NdotL = saturate(dot(worldNormal, worldLight));
                // 
                // if(NdotL > 0.9f)
                // {
                //     NdotL = 1;
                // }
                // else if(NdotL > 0.5f)
                // {
                //     NdotL = 0.6f;
                // }
                // else
                // {
                //     NdotL = 0;
                // }
                // 
                // fixed3 diffuse = _LightColor0.rgba * _Diffuse.rgba * NdotL;
                // 
                // o.color = ambient + diffuse;

                o.pos = UnityObjectToClipPos(v.vertex);

                o.worldNormal = normalize(mul(v.normal,(float3x3)unity_WorldToObject));

               


                return o;
            }
             
            float4 frag (v2f i) : SV_Target
            {
               // return fixed4(i.color,1.0);


               fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

               fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);

               // fixed3 diffuse = _LightColor0.rgba * _Diffuse.rgba * saturate(dot(i.worldNormal,worldLight));

               // float NdotL = saturate(dot(i.worldNormal, worldLight));
               float NdotL = 0.5f + 0.5f * dot(i.worldNormal, worldLight);


               if(NdotL > 0.9f)
               {
                   NdotL = 1;
               }
               else if(NdotL > 0.5f)
               {
                   NdotL = 0.6f;
               }
               else
               {
                   NdotL = 0;
               }
               
               fixed3 diffuse = _LightColor0.rgba * _Diffuse.rgba * NdotL;

               return fixed4(ambient + diffuse,1.0);
            }
            ENDCG
        }
    }
}