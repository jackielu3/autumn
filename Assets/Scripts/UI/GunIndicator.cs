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

    public void UpdateBulletType(Component sender, object data)
    {
        if (data is BulletData)
        {
            BulletData value = (BulletData)data;
            SetBulletType(value.name);
            SetBulletCount(value.bulletCount);
        }
    }

    public void UpdateBulletCount(Component sender, object data)
    {
        if (data is int)
        {
            int value = (int)data;
            SetBulletCount(value);
        }
    }
}
