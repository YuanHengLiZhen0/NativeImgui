#ifndef DEARIMGUI_BUILTIN_INCLUDED
#define DEARIMGUI_BUILTIN_INCLUDED

#include "UnityCG.cginc"
#include "Packages/com.realgames.dear-imgui/Resources/Shaders/Common.hlsl"

sampler2D _Tex;

half4 unpack_color(uint c)
{
    half4 color = half4(
        (c      ) & 0xff,
        (c >>  8) & 0xff,
        (c >> 16) & 0xff,
        (c >> 24) & 0xff
    ) / 255;
#ifndef UNITY_COLORSPACE_GAMMA
    color.rgb = GammaToLinearSpace(color.rgb);
#endif
    return color;
}

Varyings ImGuiPassVertex(ImVert input)
{
    Varyings output  = (Varyings)0;
    output.vertex    = UnityObjectToClipPos(float4(input.vertex, 0, 1));
    output.uv        = float2(input.uv.x, 1 - input.uv.y);
    //output.color     = unpack_color(input.color);

	output.color.x = input.color & 0xff;
	output.color.y = (input.color & 0xff00) >> 8;
	output.color.xy = output.color.xy / 255.0;
	output.color.w = 1.0;
    return output;
}

half4 ImGuiPassFrag(Varyings input) : SV_Target
{
    return input.color * tex2D(_Tex, input.uv);
}

#endif
