using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [Header("References")]
    public bool playerAlive;

    [Header("Events")]
    public GameEvent onPlayerHealthChanged;
    public GameEvent onPlayerMaxHealthChanged;
    public GameEvent onPlayerDeath;
    public GameEvent onPlayerTakesDamage;



    [Header("Health")]
    [SerializeField] private float MaxHp;
    [SerializeField] private float Hp;


    // Start is called before the first frame update
    void Start()
    {
        playerAlive = true;
        SetMaxHealth(MaxHp);
        SetHealth(Hp);
    }

    // Update is called once per frame
    void Update()
    {
        // Game Over HP 0
        if (Hp <= 0 && playerAlive)
        {
            PlayerDeath();
        }

    }

    void OnCollisionEnter(Collision col)
    {
        // Debug.Log(col.gameObject.tag);
        GameObject source = col.gameObject;

        // Debug.Log(col);

        if (source.tag == "Enemy")
        {
            float damageTaken = source.GetComponent<EnemyAttack>().getBodyDamage();
            Hit(damageTaken);
            onPlayerTakesDamage.Raise(source.GetComponent<EnemyAttack>(), damageTaken);
        }
    }

    // Calculates the damage
    private void Hit(float damage)
    {
        if (Hp - damage > 0)
        {
            Hp -= damage;
        }
        else
        {
            Hp = 0;
        }

        SetHealth(Hp);
    }

    // Sets the HP of the player
    public void SetHealth(float health)
    {
        Hp = health;
        onPlayerHealthChanged.Raise(this, health);
    }

    // Sets the Max HP of the player
    public void SetMaxHealth(float health)
    {
        MaxHp = health;
        onPlayerMaxHealthChanged.Raise(this, health);
    }

    // Called when player dies
    public void PlayerDeath()
    {
        playerAlive = false;
        onPlayerDeath.Raise(this, null);
    }
}
