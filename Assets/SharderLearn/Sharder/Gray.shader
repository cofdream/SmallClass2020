Shader "Master/Sprite/Gray"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_GrayFactor("GrayFactor", Range(0,1)) = 1 // 设置灰度程度

		[MaterialToggle] _Gradient("HorizontalGradient", Float) = 0
		[ToolTip]_FromHColor("_FromHColor", Color) = (0,0,0,0)
		_ToHColor("_ToHColor", Color) = (1,1,1,0)

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
				float _GrayFactor;

				float4  _FromHColor;
				float4  _ToHColor;
				float _HorizontalGradient;

				float4  _FromVColor;
				float4  _ToVColor;
				float _VerticalGradient;

				fixed4 SampleSpriteTexture(float2 uv)
				{
					fixed4 color = tex2D(_MainTex, uv);

					float gray = color.r * 0.299 + color.g * 0.587 + color.b * 0.114;

					float4 graycolor = float4(gray, gray, gray, color.a);

					color = lerp(color, graycolor, _GrayFactor);

					if (_HorizontalGradient)
					{
						_FromHColor.a = color.a;
						_ToHColor.a = color.a;
						color = lerp(_FromHColor, _ToHColor, uv.x) * color;
					}
					else
					{
						_FromVColor.a = color.a;
						_ToVColor.a = color.a;
						color = lerp(_ToVColor, _FromVColor, uv.y) * color;
					}



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
