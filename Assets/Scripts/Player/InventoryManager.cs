using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public ItemContainer itemContainer;

    public static InventoryManager instance;

    public Image itemInfo;
    public List<Sprite> itemInfos = new List<Sprite>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void OnItemInfo(Item item)
    {
        switch (item.type)
        {
            case ItemType.Default: itemInfo.sprite = null; break;
            case ItemType.FlashLight: itemInfo.sprite = itemInfos[0]; break;
            case ItemType.Lighter: itemInfo.sprite = itemInfos[1]; break;
            case ItemType.Radio: itemInfo.sprite = itemInfos[2]; break;
            case ItemType.Key: itemInfo.sprite = itemInfos[3]; break;
            case ItemType.Cola: itemInfo.sprite = itemInfos[4]; break;
            case ItemType.Sprite: itemInfo.sprite = itemInfos[5]; break;
            case ItemType.Amond: itemInfo.sprite = itemInfos[6]; break;
            case ItemType.NormalSnack: itemInfo.sprite = itemInfos[7]; break;
            case ItemType.NudeChoko: itemInfo.sprite = itemInfos[8]; break;
            case ItemType.Bread: itemInfo.sprite = itemInfos[9]; break;
            case ItemType.SamGak1: itemInfo.sprite = itemInfos[10]; break;
            case ItemType.SamGak2: itemInfo.sprite = itemInfos[11]; break;
            case ItemType.Medicine: itemInfo.sprite = itemInfos[12]; break;
            case ItemType.Vitamine: itemInfo.sprite = itemInfos[13]; break;
            case ItemType.Pocari: itemInfo.sprite = itemInfos[14]; break;
            case ItemType.StoreKey: itemInfo.sprite = itemInfos[15]; break;
            case ItemType.CafeKey: itemInfo.sprite = itemInfos[16]; break;
            case ItemType.DrugKey: itemInfo.sprite = itemInfos[17]; break;
            case ItemType.Fuse: itemInfo.sprite = itemInfos[18]; break;
        }
    }
}
