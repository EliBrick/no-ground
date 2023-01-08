using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockableInput : Input
{
    public static bool blockInput = false;
    
    public static new bool GetKeyDown(KeyCode keyCode)
    {
        if (blockInput) return false;
        else return Input.GetKeyDown(keyCode);
    }

    public static new float GetAxis(string axisName)
    {
        if (blockInput) return 0;
        else return Input.GetAxis(axisName);
    }

}
