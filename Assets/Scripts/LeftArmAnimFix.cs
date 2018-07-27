using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour 
{
    private Animator anim;
    public Vector3 offset;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if(!anim.GetBool("defense"))
        {
            Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            leftLowerArm.transform.localEulerAngles += 0.75f * offset;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
        }
        print(anim.GetBoneTransform(HumanBodyBones.LeftFoot).position);
    }
}
