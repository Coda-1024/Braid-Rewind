using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputMgr : BaseManager<InputMgr>
{
    private Dictionary<E_EventType, InputInfo> inputDic = new Dictionary<E_EventType, InputInfo>();

    private bool isStart = true;

    private InputInfo nowInputInfo;

    public InputMgr()
    {
        MonoMgr.Instance.AddUpdateAction(UpdateAction);
    }

    public void OpenOrCloseInput(bool isStart)
    {
        this.isStart = isStart;
    }

    //初始化和改键
    public void ChangeKeyboardInfo(E_EventType eventType, KeyCode key, InputInfo.E_InputType inputType)
    {
        if(!inputDic.ContainsKey(eventType))
        {
            inputDic.Add(eventType, new InputInfo(inputType, key));
        }
        else
        {
            inputDic[eventType].key = key;
            inputDic[eventType].inputType = inputType;
            inputDic[eventType].keyOrMouse = InputInfo.E_KeyOrMouse.Key;
        }
    }


    public void ChangeMouseInfo(E_EventType eventType, int mouseID, InputInfo.E_InputType inputType)
    {
        if (!inputDic.ContainsKey(eventType))
        {
            inputDic.Add(eventType, new InputInfo(inputType, mouseID));
        }
        else
        {
            inputDic[eventType].mouseID = mouseID;
            inputDic[eventType].inputType = inputType;
            inputDic[eventType].keyOrMouse = InputInfo.E_KeyOrMouse.Mouse;
        }
    }

    //移除
    public void RemoveInputInfo(E_EventType eventType)
    {
        if(inputDic.ContainsKey(eventType))
        {
            inputDic.Remove(eventType);
        }
    }


    private void UpdateAction()
    {
        if (!isStart) return;


        foreach(E_EventType eventType in inputDic.Keys)
        {
            nowInputInfo = inputDic[eventType];

            if(nowInputInfo.keyOrMouse == InputInfo.E_KeyOrMouse.Key)
            {
                switch (nowInputInfo.inputType)
                {
                    case InputInfo.E_InputType.Down:
                        if(Input.GetKeyDown(nowInputInfo.key))
                            EventCenter.Instance.EventTrigger(eventType);
                        break;
                    case InputInfo.E_InputType.Up:
                        if (Input.GetKeyUp(nowInputInfo.key))
                            EventCenter.Instance.EventTrigger(eventType);
                        break;
                    case InputInfo.E_InputType.Always:
                        if (Input.GetKey(nowInputInfo.key))
                            EventCenter.Instance.EventTrigger(eventType);
                        break;
                }
            }
            else
            {
                switch (nowInputInfo.inputType)
                {
                    case InputInfo.E_InputType.Down:
                        if (Input.GetMouseButtonDown(nowInputInfo.mouseID))
                            EventCenter.Instance.EventTrigger(eventType);
                        break;
                    case InputInfo.E_InputType.Up:
                        if (Input.GetMouseButtonUp(nowInputInfo.mouseID))
                            EventCenter.Instance.EventTrigger(eventType);
                        break;
                    case InputInfo.E_InputType.Always:
                        if (Input.GetMouseButton(nowInputInfo.mouseID))
                            EventCenter.Instance.EventTrigger(eventType);
                        break;
                }
            }
        }

        EventCenter.Instance.EventTrigger<float>(E_EventType.E_Input_Horizontal, Input.GetAxisRaw("Horizontal"));
        EventCenter.Instance.EventTrigger<float>(E_EventType.E_Input_Vertical, Input.GetAxisRaw("Vertical"));


    }
}