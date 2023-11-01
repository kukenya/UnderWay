using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //Singleton instance of this script.
    public static ItemManager Instance { get; private set; }

    //List of all the item scriptable obects.
    //Either assign the items manually when created or select the item scriptable object > right click > select Add To Item List 
    public List<Item> itemList = new List<Item>();
    public PlayerEquipItem playerEquipItem;

    private void Awake()
    {
        //Singleton logic
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion

        //Any code in awake should be after the singleton evaluation
    }

    //This function is called when the Use Item button is clicked from one of the inventory items.
    public void UseItem(ItemSlot slot)
    {
        if (slot.IsEmpty) return;

        //ADD CASES FOR CUSTOM ITEM TYPES AND CALL THE CORRESPONDING USE METHODS CREATED BELOW.
        //PASS IN slot AS A PARAMETER FOR THE METHOD.
        switch (slot.slotItem.type)
        {
            case ItemType.Default: break;
            case ItemType.FlashLight: FlashLightEquip(slot); break;
            case ItemType.Lighter: LighterEquip(slot); break;
            case ItemType.Radio: break;
            case ItemType.Key:  break;
            case ItemType.Cola:  EatCola(slot); break;
            case ItemType.Sprite: EatSprite(slot); break;
            case ItemType.Amond: EatAmond(slot); break;
            case ItemType.NormalSnack: EatNormalSnack(slot); break;
            case ItemType.NudeChoko: EatNudeChoco(slot); break;
            case ItemType.Bread: EatBread(slot); break;
            case ItemType.SamGak1: EatSamGak1(slot); break;
            case ItemType.SamGak2: EatSamGak2(slot); break;
            case ItemType.Medicine: ; EatMedicine(slot); break;
            case ItemType.Vitamine: EatVitamine(slot); break;
            case ItemType.Pocari: EatPocari(slot); break;
            case ItemType.Fuse: break;
            default: DefaultItemUse(slot); break;
        }
    }

    private void EatPocari(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.mentality += 1;
        DataManager.instance.nowPlayer.thirst += 20;
        slot.Remove(1);
    }

    private void LighterEquip(ItemSlot slot)
    {
        playerEquipItem.EquipLighter();
    }

    private void FlashLightEquip(ItemSlot slot)
    {
        playerEquipItem.EquipFlashLight();
        DataManager.instance.nowPlayer.flashLightBattery++;
    }
    
    private void EatCola(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.mentality += 1;
        DataManager.instance.nowPlayer.thirst += 12;
        slot.Remove(1);
    }

    private void EatSprite(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.mentality += 2;
        DataManager.instance.nowPlayer.thirst += 10;
        slot.Remove(1);
    }

    private void EatAmond(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.hungry += 5;
        DataManager.instance.nowPlayer.thirst -= 3;
        slot.Remove(1);
    }

    private void EatNormalSnack(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.hungry += 4;
        DataManager.instance.nowPlayer.thirst -= 2;
        slot.Remove(1);
    }

    private void EatNudeChoco(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.hungry += 3;
        DataManager.instance.nowPlayer.thirst -= 1;
        slot.Remove(1);
    }

    private void EatBread(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.hungry += 20;
        DataManager.instance.nowPlayer.thirst -= 15;
        slot.Remove(1);
    }

    private void EatSamGak1(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.hungry += 12;
        DataManager.instance.nowPlayer.thirst -= 8;
        slot.Remove(1);
    }

    private void EatSamGak2(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.hungry += 9;
        DataManager.instance.nowPlayer.thirst -= 5;
        slot.Remove(1);
    }

    private void EatMedicine(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.mentality += 20;
        DataManager.instance.nowPlayer.eatPenaltyMedicine++;
        slot.Remove(1);
    }

    private void EatVitamine(ItemSlot slot)
    {
        DataManager.instance.nowPlayer.mentality += 7;
        slot.Remove(1);
    }

    private void DefaultItemUse(ItemSlot slot)
    {
        
    }


    //Returns the item from itemList at index.
    public Item GetItemByIndex(int index)
    {
        return itemList[index];
    }

    //Returns the item from the itemList with the name.
    public Item GetItemByName(string name)
    {
        foreach (Item item in itemList) if (item.itemName == name) return item;
        return null;
    }

    //Returns the index of the passed in item on the itemList.
    //NOTE: Returns -1 if the item does not exist in the list and the item should be added to the list.
    public int GetItemIndex(Item item)
    {
        for (int i = 0; i < itemList.Count; i++) if (itemList[i] == item) return i;
        return -1;
    }
}