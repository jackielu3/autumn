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
        if (data is Dictionary<string, object> bulletData)
        {
            if (bulletData.TryGetValue("itemType", out object itemTypeObj) &&
                bulletData.TryGetValue("count", out object countObj))
            {
                BulletData itemType = itemTypeObj as BulletData;
                int count = (int)countObj;

                SetBulletType(itemType.name);
                SetBulletCount(count);

                Debug.Log("Gun UI Set");
            }
        }
    }
}
