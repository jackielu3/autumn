using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    [SerializeField] private float bodyDamage;


    public float getBodyDamage()
    {
        return bodyDamage;
    }
}
