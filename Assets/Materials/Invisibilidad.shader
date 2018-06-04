Shader "Custom/Cloak"
{
	Properties
	{
		[HideInspector] _dirty("", int) = 1
		_Blending("Blending", Range(0, 1)) = 0
		_DistortionMap("DistortionMap", 2D) = "bump" {}
	_DistortionScale("DistortionScale", Range(0, 1)) = 0
		_RippleScale("RippleScale", Range(0, 20)) = 0
		_RippleSpeed("RippleSpeed", Range(0, 1)) = 0
	}

		SubShader
	{
		Tags{ "RenderType" = "Opaque" "Queue" = "Transparent+0" "IsEmissive" = "true" }
		Cull Back
		GrabPass{ "_GrabScreen0" }
		CGPROGRAM
#include "UnityShaderVariables.cginc"
#pragma target 3.0
#pragma surface surf Standard keepalpha addshadow fullforwardshadows
		struct Input
	{
		float4 screenPos;
	};

	uniform sampler2D _GrabScreen0;
	uniform sampler2D _DistortionMap;
	uniform float _RippleScale;
	uniform float _RippleSpeed;
	uniform float _DistortionScale;
	uniform float _Blending;

	void surf(Input i, inout SurfaceOutputStandard o)
	{
		o.Albedo = float3(0, 0, 0);
		float4 screenPos7 = i.screenPos;
#if UNITY_UV_STARTS_AT_TOP
		float scale7 = -1.0;
#else
		float scale7 = 1.0;
#endif
		float halfPosW7 = screenPos7.w * 0.5;
		screenPos7.y = (screenPos7.y - halfPosW7) * _ProjectionParams.x * scale7 + halfPosW7;
		screenPos7.w += 0.00000000001;
		screenPos7.xyzw /= screenPos7.w;
		float4 temp_cast_0 = (_Time.y * _RippleSpeed);
		float4 temp_cast_3 = 1.0;
		float4 temp_output_3_0 = lerp(tex2Dproj(_GrabScreen0, UNITY_PROJ_COORD((float4((UnpackNormal(tex2D(_DistortionMap, (_RippleScale * float2((temp_cast_0 + screenPos7).x, (temp_cast_0 + screenPos7).y)).xy)) * _DistortionScale), 0.0) + screenPos7))), temp_cast_3, _Blending);
		o.Emission = temp_output_3_0.rgb;
		o.Metallic = temp_output_3_0.r;
		o.Smoothness = temp_output_3_0.r;
		o.Alpha = 1;
	}
	ENDCG
	}
		Fallback "Diffuse"
}