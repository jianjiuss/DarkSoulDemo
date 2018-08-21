using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;
    public BattleManager bm;

	void Awake ()
    {
        ac = GetComponent<ActorController>();
        GameObject sensor = transform.Find("sensor").gameObject;
        bm = sensor.GetComponent<BattleManager>();
        if(bm == null)
        {
            bm = sensor.AddComponent<BattleManager>();
        }
        bm.am = this;
	}
	
	void Update ()
    {
		
	}

    public void DoDamage()
    {
        ac.IssueTrigger("hit");
    }
}
