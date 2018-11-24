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

    private void Awake()
    {
        Transform weaponHandleLTrans = transform.DeepFind("weaponHandleL");
        whL = weaponHandleLTrans == null ? null : weaponHandleLTrans.gameObject;
        Transform weaponHandleRTrans = transform.DeepFind("weaponHandleR");
        whR = weaponHandleRTrans == null ? null : weaponHandleRTrans.gameObject;

        weaponColR = whR.GetComponentInChildren<Collider>();
        weaponColL = whL.GetComponentInChildren<Collider>();

        SetEnable(weaponColR, false);
        SetEnable(weaponColL, false);

        wcL = BindWeaponController(whL);
        wcR = BindWeaponController(whR);
    }

    public void UpdateWeaponCollider(string side, Collider col)
    {
        if(side == "L")
        {
            weaponColL = col;
        }
        else if(side == "R")
        {
            weaponColR = col;
        }
    }

    public void UnloadWeapon(string side)
    {
        if(side == "L")
        {
            weaponColL = null;
            wcL.wdata = null;
            foreach(Transform trans in whL.transform)
            {
                Destroy(trans.gameObject);
            }
        }
        else if(side == "R")
        {
            weaponColR = null;
            wcR.wdata = null;
            foreach(Transform trans in whR.transform)
            {
                Destroy(trans.gameObject);
            }
        }
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
            SetEnable(weaponColR, true);
            //print("WeaponR Enable");
        }
        else
        {
            SetEnable(weaponColL, true);
            //print("WeaponL Enable");
        }
    }

    public void WeaponDisable()
    {
        SetEnable(weaponColR, false);
        SetEnable(weaponColL, false);

        //print("WeaponR And WeaponL Disable");
    }

    private void SetEnable(Collider collider, bool enabled)
    {
        if(collider != null)
        {
            collider.enabled = enabled;
        }
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
