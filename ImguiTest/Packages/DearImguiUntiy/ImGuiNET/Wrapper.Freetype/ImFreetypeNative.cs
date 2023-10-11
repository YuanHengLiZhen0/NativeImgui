using System.Runtime.InteropServices;

namespace ImGuiNET
{
    static unsafe partial class ImFreetypeNative
    {
#if (UNITY_IOS && !UNITY_EDITOR) || (UNITY_IPHONE && !UNITY_EDITOR)
        public const string dllName = "__Internal";
#else
        public const string dllName = "cimgui-freetype";
#endif
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool frBuildFontAtlas(ImFontAtlas* atlas, uint extra_flags);
    }
}
