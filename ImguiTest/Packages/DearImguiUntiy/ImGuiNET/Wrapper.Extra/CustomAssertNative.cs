using System.Runtime.InteropServices;

namespace ImGuiNET
{
    static unsafe partial class CustomAssertNative
    {
#if (UNITY_IOS && !UNITY_EDITOR) || (UNITY_IPHONE && !UNITY_EDITOR)
        public const string dllName = "__Internal";
#else
        public const string dllName = "cimgui";
#endif

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PluginLogAssert(byte* condition, byte* file, int line);

        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PluginDebugBreak();
    }
}
