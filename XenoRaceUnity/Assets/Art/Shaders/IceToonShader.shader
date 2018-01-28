Shader "Custom/IceToon"
{

 Properties
{
  // color of the water
  _Color("Color", Color) = (1, 1, 1, 1)
  // color of the edge effect
  _EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
  // width of the edge effect
  _DepthFactor("Depth Factor", Range (0, 1)) = 1.0
  _DepthRampTex("Depth Ramp (SHOULD BE BLUE ISH)", 2D) = "white" {}
}

SubShader
{
Pass
{

CGPROGRAM
// required to use ComputeScreenPos()
#include "UnityCG.cginc"

#pragma vertex vert
#pragma fragment frag
 
 // Unity built-in - NOT required in Properties
 sampler2D _CameraDepthTexture;

 sampler2D _DepthRampTex;
 float _DepthFactor;
 half4 _EdgeColor;
 half4 _Color;

struct vertexInput
 {
   float4 vertex : POSITION;
 };

struct vertexOutput
 {
   float4 pos : SV_POSITION;
   float4 screenPos : TEXCOORD1;
 };

vertexOutput vert(vertexInput input)
  {
    vertexOutput output;

    // convert position to world space
    output.pos = UnityObjectToClipPos(input.vertex);

    // compute depth (screenPos is a float4)
    output.screenPos = ComputeScreenPos(output.pos);

    return output;
  }

  float4 frag(vertexOutput input) : COLOR
  {
    // sample camera depth texture
    float4 s = UNITY_PROJ_COORD(input.screenPos);
    float4 depthSample = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, s);
    float depth = LinearEyeDepth(depthSample).r;


    // apply the DepthFactor to be able to tune at what depth values
    // the foam line actually starts
    float foamLine = saturate(_DepthFactor * (depth - input.screenPos.w));
    float4 foamRamp = float4(tex2D(_DepthRampTex, float2(foamLine, 0.5)).rgb, 1);
    // multiply the edge color by the foam factor to get the edge,
    // then add that to the color of the water
    float4 col = _Color * foamRamp;
    if(foamLine < 0.1) col *= _EdgeColor;
    return col;
  }

  ENDCG
}}}