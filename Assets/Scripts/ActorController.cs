using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour 
{
    public GameObject model;
    public PlayerInput pi;
    public float movingSpeed = 1.4f;
    public float runMultiplier = 2.0f;
    public float jumpVelocity;
    public float rollVelocity = 3.0f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 thrustVec;

    private bool lockPlanar = false;

	void Awake () 
    {
        pi = GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
	}
	
	void Update () 
    {
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), (pi.run ? 2f : 1f), 0.5f));
        if(rigid.velocity.magnitude > 0f)
        {
            anim.SetTrigger("roll");
        }

        if(pi.jump)
        {
            anim.SetTrigger("jump");
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
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;
    }


    //Messenger=======================================================================================
    public void OnJumpEnter()
    {
        thrustVec = new Vector3(0, jumpVelocity, 0);
        pi.inputEnable = false;
        lockPlanar = true;
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
    }

    public void OnFailEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.inputEnable = false;
        lockPlanar = true;
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
}
