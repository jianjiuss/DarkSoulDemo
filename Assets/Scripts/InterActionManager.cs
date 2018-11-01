using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActionManager : IActorManager
{
    private CapsuleCollider interCol;

    public List<EventCasterManager> overlapEcastms = new List<EventCasterManager>();

	void Start ()
    {
        interCol = GetComponent<CapsuleCollider>();
	}

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("stay");
        EventCasterManager[] ecastms = other.GetComponents<EventCasterManager>();
        foreach(var ecastm in ecastms)
        {
            if(!overlapEcastms.Contains(ecastm))
            {
                overlapEcastms.Add(ecastm);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EventCasterManager[] ecastms = other.GetComponents<EventCasterManager>();
        foreach (var ecastm in ecastms)
        {
            if (overlapEcastms.Contains(ecastm))
            {
                overlapEcastms.Remove(ecastm);
            }
        }
    }
}
