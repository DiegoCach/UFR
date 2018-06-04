Shader "Custom/invisible" {
        Properties
        {
                _MainTex ("Base (RGB)", 2D) = "white" {}
                _Cutoff("Cutoff", Range(0, 1)) = 0
                _BurnShift("BurnShift", Range(0, 0.1)) = 0.01
                _BurnColor("Cutoff", Color) = (1, 1, 0, 1)
                _DistortionInfluence("DistortionInfluence", Range(0, 0.05)) = 0.01
        }
        SubShader
        {
                Tags { "Queue" = "Transparent" }
                GrabPass {"_GrabTex"}
 
 
 
                Tags { "RenderType"="Opaque" }
                   
                CGPROGRAM
                #pragma only_renderers d3d9 opengl
                #pragma glsl
                #pragma surface surf Lambert
     
                sampler2D _MainTex;
                fixed _Cutoff;
                fixed _BurnShift;
                fixed4 _BurnColor;
                sampler2D _GrabTex;
                fixed _DistortionInfluence;
     
                struct Input
                {
                        float2 uv_MainTex;
                        float4 screenPos;
                        float3 worldNormal;
                };
     
                void surf (Input IN, inout SurfaceOutput o)
                {
                        half4 c = tex2D (_MainTex, IN.uv_MainTex);
     
                        fixed pos = c.a - _Cutoff;
                        fixed bPos = pos - _BurnShift;
                        fixed isBurn = 1-step(-bPos, 0);
 
                        float3 tpCoord = IN.screenPos.xyz/IN.screenPos.w;
                        #if (UNITY_UV_STARTS_AT_TOP)
                            tpCoord.y=1.0-tpCoord.y;              
                        #endif
                        fixed3 grabColor = tex2D(_GrabTex, tpCoord+IN.worldNormal.xz*IN.worldNormal.y*_DistortionInfluence).xyz;
     
                        o.Albedo = lerp(c.rgb, _BurnColor.rgb, isBurn);
                        fixed cutout = 1.0-step(-(c.a-_Cutoff), 0.0);
                        o.Albedo = lerp(o.Albedo, grabColor, cutout);
 
                        o.Emission = lerp(fixed3(0,0,0), _BurnColor.rgb, isBurn);
                        o.Emission = lerp(o.Emission, 0.0, cutout);
                }
                ENDCG
        }
        FallBack "VertexLit"
}
 
 