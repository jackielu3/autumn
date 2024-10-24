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
            damageSource.GetDamage();

            Debug.Log($"{gameObject.name} dammage taken: {damageSource.GetDamage()}, Current HP: {MaxHP}/{Hp}");
        }
    }
}