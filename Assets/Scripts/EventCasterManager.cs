using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCasterManager : IActorManager
{
    public string eventName;
    public bool active;
    public float offset;

    public void Start()
    {
        if(am == null)
        {
            am = GetComponentInParent<ActorManager>();
        }
    }
}
