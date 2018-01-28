// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Default"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        _StephensMagicDepthCutoffValue("Magic Cutoff Value before I don't render it (Magic units)", Float) = 0.0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent+100" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
        Fog { Mode Off }
        ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha

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
                float4 screenPos : TEXCOORD1;
			};
			
			fixed4 _Color;
            float _StephensMagicDepthCutoffValue;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif
                // compute depth (screenPos is a float4)
                OUT.screenPos = ComputeScreenPos(OUT.vertex);
				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

            // Unity built-in - NOT required in Properties
            sampler2D _CameraDepthTexture;

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
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
                    
				c.rgb *= c.a;
                // sample camera depth texture
                float4 s = UNITY_PROJ_COORD(IN.screenPos);
                float4 depthSample = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, s);
                float depth = LinearEyeDepth(depthSample).r;
                if(depth > 4) {
                    float dist = length(ObjSpaceViewDir(IN.vertex));
                    if(dist > _StephensMagicDepthCutoffValue) {
                        c.a *= 0;
                    }
                }
				return c;
			}
		ENDCG
		}
	}
}