Shader "Custom/LightBlueDiamondShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}  // The texture to modify
        _TintColor ("Tint Color", Color) = (0.7, 0.9, 1, 1)  // Light blue diamond color (almost white with a hint of blue)
        _TintIntensity ("Tint Intensity", Range(0, 1)) = 1.0  // Controls how much blue tint is applied
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
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
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            uniform sampler2D _MainTex;
            uniform float4 _TintColor;
            uniform float _TintIntensity;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                half4 texColor = tex2D(_MainTex, i.uv);

                // Convert texture to grayscale (luminance)
                float luminance = dot(texColor.rgb, float3(0.299, 0.587, 0.114));

                // Create a light blue (almost white) version of the grayscale value
                float3 lightBlue = _TintColor.rgb * luminance;

                // Blend between the original texture and the light blue based on the tint intensity
                float3 finalColor = lerp(texColor.rgb, lightBlue, _TintIntensity);

                // Return the final color with original alpha
                return half4(finalColor, texColor.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
