using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkItem : MonoBehaviour
{
    public float waterPlusThirst;

    public float sodaPlusThirst;
    public float sodaPlusMentality;

    public float IonPlusThirst;

    public DrinkItemType drinkItemType = new DrinkItemType();
    public enum DrinkItemType {None, Water, Soda, Ion }

    public void Drink()
    {
        switch (drinkItemType)
        {
            case DrinkItemType.Water:
                DataManager.instance.nowPlayer.thirst += waterPlusThirst;
                break;
            case DrinkItemType.Soda:
                DataManager.instance.nowPlayer.thirst += sodaPlusThirst;
                DataManager.instance.nowPlayer.mentality += sodaPlusMentality;
                break;
            case DrinkItemType.Ion:
                DataManager.instance.nowPlayer.thirst += IonPlusThirst;
                break;
            default:
                break;
        }
        Destroy(this.gameObject);
    }
}
