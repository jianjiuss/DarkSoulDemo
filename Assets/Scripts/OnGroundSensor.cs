﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour 
{
    public CapsuleCollider capcol;
    public float offset = 0.1f;

    private Vector3 point1;
    private Vector3 point2;
    private float radius;

	void Awake () 
    {
        radius = capcol.radius - 0.3f;
	}



	void Update () 
    {
        point1 = transform.position + transform.up * (radius - offset);
        point2 = transform.position + transform.up * (capcol.height - offset) - transform.up * radius;

        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius, LayerMask.GetMask("Ground"));
        if(outputCols.Length != 0)
        {
            gameObject.SendMessageUpwards("IsGround");
        }
        else
        {
            gameObject.SendMessageUpwards("IsNotGround");
        }
	}
}
