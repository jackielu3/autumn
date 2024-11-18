using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DynamicInventory inventory;

    [Header("Trackers")]
    private List<ItemInstance> inRangeItems;
        

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out InstanceItemContainer foundItem))
        {
            ItemInstance item = foundItem.TakeItem();
            inventory.AddItem(item);

            Debug.Log($"Item Collected: { item.itemType.itemName }, Amout: { foundItem.item.count }");
        }
    }
}
