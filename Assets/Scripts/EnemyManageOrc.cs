using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManageOrc : MonoBehaviour
{
    [Header("Enemy Orc Stats")]
    [SerializeField] int scoreValue = 500;
    [SerializeField] public int maxHealth = 500;
    [SerializeField] HealthBarManage healthBarOrc;
    int currentHealth;

    [Header("Orc Hurt & Death Sound")]
    [SerializeField] AudioClip orcHurtSound;
    [SerializeField] [Range(0, 1)] float orcHurtSoundVolume = 0.75f;
    [SerializeField] AudioClip orcDeathSound;
    [SerializeField] [Range(0, 1)] float orcDeathSoundVolume = 0.75f;

    GameSessionManage gameSession;

    GameObject[] orcEnemies;
    int orcDamage = 100;
    float shotCounter;
    float timeBetweenHits = 1.5f;

    PlayerManage player;

    void Start()
    {
        gameSession = FindObjectOfType<GameSessionManage>();

        currentHealth = maxHealth;
        healthBarOrc.SetMaxHealth(maxHealth);

        orcEnemies = GameObject.FindGameObjectsWithTag("Orc");
        shotCounter = timeBetweenHits;

        player = FindObjectOfType<PlayerManage>();
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
        healthBarOrc.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(orcHurtSound, Camera.main.transform.position, orcHurtSoundVolume);
        }
    }

    private void Die()
    {
        if (gameSession)
        {
            gameSession.AddToScore(scoreValue);
        }

        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(orcDeathSound, Camera.main.transform.position, orcDeathSoundVolume);
    }

    void Update()
    {
        CheckIfOrcCanAttack();
    }

    void CheckIfOrcCanAttack()
    {
        foreach (GameObject orcEnemy in orcEnemies)
        {
            // Debug.Log("X: " + orcEnemy.transform.position.x + " Y: " + orcEnemy.transform.position.y);

            if (orcEnemy && player)
            {
                if (orcEnemy.transform.position.x == -20)
                {
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0f)
                    {
                        player.OrcAttacsPlayer(orcDamage);
                        shotCounter = timeBetweenHits;
                    }                    
                }

                if (orcEnemy.transform.position.x == 20)
                {
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0f)
                    {
                        player.OrcAttacsPlayer(orcDamage);
                        shotCounter = timeBetweenHits;
                    }
                }
            }
        }
    }
}
