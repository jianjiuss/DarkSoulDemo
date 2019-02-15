using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : MonoBehaviour
{
    public ActorController ac;

    [Header("=== Auto Generate if Null ===")]
    public BattleManager bm;
    public WeaponManager wm;
    public StateManager sm;
    public DirectorManager dm;
    public InterActionManager im;

    [Header("=== Override Animator ===")]
    public AnimatorOverrideController oneHandAnimator;
    public AnimatorOverrideController TwoHandAnimator;

	void Awake ()
    {
        ac = GetComponent<ActorController>();
        GameObject model = ac.model;

        if(!ac.isTrigger)
        {
            GameObject sensor = transform.Find("sensor").gameObject;
            bm = Bind<BattleManager>(sensor);
            im = Bind<InterActionManager>(sensor);
            wm = Bind<WeaponManager>(model);
        }

        sm = Bind<StateManager>(gameObject);
        dm = Bind<DirectorManager>(gameObject);

        ac.OnAction += DoAction;
        ac.OnChangeDualHand += Ac_OnChangeDualHand;
	}

    private void Ac_OnChangeDualHand()
    {
        if(ac.anim.runtimeAnimatorController.name.Equals(oneHandAnimator.name))
        {
            ChangedDualHands(true);
        }
        else
        {
            ChangedDualHands(false);
        }
    }

    private void DoAction()
    {
        if(im.overlapEcastms.Count != 0)
        {
            if(im.overlapEcastms[0].active && !dm.IsPlaying())
            {
                if(im.overlapEcastms[0].eventName == "frontStab")
                {
                    if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 90))
                    {
                        var targetPos = im.overlapEcastms[0].am.gameObject.transform.position;
                        transform.position = targetPos + im.overlapEcastms[0].transform.forward * im.overlapEcastms[0].offset;
                        ac.SetLockForward(targetPos);
                        dm.PlayFrontStab(this, im.overlapEcastms[0].am);
                    }
                }
                else if (im.overlapEcastms[0].eventName == "openBox")
                {
                    if(BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject,90 ))
                    {
                        //im.overlapEcastms[0].active = false;
                        var targetPos = im.overlapEcastms[0].am.gameObject.transform.position;
                        transform.position = targetPos + im.overlapEcastms[0].transform.forward * im.overlapEcastms[0].offset;
                        ac.SetLockForward(targetPos);

                        dm.PlayOpenBox(this, im.overlapEcastms[0].am);
                    }
                }
                else if(im.overlapEcastms[0].eventName == "lever")
                {
                    if (BattleManager.CheckAnglePlayer(ac.model, im.overlapEcastms[0].am.gameObject, 90))
                    {
                        //im.overlapEcastms[0].active = false;
                        var targetPos = im.overlapEcastms[0].am.gameObject.transform.position;
                        transform.position = targetPos + im.overlapEcastms[0].transform.forward * im.overlapEcastms[0].offset;
                        ac.SetLockForward(targetPos);

                        dm.PullLever(this, im.overlapEcastms[0].am);
                    }
                }
            }
        }
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
                HitOrDie(wc, false);
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
                HitOrDie(wc, true);
        }
    }

    public void HitOrDie(WeaponController targetWc, bool doHitAni)
    {
        if (sm.HP <= 0)
        {
            // already dead
        }
        else
        {
            sm.AddHP(-1 * targetWc.GetATK());
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

    public void LockActorController(bool value)
    {
        if(ac != null)
        {
            ac.SetBool("lock", value);
        }
    }

    public void ChangedDualHands(bool dualOn)
    {
        if(dualOn)
        {
            wm.UnloadWeapon("L");
            ac.anim.runtimeAnimatorController = TwoHandAnimator;
        }
        else
        {
            wm.UpdateWeaponCollider("L", GameManager.Ins.WeaponFactory.CreateWeapon("Shield", "L", wm));
            ac.anim.runtimeAnimatorController = oneHandAnimator;
        }
    }
}
