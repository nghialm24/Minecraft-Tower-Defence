Shader "Unlit/DiffuseAtten"
{
    
    Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Atten("Atten", Range(0, 2)) = 1
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 150

    Cull Off

CGPROGRAM
#pragma surface surf Lambert noforwardadd

sampler2D _MainTex;
fixed _Atten;

struct Input {
    float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Atten;
    o.Albedo = c.rgb;
    o.Alpha = c.a;
}
ENDCG
}

Fallback "Mobile/VertexLit"
}
