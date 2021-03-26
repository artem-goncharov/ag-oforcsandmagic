using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManageBomb : MonoBehaviour
{
    [Header("Enemy Bomb Stats")]
    [SerializeField] int scoreValue = 100;
    [SerializeField] public int maxHealth = 100;
    [SerializeField] HealthBarManage healthBarBomb;
    int currentHealth;

    [Header("Bomb Death Sound")]
    [SerializeField] AudioClip bombDeathSound;
    [SerializeField] [Range(0, 1)] float bombDeathSoundVolume = 0.75f;

    GameSessionManage gameSession;

    void Start()
    {
        gameSession = FindObjectOfType<GameSessionManage>();

        currentHealth = maxHealth;
        healthBarBomb.SetMaxHealth(maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealerManage damageDealer = other.gameObject.GetComponent<DamageDealerManage>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealerManage damageDealer)
    {
        // Destroy Player Suriken
        damageDealer.Hit();

        // Decrease enemy health and destroy it        
        currentHealth -= damageDealer.GetDamage();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (gameSession)
        {
            gameSession.AddToScore(scoreValue);
        }

        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(bombDeathSound, Camera.main.transform.position, bombDeathSoundVolume);
    }
}
