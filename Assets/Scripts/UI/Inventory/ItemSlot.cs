using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [Header("Item Data")]
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;

    [Header("Item Slot")]
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public void AddItem(ItemInstance item)
    {
        ItemData data = item.itemType;
        this.itemName = data.itemName;
        this.quantity = item.count;
        this.itemSprite = data.sprite;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }
}
