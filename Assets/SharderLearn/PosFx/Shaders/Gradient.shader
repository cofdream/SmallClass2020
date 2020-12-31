Shader "Master/PostFx/Gradient"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}

        _Color("Tint", Color) = (1,1,1,1)
            // 初识颜色
            _FromColor("FromColor",Color) = (0,1,1,1)
            // 目标颜色
            _ToColor("ToColor",Color) = (1,1,0,1)
            // 是否是垂直方向
            [Toggle(VertialDirection)] _VerticalDirection("Vertical Direction", Int) = 1
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

                float4 _FromColor;
                float4 _ToColor;
                int _VerticalDirection;

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
                    fixed4 color = tex2D(_MainTex, i.uv);

                    float toColorFactor = lerp(i.uv.x,i.uv.y,_VerticalDirection);

                    color = lerp(_FromColor,_ToColor, toColorFactor) * color;

                    return color;
                }
                ENDCG
            }
        }
}