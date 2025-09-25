using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunIndicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField][ReadOnly] private string bulletType;
    [SerializeField][ReadOnly] private int bulletCount;


    private void SetBulletType(string type)
    {
        bulletType = type;
        SetText();
    }

    private void SetBulletCount(int count)
    {
        bulletCount = count;
        SetText();
    }

    private void SetText()
    {
        text.text = $"{bulletType} {bulletCount}";
    }

    public void UpdateBulletUI(Component sender, object data)
    {
        if (data is DynamicInventory.InventoryEventData eventData)
        {
            // Only react to bullet items
            if (eventData.itemType is BulletData bulletData)
            {
                SetBulletType(bulletData.name);
                SetBulletCount(eventData.count);
            }
        }
    }
}
