using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WeaponManager wmTest;

    public static GameManager Ins
    {
        get
        {
            return instance;
        }
    }

    public WeaponFactory WeaponFactory
    {
        get
        {
            return weaponFactory;
        }
    }

    private static GameManager instance;
    private DataBase weaponDB;
    private WeaponFactory weaponFactory;

    private void Awake()
    {
        CheckGameObject();
        CheckSingle();
    }

    private void Start()
    {
        InitWeaponDB();
        InitWeaponFactory();

        Collider col = weaponFactory.CreateWeapon("Sword", "R", wmTest);
        wmTest.UpdateWeaponCollider("R", col);
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 150, 30), "R: Sword"))
        {
            wmTest.UnloadWeapon("R");
            wmTest.UpdateWeaponCollider("R", weaponFactory.CreateWeapon("Sword", "R", wmTest));
        }
        if (GUI.Button(new Rect(10, 50, 150, 30), "R: Falchion"))
        {
            wmTest.UnloadWeapon("R");
            wmTest.UpdateWeaponCollider("R", weaponFactory.CreateWeapon("Falchion", "R", wmTest));
        }
        if (GUI.Button(new Rect(10, 90, 150, 30), "R: Mace"))
        {
            wmTest.UnloadWeapon("R");
            wmTest.UpdateWeaponCollider("R", weaponFactory.CreateWeapon("Mace", "R", wmTest));
        }

        if(GUI.Button(new Rect(10, 130, 150, 30), "R: Clear allweapons"))
        {
            wmTest.UnloadWeapon("R");
        }
    }

    private void InitWeaponFactory()
    {
        weaponFactory = new WeaponFactory(weaponDB); 
    }

    private void InitWeaponDB()
    {
        weaponDB = new DataBase();
    }

    private void CheckSingle()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        
        Destroy(this);
    }

    private void CheckGameObject()
    {
        if(tag == "GM")
        {
            return;
        }
        Destroy(this);
    }
}
