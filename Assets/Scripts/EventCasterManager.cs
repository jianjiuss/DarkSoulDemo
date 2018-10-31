﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManager
{
    public string eventName;
    public bool active;

    public void Start()
    {
        if(am == null)
        {
            am = GetComponentInParent<ActorManager>();
        }
    }
}
