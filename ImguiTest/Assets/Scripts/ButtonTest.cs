using AOT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using ImGuiNET;

public class ButtonTest : MonoBehaviour
{

    public Button _BtnTestKeyboard;
    public Button _BtnShowWindow;
    // Start is called before the first frame update
    static TouchScreenKeyboard keyboard;
    private string lastInput = String.Empty;

    private bool bShowWindow;
    void Start()
    {
       
        //_Btn = GetComponent<Button>();
        _BtnTestKeyboard.onClick.AddListener(ButtonTestKeyboardClick);
        _BtnShowWindow.onClick.AddListener(ButtonShowWindowClick);
        bShowWindow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchScreenKeyboard.visible == false && keyboard != null)
        {
            if (keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                DealInput(keyboard.text);
                lastInput = String.Empty;
                keyboard = null;
            }
        }
    }


    private void ButtonShowWindowClick()
    {
        if (!bShowWindow)
        {
            ImGuiUn.Layout += OnLayout;
        }
        else
        {
            ImGuiUn.Layout -= OnLayout;
        }
        bShowWindow = !bShowWindow;
    }

    void OnLayout()
    {
        ImGui.ShowDemoWindow();
    }


    // 处理输入
    public void DealInput(string content)
    {
        Debug.Log("content");
    }

    public void ButtonTestKeyboardClick()
    {
        Debug.Log("button is click");
        keyboard = new TouchScreenKeyboard("", TouchScreenKeyboardType.Default, false,false,false,false,"",1024);
        

    }

    // 因为在 android 上点击键盘外边，会收起但是不会被判定为 cancel
    // 所以补一个键盘失去焦点的判断
    // 2018.4.19 是这样，后边不清楚有没有改
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            if (keyboard != null && keyboard.status == TouchScreenKeyboard.Status.Visible)
            {
                keyboard.active = false;
                lastInput = keyboard.text;
                keyboard = null;
            }
        }
    }
}
