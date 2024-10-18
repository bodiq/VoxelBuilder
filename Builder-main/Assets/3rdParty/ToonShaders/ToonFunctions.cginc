struct v2f
{
    half4 pos : SV_POSITION;
    half3 worldNormal : NORMAL;
    half3 normal : TEXCOORD0;
    half2 uv : TEXCOORD1;
    LIGHTING_COORDS(0, 2)
    UNITY_FOG_COORDS(3)
};

half3 saturate(half3 target, half value)
{
    half luma = dot(target, half3(0.2126729, 0.7151522, 0.0721750));
    return luma.xxx + value.xxx * (target - luma.xxx);
}

half3 contrast(half3 target, half value)
{
    const half midpoint = 0.21763764082;
    return (target - midpoint) * value + midpoint;
}

half3 calc_surface(half3 target, half3 worldNormal, half value)
{
    half3 ownShadow = dot(_WorldSpaceLightPos0.xyz, worldNormal) * value;
    return target * ownShadow + target;
}

half3 calc_shadow(half3 target, v2f i)
{
    half3 dropShadow = LIGHT_ATTENUATION(i);
    return lerp(0.0, target, dropShadow);
}
