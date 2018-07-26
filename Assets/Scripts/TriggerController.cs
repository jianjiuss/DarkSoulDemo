﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour 
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ResetTrigger(string name)
    {
        anim.ResetTrigger(name);
    }
}
