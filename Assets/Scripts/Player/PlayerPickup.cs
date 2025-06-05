using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DynamicInventory inventory;

    [Header("Trackers")]
    private readonly List<InstanceItemContainer> inRangeItems = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PickupItems();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out InstanceItemContainer foundItem))
        {
            if (!inRangeItems.Contains(foundItem))
            {
                inRangeItems.Add(foundItem);
            }

        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out InstanceItemContainer foundItem))
        {
            if (inRangeItems.Contains(foundItem))
            {
                inRangeItems.Remove(foundItem);
            }
        }
    }

    private void PickupItems()
    {
        List<InstanceItemContainer> itemsToRemove = new(inRangeItems);

        foreach (InstanceItemContainer itemContainer in itemsToRemove) {
            ItemInstance item = itemContainer.TakeItem();
            inventory.AddItem(item);
            inRangeItems.Remove(itemContainer);

            Debug.Log($"Item Collected: {item.itemType.itemName}, Amount: {item.count}");
        }
    }
}
