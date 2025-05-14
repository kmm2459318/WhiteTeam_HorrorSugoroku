Shader "Custom/RedPulseGlow"
{
    Properties
    {
        _EmissionColor ("Emission Color", Color) = (1, 0, 0, 1)
        _EmissionStrength ("Emission Strength", Float) = 5.0
        _GlowSpeed ("Glow Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Name "FORWARD"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float4 _EmissionColor;
            float _EmissionStrength;
            float _GlowSpeed;

            float _Time; // Unityが自動で渡してくれる時間変数

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = TransformObjectToHClip(v.vertex.xyz);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float glow = (sin(_Time * _GlowSpeed) * 0.5 + 0.5); // 0〜1の値
                float3 emission = _EmissionColor.rgb * _EmissionStrength * glow;
                return half4(emission, 1.0); // Alphaは常に1.0
            }
            ENDHLSL
        }
    }
}
