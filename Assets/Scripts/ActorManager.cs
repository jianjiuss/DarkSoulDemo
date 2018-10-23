using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;

	void Awake ()
    {
        ac = GetComponent<ActorController>();
        GameObject model = ac.model;
        GameObject sensor = transform.Find("sensor").gameObject;

        bm = Bind<BattleManager>(sensor);
        wm = Bind<WeaponManager>(model);
        sm = Bind<StateManager>(gameObject);
	}

    private T Bind<T>(GameObject go) where T : IActorManager
    {
        T tempIns;
        tempIns = go.GetComponent<T>();
        if(tempIns == null)
        {
            tempIns = go.AddComponent<T>();
        }
        tempIns.am = this;
        return tempIns;
    }
	
	void Update ()
    {
		
	}

    public void TryDoDamage()
    {
        //if(sm.HP > 0)
        //{
        //    sm.AddHP(-5);
        //}

        if(sm.isImmortal)
        {
            //Do nothing
        }
        else if(sm.isDefense)
        {
            // attack should be blocked
            Blocked();
        }
        else
        {
            if(sm.HP <= 0)
            {
                // already dead
            }
            else
            {
                sm.AddHP(-5);
                if (sm.HP > 0)
                {
                    Hit();
                }
                else
                {
                    Die();
                }
            }
        }
    }

    public void Blocked()
    {
        ac.IssueTrigger("blocked");
    }

    public void Hit()
    {
        ac.IssueTrigger("hit");
    }

    public void Die()
    {
        ac.IssueTrigger("die");
        ac.pi.inputEnable = false;
        if(ac.camcon.lockState)
        {
            ac.camcon.LockUnLock();
        }
        ac.camcon.enabled = false;
    }
}
