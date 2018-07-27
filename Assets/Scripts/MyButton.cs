using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton 
{
    public bool isPressing = false;
    public bool onPressed = false;
    public bool onReleased = false;
    public bool isExtending = false;
    public bool isDelaying = false;

    private bool curState = false;
    private bool lastState = false;

    private MyTimer extTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();

    public void Tick(bool input)
    {
        extTimer.Tick();
        delayTimer.Tick();

        curState = input;

        isPressing = curState;

        onPressed = false;
        onReleased = false;

        isExtending = false;
        isDelaying = false;

        if(curState != lastState)
        {
            if(curState)
            {
                onPressed = true;
                StartTimer(delayTimer, 0.15f);
            }
            else
            {
                onReleased = true;
                StartTimer(extTimer, 0.15f);
            }
        }

        lastState = curState;

        if(extTimer.state == MyTimer.STATE.RUN)
        {
            isExtending = true;
        }

        if(delayTimer.state == MyTimer.STATE.RUN)
        {
            isDelaying = true;
        }
    }

    private void StartTimer(MyTimer timer, float duration)
    {
        timer.duration = duration;
        timer.Go();
    }
}
