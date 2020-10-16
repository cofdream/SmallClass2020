﻿Shader "Master/Sprite/Outline"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0

		_EdgeColor("EdgeColor", Color) = (0,0,0,1)
		_EdgeFactor("EdgeFactor", Range(0,1)) = 0.003
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

				fixed4 SampleSpriteTexture(float2 uv)
				{
					fixed4 color = tex2D(_MainTex, uv);

					float xAdd = uv.x + _EdgeFactor;
					float xDec = uv.x - _EdgeFactor;
					float yAdd = uv.y + _EdgeFactor;
					float yDec = uv.y - _EdgeFactor;

					half edgeX = tex2D(_MainTex, float2(xDec, yAdd)).a * -1;
					//edgeX += tex2D(_MainTex, float2(uv.x, yAdd)).a * 0;
					edgeX +=tex2D(_MainTex, float2(xAdd, yAdd)).a * 1;

					edgeX += tex2D(_MainTex, float2(xDec, uv.y)).a * -2;
					//edgeX += tex2D(_MainTex, float2(uv.x, uv.y)).a * 0;
					edgeX += tex2D(_MainTex, float2(xAdd, uv.y)).a * 2;

					edgeX += tex2D(_MainTex, float2(xDec, yDec)).a * -1;
					//edgeX += _MainTex, float2(uv.x, yDec)).a * 0;
					edgeX += tex2D(_MainTex, float2(xAdd, yDec)).a * 1;


					half edgeY = tex2D(_MainTex, float2(xDec, yAdd)).a * -1;
					edgeY += tex2D(_MainTex, float2(uv.x, yAdd)).a * -2;
					edgeY += tex2D(_MainTex, float2(xAdd, yAdd)).a * -1;

					//edgeY += tex2D(_MainTex, float2(xDec, uv.y)).a * 0;
					//edgeY += tex2D(_MainTex, float2(uv.x, uv.y)).a * 0;
					//edgeY += tex2D(_MainTex, float2(xAdd, uv.y)).a * 0;

					edgeY += tex2D(_MainTex, float2(xDec, yDec)).a * 1;
					edgeY += tex2D(_MainTex, float2(uv.x, yDec)).a * 2;
					edgeY += tex2D(_MainTex, float2(xAdd, yDec)).a * 1;

					half edge = abs(edgeX) + abs(edgeY);

					//_EdgeColor.a = color.a;
					color = lerp(color, _EdgeColor, edge);


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
