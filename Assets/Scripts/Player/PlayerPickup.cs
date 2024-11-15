using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public DynamicInventory inventory;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out InstanceItemContainer foundItem))
        {
            inventory.AddItem(foundItem.TakeItem());

            Debug.Log($"Item Collected: { foundItem.name }, Amout: { foundItem.item.count }");
        }
    }
}
