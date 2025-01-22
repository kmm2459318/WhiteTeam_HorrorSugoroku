Shader "Custom/RainbowGradientShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Speed("Speed", Float) = 1
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _Speed;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    return o;
                }

                float4 frag(v2f i) : SV_Target
                {
                    float time = _Time.y * _Speed;
                    float r = sin(i.uv.x * 10 + time) * 0.5 + 0.5;
                    float g = sin(i.uv.x * 10 + time + 2.0) * 0.5 + 0.5;
                    float b = sin(i.uv.x * 10 + time + 4.0) * 0.5 + 0.5;
                    return float4(r, g, b, 1.0);
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
