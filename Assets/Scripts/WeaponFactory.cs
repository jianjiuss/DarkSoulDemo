using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{
    private DataBase weaponData;

    public WeaponFactory(DataBase _weaponData)
    {
        weaponData = _weaponData;
    }

    public GameObject CreateWeapon(string weaponName, Vector3 pos, Quaternion rot)
    {
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject go = GameObject.Instantiate(prefab, pos, rot);

        var weaponDataComponent = go.AddComponent<WeaponData>();
        weaponDataComponent.atk = weaponData.weaponDataBase[weaponName]["ATK"].f;

        return go;
    }

    public Collider CreateWeapon(string weaponName, string side,WeaponManager wm)
    {
        WeaponController wc;
        if(side == "L")
        {
            wc = wm.wcL;
        }
        else if(side == "R")
        {
            wc = wm.wcR;
        }
        else
        {
            return null;
        }

        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject go = GameObject.Instantiate(prefab);
        go.transform.parent = wc.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;

        var weaponDataComponent = go.AddComponent<WeaponData>();
        weaponDataComponent.atk = weaponData.weaponDataBase[weaponName]["ATK"].f;

        wc.wdata = weaponDataComponent;

        return go.GetComponentInChildren<Collider>();
    }
}
