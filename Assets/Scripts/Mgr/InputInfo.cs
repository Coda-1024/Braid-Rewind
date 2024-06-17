using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInfo
{
    public enum E_InputType
    {
        Down,
        Up,
        Always
    }

    public enum E_KeyOrMouse
    { 
        Key,
        Mouse
    }

    public KeyCode key;

    public int mouseID;
    
    public E_KeyOrMouse keyOrMouse;

    public E_InputType inputType;

    public InputInfo(E_InputType inputType, KeyCode key)
    {
        keyOrMouse = E_KeyOrMouse.Key;
        this.inputType = inputType;
        this.key = key;
    }

    public InputInfo(E_InputType inputType, int mouseID)
    {
        keyOrMouse = E_KeyOrMouse.Mouse;
        this.inputType = inputType;
        this.mouseID = mouseID;
    }



}
