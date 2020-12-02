Shader "Master/Sprite/Burn"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
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
                fixed4 color    : COLOR;
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
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

            sampler2D _MainTex;
            sampler2D _AlphaTex;
            float _AlphaSplitEnabled;

            float random (float2 uv) 
            {
                return frac(sin(dot(uv,float2(12.9898,78.233))) * 43758.5453123);
            }

            float noise (float2 uv) 
            { 
                // 获取 uv 的整数部分       
                float2 i = floor(uv);

                // 获取 uv 的小数部分 
                float2 f = frac(uv);

                // 获取 uv 的相邻坐标 
                float a = random(i); 
                float b = random(i + float2(1.0, 0.0)); 
                float c = random(i + float2(0.0, 1.0)); 
                float d = random(i + float2(1.0, 1.0)); 

                // 对 uv 的小数部分进行 smoothstep
                // 因为是小数部分，所以肯定是小于 1 
                // 所以去掉了对 smoothstep 的 step 操作
                // 只保留了 smooth 的过程 
                float2 u = f * f * (3 - 2 * f);

                // 没搞清楚的一堆操作
                return lerp(a, b, u.x) + (c - a)* u.y * (1.0 - u.x) + (d - b) * u.x * u.y; 
            }   
            
            float fbm (float2 uv)
            {
                float retNoise = 0.0;
                float uvOffset = 2.0;

                for ( int i = 0; i < 9; i++ ) 
                {
                    retNoise += noise(uvOffset * uv) / uvOffset; 
                    uvOffset += uvOffset; 
                }
            
                return retNoise;
            }

            fixed4 SampleSpriteTexture (float2 uv)
            {
                //float baseSpeed = -_Time.y * 0.1;
                //float2 noiseUV1 = (0.2 * uv + baseSpeed * float2(0.0,0.5)) * 80.0;
                //float2 noiseUV2 = (0.2 * uv + baseSpeed * float2(0.0,1.0)) * 80.0;

                //float noiseColor1 = noise(noiseUV1);
                //float noiseColor2 = noise(noiseUV2);

                //float noiseColor = noiseColor1 + noiseColor2;

                //// 使深色更深
                //float fireNoise = pow(noiseColor,5);

                //// 拿到 uv 的 y 值
                //float y = uv.y;

                //// 将 y 的范围控制在 整体图像的下半部分
                //fireNoise = (2 + fireNoise * 80 * y) * y;
                //fireNoise = 1 - fireNoise;

                //

                //fireNoise = pow(fireNoise,3);

                //float4 fireColor = lerp(float4(1,0,0,texColor.a),float4(1,1,0,texColor.a),fireNoise);

                //float isFireColor = step(0.001,fireNoise);

                //float4 resultColor = lerp(texColor,fireColor,isFireColor);
                #if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
                if (_AlphaSplitEnabled)
                    color.a = tex2D (_AlphaTex, uv).r;
                #endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

                float4 texColor = tex2D(_MainTex,uv);

                float4 fbmNoise = fbm(3.5 * uv);
                float burnFactor = frac(_Time.y * 0.5);

                float fadeoutFactor = frac(burnFactor * 0.9999);

                float4 resultColor = smoothstep(fadeoutFactor,fadeoutFactor + 0.1,fbmNoise);

                resultColor = texColor * resultColor;

                float3 burnColor = float3(24,3,1);

                float3 minColor = resultColor.rgb - burnFactor;

                float3 maxColor = fbmNoise * 10 * burnColor *(1 - resultColor.a);

                resultColor.rgb = lerp(minColor,maxColor,burnFactor);


                return resultColor;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
                c.rgb *= c.a;
                return c;
            }
        ENDCG
        }
    }
}