using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour 
{
    public GameObject model;
    public IUserInput pi;
    public CameraController camcon;
    public float movingSpeed = 1.4f;
    public float runMultiplier = 2.0f;
    //跳跃时向上增加的推力
    public float jumpVelocity;
    //滚动时向上增加的推力
    public float rollVelocity = 3.0f;
    //滚动时候向前增加的推力
    public int rollForward = 5;
    //落地时多大的冲力会触发滚动
    public float rollMag = 1.0f;

    [Space(10)]
    [Header("===== Friction Setting =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Animator anim;
    private Rigidbody rigid;
    //平面移动向量
    private Vector3 planarVec;
    //推力
    private Vector3 thrustVec;
    private bool canAttack = true;
    //是否锁定平面移动向量
    private bool lockPlanar = false;
    //当镜头锁定目标时是否锁定角色指向
    private bool trackDirection = false;
    private CapsuleCollider col;
    //private float lerpTarget;
    //动画位置修正
    private Vector3 deltaPos;
    //翻滚时推力
    //private bool isAddRollVeloctiy = true;

    [SerializeField]
    private float velocityMag;

    public bool isLeftShield;

	void Awake () 
    {
        pi = GetComponent<IUserInput>();
        IUserInput[] inputs = GetComponents<IUserInput>();
        foreach(var input in inputs)
        {
            if(input.enabled)
            {
                pi = input;
                break;
            }
        }
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
	}
	
	void Update () 
    {
        if (pi.lockon)
        {
            camcon.LockUnLock();
        }

        if(!camcon.lockState)
        {
            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (pi.run ? 2f : 1f), 0.1f));
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localDVec = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("forward", localDVec.z * (pi.run ? 2f : 1f));
            anim.SetFloat("right", localDVec.x * (pi.run ? 2f : 1f));
        }

        velocityMag = rigid.velocity.magnitude;

        if (pi.roll || rigid.velocity.magnitude > rollMag)
        {
            anim.SetTrigger("roll");
            canAttack = false;
        }

        if(pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if((pi.rb || pi.lb) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack)
        {
            if (pi.rb)
            {
                anim.SetBool("R0L1", false);
                anim.SetTrigger("attack");
            }
            else if (pi.lb && !isLeftShield)
            {
                anim.SetBool("R0L1", true);
                anim.SetTrigger("attack");
            }
            else if (pi.lb && isLeftShield)
            {
                anim.SetTrigger("counterBack");
            }
        }

        if((CheckState("blocked") || CheckState("ground")) && isLeftShield)
        {
            if(pi.defense)
            {
                anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 1);
                anim.SetBool("defense", true);
            }
            else
            {
                anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 0);
                anim.SetBool("defense", false);
            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Defense"), 0);
            anim.SetBool("defense", false);
        }
        

        if (!camcon.lockState)
        {
            if (pi.Dmag > 0.1f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
            }

            if (!lockPlanar)
            {
                planarVec = pi.Dmag * model.transform.forward * (pi.run ? runMultiplier : 1f) * movingSpeed;
            }
        }
        else
        {
            if(!trackDirection)
            {
                model.transform.forward = transform.forward;
            }
            else
            {
                model.transform.forward = planarVec.normalized;
            }

            if(!lockPlanar)
            {
                planarVec = pi.Dvec * movingSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
        }
	}
    

    void FixedUpdate()
    {
        //rigid.position += planarVec * Time.fixedDeltaTime * movingSpeed;
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }

    public bool CheckState(string stateName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }

    public bool CheckStateTag(string stateTag, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(stateTag);
    }


    //Messenger=======================================================================================
    public void OnJumpEnter()
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        pi.inputEnable = false;
        lockPlanar = true;
        //isAddRollVeloctiy = false;
        trackDirection = true;
    }

    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    public void EnterGround()
    {
        pi.inputEnable = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }
    public void ExitGround()
    {
        col.material = frictionZero;
    }

    public void OnFailEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
        //isAddRollVeloctiy = false;
    }

    public void OnRollEnter()
    {
        planarVec = planarVec.normalized * rollForward;
        //thrustVec = model.transform.forward * rollVelocity;
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.inputEnable = false;
        lockPlanar = true;
        trackDirection = true;
    }

    public void OnRollUpdate()
    {
        //if (isAddRollVeloctiy)
        //{
        //    thrustVec = model.transform.forward * anim.GetFloat("rollVelocity");
        //}
    }

    public void OnRollExit()
    {
        //isAddRollVeloctiy = true;
    }

    public void OnJabEnter()
    {
        //thrustVec = new Vector3(0, jumpVelocity, 0);
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");
    }

    public void OnAttack1hAEnter()
    {
        //lerpTarget = 1.0f;
        pi.inputEnable = false;
    }

    public void OnAttack1hAUpdate()
    {
        int index = anim.GetLayerIndex("Attack");
        //anim.SetLayerWeight(index, Mathf.Lerp(anim.GetLayerWeight(index), lerpTarget, 0.3f));
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
    }

    public void OnUpdateRM(object deltaPos)
    {
        if (CheckState("attack1hC"))
        {
            this.deltaPos += (this.deltaPos * 0.2f + (Vector3)deltaPos * 0.8f);
        }
    }

    public void OnHitEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");
        //print("On Attack Exit");
    }

    public void OnBlockedEnter()
    {
        pi.inputEnable = false;
    }

    public void OnDieEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }

    public void OnStunnedEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }

    public void OnCounterBackEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }
}
