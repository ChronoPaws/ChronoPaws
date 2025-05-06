Shader "Custom/Glitch2DSprite"
{
Properties
{
    _MainTex ("Sprite Texture", 2D) = "white" {}
    _PixelSize ("Pixel Size", Float) = 0.005
    _Distortion ("Distortion", Float) = 0.02
    _ColorShift ("Color Shift", Float) = 0.01
}
SubShader
{
    Tags {"RenderType"="Opaque"}
    LOD 100

    Pass
    {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;
        float _PixelSize;
        float _Distortion;
        float _ColorShift;

        struct appdata {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        v2f vert (appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            return o;
        }

        fixed4 frag (v2f i) : SV_Target
        {
            // Pixelation
            float2 pixelUV = floor(i.uv / _PixelSize) * _PixelSize;

            // Glitch noise
            float glitch = sin(_Time.y * 50.0 + i.uv.y * 100.0) * _Distortion;

            // RGB Shift
            float2 uvR = pixelUV + float2(_ColorShift, 0) + glitch;
            float2 uvG = pixelUV;
            float2 uvB = pixelUV - float2(_ColorShift, 0) + glitch;

            // Sample the texture
            float4 colR = tex2D(_MainTex, uvR);
            float4 colG = tex2D(_MainTex, uvG);
            float4 colB = tex2D(_MainTex, uvB);

            // Combine the colors
            float4 col;
            col.r = colR.r;
            col.g = colG.g;
            col.b = colB.b;

            // Preserve the alpha value (ensure transparency works properly)
            col.a = colG.a;

            return col;
        }
        ENDCG
    }
}
}
