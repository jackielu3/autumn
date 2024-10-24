using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private float MaxHP;
    [SerializeField] private float Hp;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject source = collision.gameObject;

        IDamageable damageSource = source.GetComponent<IDamageable>();

        if (damageSource != null)
        {
            Hp -= damageSource.GetDamage();

            if (Hp <= 0)
            {
                Death();
            }

            Debug.Log($"{gameObject.name} dammage taken: {damageSource.GetDamage()}, Current HP: {MaxHP}/{Hp}");
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}