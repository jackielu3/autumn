using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Inventory/Bullet")]
public class BulletData : ItemData
{
    [Header("Bullet Logic")]
    public float damage;
    public float bulletForce;
    public float reloadTime;
}