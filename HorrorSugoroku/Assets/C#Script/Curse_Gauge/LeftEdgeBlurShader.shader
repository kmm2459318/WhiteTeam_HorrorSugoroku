Shader "Custom/LeftEdgeBlurShader"
{
    Properties
    {
        _Color ("Color", Color) = (0,0,0,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _BlurAmount;

            Varyings vert (Attributes input)
            {
                Varyings output;
                output.positionHCS = TransformObjectToHClip(input.positionOS);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                return output;
            }

            half4 frag (Varyings input) : SV_Target
            {
                half4 color = tex2D(_MainTex, input.uv) * _Color;
                float blur = smoothstep(0.0, _BlurAmount, input.uv.x);
                color.a *= blur; // ç∂Ç©ÇÁâEÇ÷ÇÃÇ⁄Ç©Çµ
                return color;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}