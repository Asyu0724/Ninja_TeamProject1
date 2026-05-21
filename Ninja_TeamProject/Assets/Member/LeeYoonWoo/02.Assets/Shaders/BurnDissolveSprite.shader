Shader "Custom/Sprite Burn Dissolve Bloom"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _DissolveAmount ("Dissolve Amount", Range(0,1)) = 0
        _BurnWidth ("Burn Width", Range(0.001,0.5)) = 0.08

        [HDR] _BurnColor ("Burn Color", Color) = (1,0.18,0,1)
        [HDR] _BurnCoreColor ("Burn Core Color", Color) = (1,0.9,0.15,1)

        _BurnIntensity ("Burn Intensity", Range(1,20)) = 5
        _NoiseScale ("Noise Scale", Range(1,80)) = 18
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
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Color;
            float _DissolveAmount;
            float _BurnWidth;
            float4 _BurnColor;
            float4 _BurnCoreColor;
            float _BurnIntensity;
            float _NoiseScale;

            float hash21(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);
                return frac(p.x * p.y);
            }

            float valueNoise(float2 uv)
            {
                float2 p = floor(uv);
                float2 f = frac(uv);

                f = f * f * (3.0 - 2.0 * f);

                float a = hash21(p);
                float b = hash21(p + float2(1, 0));
                float c = hash21(p + float2(0, 1));
                float d = hash21(p + float2(1, 1));

                return lerp(
                    lerp(a, b, f.x),
                    lerp(c, d, f.x),
                    f.y
                );
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 sprite = tex2D(_MainTex, i.uv) * i.color;

                float n1 = valueNoise(i.uv * _NoiseScale);
                float n2 = valueNoise(i.uv * (_NoiseScale * 0.47) + 19.73);
                float noise = saturate(n1 * 0.72 + n2 * 0.28);

                float visible = step(_DissolveAmount, noise);

                float edge = 1.0 - smoothstep(
                    _DissolveAmount,
                    _DissolveAmount + _BurnWidth,
                    noise
                );
                edge *= visible;

                float hotCore = 1.0 - smoothstep(
                    _DissolveAmount,
                    _DissolveAmount + _BurnWidth * 0.35,
                    noise
                );
                hotCore *= visible;

                float3 burnColor = lerp(_BurnColor.rgb, _BurnCoreColor.rgb, hotCore);
                burnColor *= _BurnIntensity;

                sprite.rgb = lerp(sprite.rgb, burnColor, edge);
                sprite.a *= visible;

                return sprite;
            }
            ENDHLSL
        }
    }
}