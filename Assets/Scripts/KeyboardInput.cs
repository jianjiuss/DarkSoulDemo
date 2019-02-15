using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput 
{
    [Header("===== Key Setting =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;
    public string keyE;
    public string keyF;

    public string keyJRight = "right";
    public string keyJLeft = "left";
    public string keyJUp = "up";
    public string keyJDown = "down";

    [Header("===== Mouse Settings =====")]
    public bool mouseEnable = false;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;

    private MyButton buttonA = new MyButton();
    private MyButton buttonB = new MyButton();
    private MyButton buttonC = new MyButton();
    private MyButton buttonD = new MyButton();
    private MyButton buttonE = new MyButton();
    private MyButton buttonF = new MyButton();

	void Update () 
    {
        buttonA.Tick(Input.GetKey(keyA));
        buttonB.Tick(Input.GetKey(keyB));
        buttonC.Tick(Input.GetKey(keyC));
        buttonD.Tick(Input.GetKey(keyD));
        buttonE.Tick(Input.GetKey(keyE));
        buttonF.Tick(Input.GetKey(keyF));

        if(mouseEnable)
        {
            Jup = Input.GetAxis("Mouse Y") * mouseSensitivityY * 3f;
            Jright = Input.GetAxis("Mouse X") * mouseSensitivityX * 2.5f;
        }
        else
        {
            Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
            Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);
        }

        targetDup = (Input.GetKey(keyUp) ? 1f : 0) - (Input.GetKey(keyDown) ? 1f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1f : 0) - (Input.GetKey(keyLeft) ? 1f : 0);

        if(!inputEnable)
        {
            targetDup = 0;
            targetDright = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float dright2 = tempDAxis.x;
        float dup2 = tempDAxis.y;
        
        UpdateDmagDvec(dup2, dright2);

        roll = buttonA.onReleased && buttonA.isDelaying;
        run = (buttonA.isPressing && !buttonA.isDelaying) || buttonA.isExtending;
        jump = buttonA.onPressed && buttonA.isExtending;
        defense = buttonD.isPressing;
        //attack = buttonC.onPressed;
        action = buttonC.onPressed;
        rb = !buttonB.isPressing && buttonC.onPressed;
        lb = buttonB.isPressing && buttonC.onPressed;
        lockon = buttonE.onPressed;
        dualWielding = buttonF.onPressed;
	}

}
