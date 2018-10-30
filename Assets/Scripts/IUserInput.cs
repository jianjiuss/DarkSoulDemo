using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour 
{

    [Header("===== Output Signals =====")]
    public float Dup;
    public float Dright;
    //控制摇杆类比力度
    public float Dmag;
    //控制摇杆指向
    public Vector3 Dvec;
    public float Jup;
    public float Jright;

    //1. pressing signal
    public bool run;
    public bool defense;
    //2. trigger once signal
    public bool lockon;
    public bool jump;
    public bool action;
    //public bool attack;
    public bool roll;
    public bool lb;
    public bool lt;
    public bool rb;
    public bool rt;
    //3. double trigger

    [Header("===== Others =====")]
    public bool inputEnable = true;

    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;

    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }

    protected void UpdateDmagDvec(float Dup2, float Dright2)
    {
        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = transform.right * Dright2 + transform.forward * Dup2;
    }
}
