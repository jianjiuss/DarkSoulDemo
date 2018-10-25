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

    public void SetIsCounterBack(bool value)
    {
        sm.isCounterBackEnable = value;
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

    public void TryDoDamage(WeaponController wc, bool attackValid, bool counterValid)
    {
        //if(sm.HP > 0)
        //{
        //    sm.AddHP(-5);
        //}
        if(sm.isCounterBackSuccess)
        {
            if(counterValid)
                wc.wm.am.Stunned();
        }
        else if(sm.isCounterBackFailure)
        {
            if(attackValid)
                HitOrDie(false);
        }
        else if(sm.isImmortal)
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
            if(attackValid)
                HitOrDie(true);
        }
    }

    public void HitOrDie(bool doHitAni)
    {
        if (sm.HP <= 0)
        {
            // already dead
        }
        else
        {
            sm.AddHP(-5);
            if (sm.HP > 0)
            {
                if(doHitAni)
                {
                    Hit();
                }
                //do some vfx (blood)
            }
            else
            {
                Die();
            }
        }
    }

    public void Stunned()
    {
        ac.IssueTrigger("stunned");
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

    public void OnCounterBackExit()
    {
        SetIsCounterBack(false);
    }
}
