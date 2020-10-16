Shader "Master/Sprite/EdgeDetection"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0

		_EdgeColor("EdgeColor", Color) = (1,1,1,0)
		_EdgeFactor("EdgeFactor", Range(0,0.01)) = 0.003

		_BgColor("BgColor", Color) = (1,1,1,0)
		_BgFactor("_BgFactor", Range(0,1)) = 1
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

				fixed4	_EdgeColor;
				float _EdgeFactor;

				fixed4	_BgColor;
				float _BgFactor;

				fixed4 Luminance(fixed4 color) {
					return 0.2125 * color.r + 0.7154 * color.g + 0.0721 * color.b;
				}

				fixed4 SampleSpriteTexture(float2 uv)
				{
					fixed4 color = tex2D(_MainTex, uv);

					float xAdd = uv.x + _EdgeFactor;
					float xDec = uv.x - _EdgeFactor;
					float yAdd = uv.y + _EdgeFactor;
					float yDec = uv.y - _EdgeFactor;

					fixed4 edgeX = Luminance(tex2D(_MainTex, float2(xDec, yAdd))) * -1;
					//edgeX += Luminance(tex2D(_MainTex, float2(uv.x, yAdd))) * 0;
					edgeX += Luminance(tex2D(_MainTex, float2(xAdd, yAdd))) * 1;

					edgeX += Luminance(tex2D(_MainTex, float2(xDec, uv.y))) * -2;
					//edgeX += Luminance(tex2D(_MainTex, float2(uv.x, uv.y))) * 0;
					edgeX += Luminance(tex2D(_MainTex, float2(xAdd, uv.y))) * 2;

					edgeX += Luminance(tex2D(_MainTex, float2(xDec, yDec))) * -1;
					//edgeX += Luminance(tex2D(_MainTex, float2(uv.x, yDec))) * 0;
					edgeX += Luminance(tex2D(_MainTex, float2(xAdd, yDec))) * 1;


					fixed4 edgeY = Luminance(tex2D(_MainTex, float2(xDec, yAdd))) * -1;
					edgeY += Luminance(tex2D(_MainTex, float2(uv.x, yAdd))) * -2;
					edgeY += Luminance(tex2D(_MainTex, float2(xAdd, yAdd))) * -1;

					//edgeY += Luminance(tex2D(_MainTex, float2(xDec, uv.y))) * 0;
					//edgeY += Luminance(tex2D(_MainTex, float2(uv.x, uv.y))) * 0;
					//edgeY += Luminance(tex2D(_MainTex, float2(xAdd, uv.y))) * 0;

					edgeY += Luminance(tex2D(_MainTex, float2(xDec, yDec))) * 1;
					edgeY += Luminance(tex2D(_MainTex, float2(uv.x, yDec))) * 2;
					edgeY += Luminance(tex2D(_MainTex, float2(xAdd, yDec))) * 1;

					half edge = abs(edgeX) + abs(edgeY);

					/*_BgColor.a = color.a;
					_EdgeColor.a = color.a;

					fixed4 bgColor = lerp(color, _BgColor, _BgFactor);

					color = lerp(bgColor, _EdgeColor, edge);
					*/
					edge = 1 - edge;

					color = fixed4(edge, edge, edge, color.a);


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
