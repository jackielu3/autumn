using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Acorn : MonoBehaviour, IDamageable
{
    [SerializeField] private BulletData bulletData;
    [SerializeField] private LayerMask enemyLayerMask;

    private void Start()
    {
    }


    private void OnCollisionEnter(Collision col)
    {
        // skip if no layer
        if (((1 << col.gameObject.layer) & enemyLayerMask) == 0)
            return;

        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            OnEnemyHit();
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