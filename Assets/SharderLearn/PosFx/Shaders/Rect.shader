﻿Shader "Master/PostFx/Rect"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            float IsInLine(float x,float y)
            {
                return smoothstep(x - 0.02, x, y) -
                     smoothstep(x, x + 0.02, y);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex,i.uv);

                fixed2 bottomLeft = step(fixed2(0.1,0.1),i.uv);
                fixed2 topRight = step(fixed2(0.1,0.1), 1 - i.uv);

                float inRect = bottomLeft.x * bottomLeft.y * topRight.x * topRight.y;

                color = lerp(color,float4(0.0,0.0,0.0,1.0), inRect);

                return color;
            }
            ENDCG
        }
    }
}