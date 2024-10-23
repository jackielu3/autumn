using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseAttack : ScriptableObject
{
    public string attackName;
    public float cooldown;
    public float damage;

    public PlayerBaseAttack(string _name, float _cooldown, float _damage)
    {
        this.attackName = _name;
        this.cooldown = _cooldown;
        this.damage = _damage;
    }

    public abstract void ExecuteAttack();
}