using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : IActorManager
{

    private Collider weaponColL;
    private Collider weaponColR;
    private GameObject whL;
    private GameObject whR;

    public WeaponController wcL;
    public WeaponController wcR;

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

        wcL = BindWeaponController(weaponColL.gameObject);
        wcR = BindWeaponController(weaponColR.gameObject);
    }

    public WeaponController BindWeaponController(GameObject targetObj)
    {
        WeaponController tempWc;
        tempWc = targetObj.GetComponent<WeaponController>();
        if(tempWc == null)
        {
            tempWc = targetObj.AddComponent<WeaponController>();
        }
        tempWc.wm = this;
        return tempWc;
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

    public void CounterBackEnable()
    {
        am.SetIsCounterBack(true);
    }

    public void CounterBackDisable()
    {
        am.SetIsCounterBack(false);
    }
} 
