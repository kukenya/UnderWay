using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatItem : MonoBehaviour
{
    public float bbabbaroPlusHungry;
    public float triangleGimbabPlusHungry;
    public float breadPlusHungry;
    //public ItemContainer itemContainer;

    public EatItemType eatItemType = new EatItemType();
    public enum EatItemType {None, Bbabbaro, TriangleGimbab ,Bread}

    public void Eat()
    {
        switch (eatItemType)
        {
            case EatItemType.Bbabbaro:
                DataManager.instance.nowPlayer.hungry += bbabbaroPlusHungry;
                break;
            case EatItemType.TriangleGimbab:
                DataManager.instance.nowPlayer.hungry += triangleGimbabPlusHungry;
                break;
            case EatItemType.Bread:
                DataManager.instance.nowPlayer.hungry += breadPlusHungry;
                break;
            default:
                break;
        }
        Destroy(this.gameObject);
    } 
}
