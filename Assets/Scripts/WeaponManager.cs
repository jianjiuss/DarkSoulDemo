using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManager
{

    private Collider weaponColL;
    private Collider weaponColR;
    private GameObject whL;
    private GameObject whR;

    private void Start()
    {
        Transform weaponHandleLTrans = transform.DeepFind("weaponHandleL");
        whL = weaponHandleLTrans == null ? null : weaponHandleLTrans.gameObject;
        Transform weaponHandleRTrans = transform.DeepFind("weaponHandleR");
        whR = weaponHandleRTrans == null ? null : weaponHandleRTrans.gameObject;

        weaponColR = whR.GetComponentInChildren<Collider>();
        weaponColL = whL.GetComponentInChildren<Collider>();
        weaponColR.enabled = false;
        weaponColL.enabled = false;
    }


    public void WeaponEnable()
    {
        if (am.ac.CheckStateTag("attackR"))
        {
            weaponColR.enabled = true;
            //print("WeaponR Enable");
        }
        else
        {
            weaponColL.enabled = true;
            //print("WeaponL Enable");
        }
    }

    public void WeaponDisable()
    {
        weaponColR.enabled = false;
        weaponColL.enabled = false;

        //print("WeaponR And WeaponL Disable");
    }
} 
