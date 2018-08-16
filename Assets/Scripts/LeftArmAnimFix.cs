using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour 
{
    private Animator anim;
    private ActorController ac;
    public Vector3 offset;


    void Awake()
    {
        anim = GetComponent<Animator>();
        ac = transform.parent.GetComponent<ActorController>();
    }

    void OnAnimatorIK()
    {
        if(!anim.GetBool("defense") && ac.isLeftShield)
        {
            Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            leftLowerArm.transform.localEulerAngles += 0.75f * offset;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
        }
    }
}
