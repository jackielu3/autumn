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
    [SerializeField] private int playerMaxHP;
    [SerializeField] private int playerHP;


    // Start is called before the first frame update
    void Start()
    {
        playerAlive = true;
        SetMaxHealth(playerMaxHP);
        SetHealth(playerHP);
    }

    // Update is called once per frame
    void Update()
    {
        // Game Over HP 0
        if (playerHP <= 0 && playerAlive)
        {
            PlayerDeath();
        }

    }

    void OnCollisionEnter(Collision col)
    {
        // Debug.Log(col.gameObject.tag);
        GameObject source = col.gameObject;

        Debug.Log(col);

        if (source.tag == "Enemy")
        {
            int damageTaken = (int)source.GetComponent<EnemyAttack>().getBodyDamage();
            Hit(damageTaken);
            onPlayerTakesDamage.Raise(source.GetComponent<EnemyAttack>(), damageTaken);
        }
    }

    // Calculates the damage
    private void Hit(int damage)
    {
        if (playerHP - damage > 0)
        {
            playerHP -= damage;
        }
        else
        {
            playerHP = 0;
        }

        SetHealth(playerHP);
    }

    // Sets the HP of the player
    public void SetHealth(int health)
    {
        playerHP = health;
        onPlayerHealthChanged.Raise(this, health);
    }

    // Sets the Max HP of the player
    public void SetMaxHealth(int health)
    {
        playerMaxHP = health;
        onPlayerMaxHealthChanged.Raise(this, health);
    }

    // Called when player dies
    public void PlayerDeath()
    {
        playerAlive = false;
        onPlayerDeath.Raise(this, null);
    }
}
