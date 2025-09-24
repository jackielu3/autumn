using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* WIP, NEED TO EDIT WITH NEW CATEGORY SYSTEM, SAME ISSUE AS ITEMDISPLAY.CS
public class InventoryDisplay : MonoBehaviour
{
    public DynamicInventory inventory;
    public ItemDisplay[] slots;

    private void Start()
    {
        UpdateInventory();
    }

    void UpdateInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].UpdateItemDisplay(inventory.items[i].itemType.icon, i);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }

    // TEMP, COPIED FROM https://gamedevbeginner.com/how-to-make-an-inventory-system-in-unity/
    public void DropItem(int itemIndex)
    {
        // Creates a new object and gives it the item data
        GameObject droppedItem = new GameObject();
        droppedItem.AddComponent<Rigidbody>();
        droppedItem.AddComponent<InstanceItemContainer>().item = inventory.categories[itemIndex];
        GameObject itemModel = Instantiate(inventory.items[itemIndex].itemType.model, droppedItem.transform);

        // Removes the item from the inventory
        inventory.items.RemoveAt(itemIndex);

        // Updates the inventory again
        UpdateInventory();
    }
}
*/
