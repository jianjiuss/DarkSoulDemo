using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour 
{
    public GameObject model;
    public IUserInput pi;
    public float movingSpeed = 1.4f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity;
    public float rollVelocity = 3.0f;
    public float rollMag = 1.0f;

    [Space(10)]
    [Header("===== Friction Setting =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;
    private bool canAttack = true;
    private bool lockPlanar = false;
    private CapsuleCollider col;
    private float lerpTarget;
    private Vector3 deltaPos;
    private bool isAddRollVeloctiy = true;

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
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (pi.run ? 2f : 1f), 0.5f));
        anim.SetBool("defense", pi.defense);
        if (rigid.velocity.magnitude > rollMag)
        {
            anim.SetTrigger("roll");
        }

        if(pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if(pi.attack && CheckState("ground") && canAttack)
        {
            anim.SetTrigger("attack");
        }

        if(pi.Dmag > 0.1f)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f);
        }

        if(!lockPlanar)
        {
            planarVec = pi.Dmag * model.transform.forward * (pi.run ? runMultiplier : 1f) * movingSpeed;
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

    private bool CheckState(string stateName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }


    //Messenger=======================================================================================
    public void OnJumpEnter()
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        pi.inputEnable = false;
        lockPlanar = true;
        isAddRollVeloctiy = false;
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
    }
    public void ExitGround()
    {
        col.material = frictionZero;
    }

    public void OnFailEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
        isAddRollVeloctiy = false;
    }

    public void OnRollEnter()
    {
        //thrustVec = model.transform.forward * rollVelocity;
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnRollUpdate()
    {
        if(isAddRollVeloctiy)
        {
            thrustVec = model.transform.forward * anim.GetFloat("rollVelocity");
        }
    }

    public void OnRollExit()
    {
        isAddRollVeloctiy = true;
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
        lerpTarget = 1.0f;
        pi.inputEnable = false;
    }

    public void OnAttack1hAUpdate()
    {
        int index = anim.GetLayerIndex("Attack");
        anim.SetLayerWeight(index, Mathf.Lerp(anim.GetLayerWeight(index), lerpTarget, 0.3f));
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
    }

    public void OnAttackIdleEnter()
    {
        pi.inputEnable = true;
        lerpTarget = 0;
    }

    public void OnAttackIdleUpdate()
    {
        int index = anim.GetLayerIndex("Attack");
        anim.SetLayerWeight(index, Mathf.Lerp(anim.GetLayerWeight(index), lerpTarget, 0.3f));
    }

    public void OnUpdateRM(object deltaPos)
    {
        if (CheckState("attack1hC", "Attack"))
        {
            this.deltaPos += (this.deltaPos * 0.2f + (Vector3)deltaPos * 0.8f);
        }
    }
}
