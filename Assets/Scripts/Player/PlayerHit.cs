using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour, ISaveable
{
    [Header("References")]
    public bool playerAlive;

    [Header("Events")]
    public GameEvent onPlayerHealthChanged;
    public GameEvent onPlayerMaxHealthChanged;
    public GameEvent onPlayerDeath;
    public GameEvent onPlayerTakesDamage;



    [Header("Health")]
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;


    // Start is called before the first frame update
    void Start()
    {
        playerAlive = true;
        SetMaxHealth(maxHP);
        SetHealth(currentHP);
    }

    // Update is called once per frame
    void Update()
    {
        // Game Over HP 0
        if (currentHP <= 0 && playerAlive)
        {
            PlayerDeath();
        }

    }

    void OnCollisionEnter(Collision col)
    {
        // Debug.Log(col.gameObject.tag);
        GameObject source = col.gameObject;

        Debug.Log(col);

        if (source.CompareTag("Enemy"))
        {
            float damageTaken = source.GetComponent<EnemyAttack>().getBodyDamage();
            Hit(damageTaken);
            onPlayerTakesDamage.Raise(source.GetComponent<EnemyAttack>(), damageTaken);
        }
    }

    // Calculates the damage
    private void Hit(float damage)
    {
        if (currentHP - damage > 0)
        {
            currentHP -= damage;
        }
        else
        {
            currentHP = 0;
        }

        SetHealth(currentHP);
    }

    // Sets the HP of the player
    public void SetHealth(float health)
    {
        currentHP = health;
        onPlayerHealthChanged.Raise(this, health);
    }

    // Sets the Max HP of the player
    public void SetMaxHealth(float health)
    {
        maxHP = health;
        onPlayerMaxHealthChanged.Raise(this, health);
    }

    // Called when player dies
    public void PlayerDeath()
    {
        playerAlive = false;
        onPlayerDeath.Raise(this, null);
    }

    public void LoadData(GameData data)
    {
        maxHP = data.maxHP;
        currentHP = data.currentHP;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        Debug.Log($"Loaded Player HP: {currentHP}/{maxHP}");
    }

    public void SaveData(ref GameData data)
    {
        data.maxHP = maxHP;
        data.currentHP = currentHP;
        Debug.Log($"Saved Player HP: {currentHP}/{maxHP}");
    }
}
