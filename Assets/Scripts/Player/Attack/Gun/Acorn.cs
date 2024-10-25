using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Acorn : MonoBehaviour, IDamageable
{
    [SerializeField] private BulletData bulletData;

    private void Start()
    {
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject source = collision.gameObject;
        if (source.tag == "Enemy")
        {
            OnEnemyHit();
        }
    }

    private void OnEnemyHit()
    {
        Destroy(gameObject);
    }
    public float GetDamage()
    {
        return bulletData.damage;
    }
}