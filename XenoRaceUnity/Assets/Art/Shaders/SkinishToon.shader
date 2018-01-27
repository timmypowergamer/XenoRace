Shader "Custom/CelShadingForward" {
	Properties {
		_Color("Color", Color) = (1, 1, 1, 1)
        _BumpMap("Bump Map (RGB)", 2D) = "white" {}
		_MainTex("Albedo (RGB)", 2D) = "bump" {}
		_ColorRamp("Ramp (RGB)", 2D) = "white" {}
        _MixRamp1("Mixing Factor Ramp1", Range (-1, 1)) = 0.5
		_ColorRamp2("Ramp2 (RGB)", 2D) = "white" {}
        _MixRamp2("Mixing Factor Ramp2", Range (-1, 1)) = 0.5
	}
	SubShader {
		Tags {
			"RenderType" = "Opaque"
		}
		LOD 200

		CGPROGRAM
        #pragma surface surf CelShadingForward
        #pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _ColorRamp;
		sampler2D _ColorRamp2;
		fixed4 _Color;
        uniform float _MixRamp1;
        uniform float _MixRamp2;

        
		half4 LightingCelShadingForward(SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot(s.Normal, lightDir);
            // Find where to pick color in ramp
            float2 lookUpPos = ((NdotL),(NdotL));
            half mix1 = (tex2D(_ColorRamp, lookUpPos)) * (1+_MixRamp1);
            half mix2 = (tex2D(_ColorRamp2, lookUpPos)) * (1+_MixRamp2);

            // Color adjustment based on original color
            // Math works out to be a saturate maybe????
            half vMax = (max(max(s.Albedo.r, s.Albedo.g), s.Albedo.b));
            half3 colorAdjust = vMax > 0 ? s.Albedo / vMax : 1;

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (atten * mix1 * mix2) * colorAdjust;
			c.a = s.Alpha;
			return c;
		}


		struct Input {
			float2 uv_MainTex;
            float2 uv_BumpMap;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Normal = UnpackNormal ( tex2D (_BumpMap, IN.uv_BumpMap) );
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}