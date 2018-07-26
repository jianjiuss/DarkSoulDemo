using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton 
{
    public bool isPressing = false;
    public bool onPressed = false;
    public bool onReleased = false;

    private bool curState = false;
    private bool lastState = false;

    public void Tick(bool input)
    {
        curState = input;

        isPressing = curState;

        onPressed = false;
        onReleased = false;
        if(curState != lastState)
        {
            if(curState)
            {
                onPressed = true;
            }
            else
            {
                onReleased = true;
            }
        }

        lastState = curState;
    }
}
