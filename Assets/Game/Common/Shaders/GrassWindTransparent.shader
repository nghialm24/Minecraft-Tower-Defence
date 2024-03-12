Shader "Unlit/GrassWindTransparent"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseMap("Noise Map", 2D) = "clear" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent"}
        
        Blend One OneMinusSrcAlpha

        ZWrite Off

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _NoiseMap;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;

                float d = tex2Dlod(_NoiseMap, float4(v.vertex.x + _Time.y * 0.3, v.vertex.y * 0.06 + v.vertex.z * 0.06, 0, 0)).r - 0.5;
                float mag = d * 1 * saturate(v.vertex.y);
                v.vertex.xyz += float3(1, 0, 0) * mag;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                col.rgb *= col.a;

                return col;
            }
            ENDCG
        }
    }
}
