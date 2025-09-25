using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuActivated;
    public ItemSlot[] itemSlot;

    void Update()
    {
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public void AddItem(ItemInstance item)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                itemSlot[i].AddItem(item);
                return;
            }
        }
    }

    public void OnInventoryItemBegan(Component sender, object data)
    {
        if (data is DynamicInventory.InventoryEventData evt)
        {
            // If already present, just update quantity
            int index = FindSlotIndexByItem(evt.itemType);
            if (index >= 0)
            {
                itemSlot[index].UpdateQuantity(evt.count);
                return;
            }

            // Otherwise place into first empty slot
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (!itemSlot[i].isFull)
                {
                    AddItem(new ItemInstance(evt.itemType) { count = evt.count });
                    return;
                }
            }
        }
    }

    public void OnInventoryItemCountChanged(Component sender, object data)
    {
        if (data is DynamicInventory.InventoryEventData evt)
        {
            int index = FindSlotIndexByItem(evt.itemType);
            if (index >= 0)
            {
                itemSlot[index].UpdateQuantity(evt.count);
            }
        }
    }

    public void OnInventoryItemEnded(Component sender, object data)
    {
        if (data is DynamicInventory.InventoryEventData evt)
        {
            int index = FindSlotIndexByItem(evt.itemType);
            if (index >= 0)
            {
                itemSlot[index].ClearItem();
            }
        }
    }

    private int FindSlotIndexByItem(ItemData item)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull && itemSlot[i].itemData == item)
            {
                return i;
            }
        }
        return -1;
    }
}
