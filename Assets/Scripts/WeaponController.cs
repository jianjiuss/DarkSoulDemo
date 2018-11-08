using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponManager wm;
    public WeaponData wdata;

    public void Awake()
    {
        wdata = GetComponentInChildren<WeaponData>();
    }

    public float GetATK()
    {
        return wdata.atk + wm.am.sm.ATK;
    }
}
