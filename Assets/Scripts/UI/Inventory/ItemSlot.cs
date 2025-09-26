using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("Item Data")]
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public ItemData itemData;

    [Header("Item Slot")]
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public GameObject selectedShader;
    public bool isItemSelected;

    public void AddItem(ItemInstance item)
    {
        ItemData data = item.itemType;
        this.itemData = data;
        this.itemName = data.itemName;
        this.quantity = item.count;
        this.itemSprite = data.sprite;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
        itemImage.enabled = itemSprite != null;
        isFull = true;
    }

    public void UpdateQuantity(int newQuantity)
    {
        quantity = newQuantity;
        if (quantityText != null)
        {
            quantityText.text = quantity.ToString();
        }

        if (quantity <= 0)
        {
            ClearItem();
        }
    }

    public void ClearItem()
    {
        itemData = null;
        itemName = string.Empty;
        quantity = 0;
        itemSprite = null;

        if (quantityText != null)
        {
            quantityText.text = string.Empty;
            quantityText.enabled = false;
        }
        if (itemImage != null)
        {
            itemImage.sprite = null;
            itemImage.enabled = false;
        }
        isFull = false;
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (data.button == PointerEventData.InputButton.Left)
        {
            OnClick();   
        }
    }

    private void OnClick()
    {
        selectedShader.SetActive(true);
        isItemSelected = true;
    }   
}
