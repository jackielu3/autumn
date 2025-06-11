using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DynamicInventory : ScriptableObject
{
    [Header("Events")]
    public GameEvent onItemCountChanged;

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

    // These are for the devs (us!!!) to be able to see what is happening in the inventory
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
            ItemCountChanged(existingItem, amount);
        }
        else
        {
            if (category.items.Count < category.maxSize)
            {
                item.count = amount;
                category.items.Add(item);
                ItemCountChanged(item, amount);
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
        category?.items.Remove(item);
        ItemCountChanged(item, -1);
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

    private void ItemCountChanged(ItemInstance item, int changeAmount)
    {
        var eventData = new Dictionary<string, object>
        {
            { "itemType", item.itemType },
            { "count", item.count },
            { "changeAmount", changeAmount }
        };

        onItemCountChanged.Raise(null, eventData);
    }
}
