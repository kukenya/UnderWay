using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ItemContainer : MonoBehaviour
{
    //public Interactor carrier;
    UnderWay inputActions;

    public bool dropRemovedItemPrefabs = true;

    private ItemSlot[] slots;
    public Transform containerUI;
    public Transform slotHolder;
    //public GameObject slotOptionsUI;

    //public Button itemUseButton;
    //public Button itemRemoveButton;

    public StoryGameManager storyGameManager;

    public Item item;

    private void Awake()
    {
        inputActions = new UnderWay();
        InitContainer();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        AddItem(item);
    }

    public void InitContainer()
    {
        slots = new ItemSlot[slotHolder.childCount];
        for (int i = 0; i < slots.Length; i++)
        {
            ItemSlot slot = slotHolder.GetChild(i).GetComponent<ItemSlot>();
            slots[i] = slot;
            //slot.GetComponent<Button>().onClick.AddListener(delegate { OnSlotClicked(slot); });
        }   
        containerUI.gameObject.SetActive(false);
    }

    //Returns true if it's able to add the item to the container.
    public bool AddItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Add(item)) return true;
        } 
        return false;
    }

    public bool ContainsItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
            if (slots[i].slotItem == item) return true;
        return false;
    }

    public bool ContainsItemQuantity(Item item, int amount)
    {
        int count = 0;
        foreach (ItemSlot slot in slots)
        {
            if (slot.slotItem == item) count += slot.itemCount;
            if (count >= amount) return true;
        }
        return false;
    }

    

    /*private void OnSlotClicked(ItemSlot slot)
    {
        itemUseButton.onClick.RemoveAllListeners();
        itemUseButton.onClick.AddListener(delegate { OnItemUseClicked(slot); slotOptionsUI.SetActive(false); });

        itemRemoveButton.onClick.RemoveAllListeners();
        itemRemoveButton.onClick.AddListener(delegate { OnRemoveItemClicked(slot); slotOptionsUI.SetActive(false); });

        slotOptionsUI.transform.position = inputActions.UI.Point.ReadValue<Vector2>();
        slotOptionsUI.SetActive(true);
    }

    private void OnRemoveItemClicked(ItemSlot slot)
    {
        slot.Remove(1);
    }

    private void OnItemUseClicked(ItemSlot slot)
    {
        ItemManager.Instance.UseItem(slot);
    }*/
}