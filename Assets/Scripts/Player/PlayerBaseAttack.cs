using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseAttack : ScriptableObject
{
    public string attackName;
    public float cooldown;
    public float damage;
}