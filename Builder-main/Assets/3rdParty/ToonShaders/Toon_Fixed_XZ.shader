// Unlit Shader
// (c) Yaroslav Stadnyk

Shader "Toon Shaders/Fixed (XZ)"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Lit ("Lit", float) = 0.5
    }
    SubShader
    {
        Tags
        { 
            "RenderType" = "Opaque"
            "LightMode" = "ForwardBase"
        }

        Pass 
        { 
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "ToonFunctions.cginc"

            sampler2D _MainTex;
            half4 _MainTex_ST;
            half4 _Color;
            half _Lit;

            v2f vert(appdata_base v) 
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.normal = v.normal;
                o.uv = TRANSFORM_TEX(mul(unity_ObjectToWorld, v.vertex).xz, _MainTex);
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                UNITY_TRANSFER_FOG(o, o.pos);
                return o;
            }

            half4 frag(v2f i) : COLOR
            {
                half4 texColor = tex2D(_MainTex, i.uv) * _Color;
                
                half3 surface = calc_surface(texColor.rgb, i.worldNormal, _Lit);
                surface = calc_shadow(surface, i);

                UNITY_APPLY_FOG(i.fogCoord, surface);
                
                return half4(surface.x, surface.g, surface.b, texColor.a);
            }

            ENDCG 
        } 
    } 
    
    Fallback "VertexLit"
}