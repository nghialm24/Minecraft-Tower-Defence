Shader "Unlit/vertexlit_leaf_a"
{
   Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
        _NoiseMap("Noise Map", 2D) = "clear" {}
    _AlphaClip("Alpha Clip", Range(0, 1)) = 0.05
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 150

CGPROGRAM

#pragma surface surf Lambert noforwardadd vertex:vert

sampler2D _MainTex;
sampler2D _NoiseMap;

struct Input {
    float2 uv_MainTex;
};

void vert (inout appdata_full v) 
{
    float d = tex2Dlod(_NoiseMap, float4(v.vertex.x + _Time.y * 0.2, v.vertex.y * 0.06 + v.vertex.z * 0.06, 0, 0)).r - 0.5;
    float mag = d * 0.5 * saturate(v.vertex.y);
    v.vertex.xyz += float3(1, 0, 1) * mag;
}

void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
    clip(c.a - 0.5);
    o.Albedo = c.rgb;
    o.Alpha = c.a;
}
ENDCG
}

Fallback "Mobile/VertexLit"
}

