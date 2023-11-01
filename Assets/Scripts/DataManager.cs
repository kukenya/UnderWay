using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class PlayerData
{
    public string name;
    public float hungry = 100f;
    public float thirst = 100f;
    public float mentality = 100f;
    public bool[] item = new bool[20];
    public string recentPlayDate = null;

    public bool storeKey = false;
    public bool cafeKey = false;
    public bool drugKey = false;
    public bool underStoreKey = false;
    public bool hasBackpack = false;
    public bool hasFlashLIght = false;

    public int flashLightBattery = 1;
    public int eatPenaltyMedicine = 0;

    public int fuse = 0;

    public bool interactFirstKeypad = true;

    public Vector3 playerLastPos = new Vector3(62.75f, 2.43700004f, 288.040009f);
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

