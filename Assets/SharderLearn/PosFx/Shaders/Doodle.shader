Shader "Master/PostFx/Doodle"
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


            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float2 originalUV = uv;

                // 速度
                float speed = floor(_Time * 100);

                uv.x = sin((uv.x * 10 + speed) * 4);
                uv.y = cos((uv.y * 10 + speed) * 4);
                uv.xy = lerp(originalUV,originalUV + uv,0.005);

                float4 color = tex2D(_MainTex,uv);

                return color;
            }
            ENDCG
        }
    }
}