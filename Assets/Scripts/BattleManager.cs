﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class BattleManager : IActorManager
{
    private CapsuleCollider defCol;

    private void Start()
    {
        defCol = GetComponent<CapsuleCollider>();
        defCol.center = Vector3.up * 1.0f;
        defCol.height = 2.0f;
        defCol.radius = 0.35f;
        defCol.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Weapon"))
        {
            am.TryDoDamage();
        }
    }
}
