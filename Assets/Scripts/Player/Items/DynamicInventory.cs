using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DynamicInventory : ScriptableObject
{
    [Serializable]
    public class PredefinedItem
    {
        public ItemData itemType;
        public int count;
    }

    // Eventually will (hopefully) be used to track save data and used to store items
    [Header("Predefined Items (for Initialization)")]
    public List<PredefinedItem> predefinedItems = new();

    [Serializable]
    public class Category
    {
        public int maxSize;
        public List<ItemInstance> items = new();
    }

    [SerializeField] private Dictionary<Type, Category> categories = new();

    // These are for the devs (us!!!) to be able to see what is happening in the inventory uwu
    [Header("Debug: Current Inventory")]
    [TextArea(5, 10)]
    public string inventoryDebugInfo;
    public void RefreshDebugInfo()
    {
        inventoryDebugInfo = "";
        foreach (var category in categories)
        {
            inventoryDebugInfo += $"{category.Key.Name}:\n";
            foreach (var item in category.Value.items)
            {
                inventoryDebugInfo += $"- {item.itemType.itemName}: {item.count}\n";
            }
        }
    }

    public void Initialize()
    {
        categories = new Dictionary<Type, Category>
        {
            { typeof(BulletData), new Category { maxSize = 50 } },
        };

        foreach (var predefinedItem in predefinedItems)
        {
            if (predefinedItem.itemType == null)
                continue;

            ItemInstance itemInstance = new(predefinedItem.itemType) { count = predefinedItem.count };
            AddItem(itemInstance, predefinedItem.count);
        }
    }

    public void AddItem(ItemInstance item, int amount = 1)
    {
        Category category = GetCategory(item.itemType.GetType());
        if (category == null)
        {
            Debug.LogWarning($"No category found for item type: {item.itemType.GetType().Name}");
            return;
        }

        ItemInstance existingItem = category.items.Find(existing => existing.itemType == item.itemType);

        if (existingItem != null)
        {
            existingItem.count += amount;
        }
        else
        {
            if (category.items.Count < category.maxSize)
            {
                item.count = amount;
                category.items.Add(item);
            }
            else
            {
                Debug.LogWarning($"No space left in category for {item.itemType.itemName}");
            }
        }
    }

    public void RemoveItem(ItemInstance item)
    {
        var category = GetCategory(item.itemType.GetType());
        if (category != null)
        {
            category.items.Remove(item);
        }
    }

    public ItemInstance FindItemInstance(ItemInstance item)
    {
        Category category = GetCategory(item.itemType.GetType());
        if (category == null) return null;

        return category.items.Find(existing => existing.itemType == item.itemType);
    }

    public Category GetCategory(Type itemType)
    {
        if (categories.TryGetValue(itemType, out Category category))
        {
            return category;
        }

        return null;
    }
    public Dictionary<Type, Category> GetCategories()
    {
        return categories;
    }

}





/*
[CreateAssetMenu]
public class DynamicInventory : ScriptableObject
{
    // HAVE DIFFERENT INVENTORY MAXS FOR DIFFERNET ITEM TYPES, IE. BULLETS, COLLECTABLES, POO, ETC

    [Header("Inventory Types")]
    public int maxBullets = 10;

    public List<ItemInstance> bullets = new();


    public void AddItem(ItemInstance item, int amount = 1)
    {
        if (item.itemType is BulletData)
        {
            AddToCategory(item, bullets, maxBullets, amount);
        }
    }

    private void AddToCategory(ItemInstance item, List<ItemInstance> category, int maxSize, int amount = 1)
    {
        ItemInstance existingItem = category.Find(existing => existing.itemType == item.itemType);

        if (existingItem != null)
        {
            existingItem.count += amount;
        }
        else
        {
            if (category.Count < maxSize)
            {
                item.count = amount;
                category.Add(item);
            }
            else
            {
                Debug.LogWarning($"No space left in category for {item.itemType.itemName}");
            }
        }
    }
    private ItemInstance FindItemInstance(ItemInstance item)
    {
        if (item.itemType is BulletData)
        {
            return bullets.Find(existing => existing.itemType == item.itemType);
        }
        else
        {
            return null;
        }
    }
}
*/