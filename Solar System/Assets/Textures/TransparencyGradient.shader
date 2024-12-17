Shader "Custom/VerticalTransparencyGradient"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" { }
        _StartAlpha ("Start Transparency", Range(0,1)) = 1.0
        _EndAlpha ("End Transparency", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _StartAlpha;
            float _EndAlpha;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 texColor = tex2D(_MainTex, i.uv);

                // Calculate the alpha value based on the vertical position (y-axis)
                float alpha = lerp(_StartAlpha, _EndAlpha, i.uv.y);

                texColor.a *= alpha; // Apply the alpha gradient to the texture

                return texColor;
            }
            ENDCG
        }
    }
}
