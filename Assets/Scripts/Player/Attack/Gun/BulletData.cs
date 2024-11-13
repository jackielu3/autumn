using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullets/BulletData")]
public class BulletData : ScriptableObject
{
    [Header("References")]
    public GameObject model;
    public Rigidbody rigidbody;
    public AnimationClip animation;

    [Header("Bullet Logic")]
    public float damage;
    public float bulletForce;
    public float reloadTime;

    [Header("Inventory")]
    public int bulletCount;
}