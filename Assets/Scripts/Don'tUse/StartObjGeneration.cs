using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartObjGeneration : MonoBehaviour
{
    public GameObject snack;
    public GameObject stake;

    public bool[] item = new bool[20];

    private void Start()
    {
        GetItemBoolForSave();
        GenerateItem();
    }

    private void GenerateItem()
    {
        for(int i = 0; i < item.Length; i++)
        {

        }
    }

    private void GetItemBoolForSave()
    {
        for (int i = 0; i < item.Length; i++)
        {
            item[i] = DataManager.instance.nowPlayer.item[i];
        }
    }
}
