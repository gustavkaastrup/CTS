Shader "Custom/SimpleBlurShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200

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
            float _BlurSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // Weiche Kanten erzeugen, ohne die Helligkeit zu ändern
                fixed4 blurredColor = tex2D(_MainTex, i.uv + float2(-_BlurSize, 0)) * 0.2 +
                                      tex2D(_MainTex, i.uv + float2(_BlurSize, 0)) * 0.2 +
                                      tex2D(_MainTex, i.uv + float2(0, -_BlurSize)) * 0.2 +
                                      tex2D(_MainTex, i.uv + float2(0, _BlurSize)) * 0.2;
                blurredColor += col * 0.2; // Originalfarbe hinzufügen
                return blurredColor;
            }
            ENDCG
        }
    }
}
