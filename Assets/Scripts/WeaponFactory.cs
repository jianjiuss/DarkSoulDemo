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

    public GameObject CreateWeapon(string weaponName, Transform parent)
    {
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject go = GameObject.Instantiate(prefab, parent);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;

        var weaponDataComponent = go.AddComponent<WeaponData>();
        weaponDataComponent.atk = weaponData.weaponDataBase[weaponName]["ATK"].f;

        return go;
    }
}
