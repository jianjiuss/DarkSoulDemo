﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private DataBase weaponDB;

    private void Awake()
    {
        CheckGameObject();
        CheckSingle();
    }

    private void Start()
    {
        InitWeaponDB();
        print(weaponDB.weaponDataBase);
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
