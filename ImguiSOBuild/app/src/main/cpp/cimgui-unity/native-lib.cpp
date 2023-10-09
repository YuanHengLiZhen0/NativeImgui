#include <jni.h>
#include <string>
#include "imgui.h"
extern "C" JNIEXPORT jstring JNICALL Java_com_netease_imguiUnity_MainActivity_stringFromJNI(
        JNIEnv* env,
        jobject /* this */) {
    std::string hello = "Hello from C++";
    return env->NewStringUTF(hello.c_str());
}


extern "C"
JNIEXPORT jdouble JNICALL
Java_com_netease_imguiUnity_MainActivity_igGetTime(JNIEnv *env, jobject thiz) {
    ImFontAtlas* shared_font_atlas = nullptr;
    ImGuiContext* context = ImGui::CreateContext(shared_font_atlas);
    if(context== nullptr){
        return -1;
    }
    ImGui::SetCurrentContext(context);
    ImGuiIO* io=&ImGui::GetIO();
    io->Fonts->SetTexID(0);
    return 1;
}