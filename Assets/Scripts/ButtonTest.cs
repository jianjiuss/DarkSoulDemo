using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour 
{
    public string btn;

    private MyButton button = new MyButton();
    private int counter = 0;

    void Update()
    {
        counter++;

        button.Tick(Input.GetKey(btn));

        if(button.onPressed)
        {
            print("Btn OnPressed" + counter);
        }

        if(button.isPressing)
        {
            print("Btn IsPressing" + counter);
        }

        if(button.onReleased)
        {
            print("Btn OnReleased" + counter);
        }

        if(button.isDelaying)
        {
            print("Btn IsDelaying" + counter);
        }

        if(button.isExtending)
        {
            print("Btn IsExtending" + counter);
        }
    }
}
