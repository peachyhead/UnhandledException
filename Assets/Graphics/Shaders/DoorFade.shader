Shader "Custom/DoorFade"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Gradient ("Gradient", 2D) = "white" {}
        _Scale ("Scale", Vector) = (1, 1, 1)
        _Offset ("Offset", Vector) = (0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" }
        LOD 200

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        ENDHLSL

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _Gradient;
        float4 _Color;
        float3 _Scale;
        float3 _Offset;

        struct Input
        {
            float2 uv_Gradient;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 gradientSample = tex2D(_Gradient, IN.uv_Gradient).rgb;

            float3 scaledWorldPos = (IN.worldPos + _Offset) * _Scale;
            float3 gradientValue = lerp(float3(1, 1, 1), gradientSample, saturate(scaledWorldPos.y));

            o.Albedo = gradientValue * _Color.rgb;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
