using System.Collections;
using UnityEngine;

//This struct contains utility functions for the inventory system, object tweening and object highlighting.
public readonly struct Utils
{
    /*
    * This method attempts to transfer the items from the trigger ItemSlot to the target ItemSlot.
    * If the item types are the same, they will stack until the maximum capcity is reached on the target ItemSlot.
    * If the item types are different they will swap places.
    */
    public static void TransferItem(ItemSlot trigger, ItemSlot target)
    {
        if (trigger == target) return;

        Item triggerItem = trigger.slotItem;
        Item targetItem = target.slotItem;

        int triggerItemCount = trigger.itemCount;

        if (!trigger.IsEmpty)
        {
            if (target.IsEmpty || targetItem == triggerItem)
            {
                for (int i = 0; i < triggerItemCount; i++)
                {
                    if (target.Add(triggerItem)) trigger.Remove(1);
                    else return;
                }
            }
            else
            {
                int targetItemCount = target.itemCount;

                target.Clear();
                for (int i = 0; i < triggerItemCount; i++) target.Add(triggerItem);

                trigger.Clear();
                for (int i = 0; i < targetItemCount; i++) trigger.Add(targetItem);
            }
        }
    }

    //This method attempts to transfer the passed in amount of items from the trigger ItemSlot to the target ItemSlot.
    public static void TransferItemQuantity(ItemSlot trigger, ItemSlot target, int amount)
    {
        if (!trigger.IsEmpty)
        {
            for (int i = 0; i < amount; i++)
            {
                if (!trigger.IsEmpty)
                {
                    if (target.Add(trigger.slotItem)) trigger.Remove(1);
                    else return;
                }
                else return;
            }
        }
    }
}