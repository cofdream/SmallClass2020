Shader "Master/Sprite/Sharp"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_BlurFactor("BlurFactor", Range(0,0.1)) = 0.03
	}

		SubShader
		{
			Tags
			{
				"Queue" = "Transparent"
				"IgnoreProjector" = "True"
				"RenderType" = "Transparent"
				"PreviewType" = "Plane"
				"CanUseSpriteAtlas" = "True"
			}

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

			Pass
			{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile _ PIXELSNAP_ON
				#include "UnityCG.cginc"

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
				};

				fixed4 _Color;

				v2f vert(appdata_t IN)
				{
					v2f OUT;
					OUT.vertex = UnityObjectToClipPos(IN.vertex);
					OUT.texcoord = IN.texcoord;
					OUT.color = IN.color * _Color;
					#ifdef PIXELSNAP_ON
					OUT.vertex = UnityPixelSnap(OUT.vertex);
					#endif

					return OUT;
				}

				sampler2D _MainTex;
				sampler2D _AlphaTex;
				float _AlphaSplitEnabled;

				float _BlurFactor;

				fixed4 SampleSpriteTexture(float2 uv)
				{
					float xAdd = uv.x + _BlurFactor;
					float xDec = uv.x - _BlurFactor;
					float yAdd = uv.y + _BlurFactor;
					float yDec = uv.y - _BlurFactor;

					// 左上 - 右上
					fixed4 color = tex2D(_MainTex, float2(xDec, yAdd)) * -1;
					color += tex2D(_MainTex, float2(uv.x, yAdd)) * -1;
					color += tex2D(_MainTex, float2(xAdd, yAdd)) * -1;

					// 左中-右中								 
					color += tex2D(_MainTex, float2(xDec, uv.y)) * -1;
					color += tex2D(_MainTex, float2(uv.x, uv.y)) * 9;
					color += tex2D(_MainTex, float2(xAdd, uv.y)) * -1;

					// 左下 - 右下								 
					color += tex2D(_MainTex, float2(xDec, yDec)) * -1;
					color += tex2D(_MainTex, float2(uv.x, yDec)) * -1;
					color += tex2D(_MainTex, float2(xAdd, yDec)) * -1;

	#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
					if (_AlphaSplitEnabled)
						color.a = tex2D(_AlphaTex, uv).r;
	#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

					return color;
				}

				fixed4 frag(v2f IN) : SV_Target
				{
					fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
					c.rgb *= c.a;
					return c;
				}
			ENDCG
			}
		}
}
