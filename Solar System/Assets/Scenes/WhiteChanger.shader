Shader "Custom/GrayscaleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}  // The texture to modify
        _GrayScaleAmount ("Grayscale Amount", Range(0, 1)) = 1.0  // Controls how much grayscale is applied (1 = full grayscale, 0 = no grayscale)
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
            uniform float _GrayScaleAmount;

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
                
                // Convert the texture color to grayscale
                float gray = dot(texColor.rgb, float3(0.299, 0.587, 0.114));

                // Blend between the original color and the grayscale value based on the _GrayScaleAmount
                float3 finalColor = lerp(texColor.rgb, float3(gray, gray, gray), _GrayScaleAmount);
                
                // Return the final color with the original alpha value
                return half4(finalColor, texColor.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
