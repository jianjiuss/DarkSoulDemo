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

        if (am != null)
        {
            am.ac.OnStunnedEnterEvent += Ac_OnStunnedEnterEvent;
            am.ac.OnGroundEnterEvent += Ac_OnGroundEnterEvent;
        }
    }

    private void Ac_OnGroundEnterEvent()
    {
        if(eventName.Equals("frontStab"))
        {
            active = false;
        }
    }

    private void Ac_OnStunnedEnterEvent()
    {
        if(eventName.Equals("frontStab"))
        {
            active = true;
        }
    }
}
