using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DynamicInventory : ScriptableObject
{
    [Header("Events")]
    public GameEvent onItemBegan;
    public GameEvent onItemCountChanged;
    public GameEvent onItemEnded;

    [Serializable]
    public class PredefinedItem
    {
        public ItemData itemType;
        public int count;
    }

    [Serializable]
    public class InventoryEventData
    {
        public ItemData itemType;
        public int count;         // resulting count after the change
        public int changeAmount;  // delta applied (positive for add, negative for remove)

        public InventoryEventData(ItemData itemType, int count, int changeAmount)
        {
            this.itemType = itemType;
            this.count = count;
            this.changeAmount = changeAmount;
        }
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

    // used for debugging
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
                ItemBegan(item, amount);
                ItemCountChanged(item, amount);
            }
            else
            {
                Debug.LogWarning($"No space left in category for {item.itemType.itemName}");
            }
        }
    }

    // TODO TEST IF USED!
    public void RemoveItem(ItemInstance item)
    {
        var category = GetCategory(item.itemType.GetType());
        if (category == null) return;

        // Remove entire item entry and raise End event
        if (category.items.Remove(item))
        {
            ItemEnded(item, -item.count);
        }
    }

    // Consume a specific amount of an item type. Returns true if successful.
    public bool TryConsume(ItemData itemType, int amount)
    {
        if (amount <= 0) return true;

        var category = GetCategory(itemType.GetType());
        if (category == null) return false;

        var existingItem = category.items.Find(existing => existing.itemType == itemType);
        if (existingItem == null) return false;
        if (existingItem.count < amount) return false;

        existingItem.count -= amount;
        ItemCountChanged(existingItem, -amount);

        if (existingItem.count <= 0)
        {
            // Remove the entry and raise End event
            category.items.Remove(existingItem);
            ItemEnded(existingItem, -amount);
        }

        return true;
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
        var eventData = new InventoryEventData(item.itemType, item.count, changeAmount);
        onItemCountChanged.Raise(null, eventData);
    }

    private void ItemBegan(ItemInstance item, int changeAmount)
    {
        var eventData = new InventoryEventData(item.itemType, item.count, changeAmount);
        onItemBegan.Raise(null, eventData);
    }

    private void ItemEnded(ItemInstance item, int changeAmount)
    {
        var eventData = new InventoryEventData(item.itemType, 0, changeAmount);
        onItemEnded.Raise(null, eventData);
    }
}
