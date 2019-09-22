// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprite/BonesOutline"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
        _Outline ("Outline", Color) = (0,0,0,.7)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        _Thiccness ("Thicc", int) = 1
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
            #pragma exclude_renderers d3d11_9x 
            #pragma exclude_renderers d3d9
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
            float4 _MainTex_TexelSize;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
            float4 _Outline;
            int _Thiccness;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
                // Made by Ian, David, and Kevin @UTGameJam2019 9/22/2019
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				if(c.a <= 0.5){
                    int i = _Thiccness;
                        float up = IN.texcoord.y + _MainTex_TexelSize.y * i;
                        float down = IN.texcoord.y - _MainTex_TexelSize.y * i;
                        float right = IN.texcoord.x + _MainTex_TexelSize.x * i;
                        float left = IN.texcoord.x - _MainTex_TexelSize.x * i;

                        fixed4 upc = SampleSpriteTexture (float2(IN.texcoord.x, up));
                        fixed4 downc = SampleSpriteTexture (float2(IN.texcoord.x, down));
                        fixed4 rightc = SampleSpriteTexture (float2(right, IN.texcoord.y));
                        fixed4 leftc = SampleSpriteTexture (float2(left, IN.texcoord.y));

                        if(upc.a >= 0.5 || downc.a >= 0.5 || rightc.a >= 0.5 || leftc.a >= 0.5){
                            c = _Outline;
                        }   
                    
                }
                c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}