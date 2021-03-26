using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManageGhost : MonoBehaviour
{
    GameObject[] ghostEnemies;

    [Header("Enemy Ghost Stats")]
    [SerializeField] int scoreValue = 300;
    [SerializeField] public int maxHealth = 300;
    [SerializeField] HealthBarManage healthBarGhost;
    int currentHealth;

    [Header("Ghost Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 75f;
    [SerializeField] AudioClip ghostShootSound;
    [SerializeField] [Range(0, 1)] float ghostShootSoundVolume = 0.25f;
    int ghostCanShoot = 0;

    [Header("Ghost Hurt & Death Sound")]    
    [SerializeField] AudioClip ghostHurtSound;
    [SerializeField] [Range(0, 1)] float ghostHurtSoundVolume = 0.75f;
    [SerializeField] AudioClip ghostDeathSound;
    [SerializeField] [Range(0, 1)] float ghostDeathSoundVolume = 0.75f;

    GameSessionManage gameSession;
    
    void Start()
    {
        shotCounter = timeBetweenShots;
        gameSession = FindObjectOfType<GameSessionManage>();

        ghostEnemies = GameObject.FindGameObjectsWithTag("Ghost");

        currentHealth = maxHealth;
        healthBarGhost.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        CheckIfGhostCanShoot();
    }

    public int SetGhostCanShoot()
    {
        // Debug.Log("Ghost Can Shoot");
        ghostCanShoot = 1;
        return ghostCanShoot;
    }

    public int SetGhostCanNotShoot()
    {
        // Debug.Log("Ghost Can Not Shoot");
        ghostCanShoot = 0;
        return ghostCanShoot;
    }

    void CheckIfGhostCanShoot()
    {
        foreach (GameObject ghostEnemy in ghostEnemies)
        {
            // Debug.Log("X: " + ghostEnemy.transform.position.x + " Y: " + ghostEnemy.transform.position.y);

            if (ghostEnemy)
            {
                if (ghostEnemy.transform.position.x == -95)
                {
                    SetGhostCanShoot();
                }

                if (ghostEnemy.transform.position.x == 95)
                {
                    SetGhostCanShoot();
                }
            }
            else
            {
                SetGhostCanNotShoot();
            }
        }

        if (ghostCanShoot == 1)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0f)
            {
                Fire();
                shotCounter = timeBetweenShots;
            }
        }
    }

    private void Fire()
    {
        GameObject spell = Instantiate(projectile, transform.position + new Vector3(0, 25, 0), Quaternion.identity) as GameObject;
        spell.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(ghostShootSound, Camera.main.transform.position, ghostShootSoundVolume);
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
        healthBarGhost.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            AudioSource.PlayClipAtPoint(ghostHurtSound, Camera.main.transform.position, ghostHurtSoundVolume);
        }
    }

    private void Die()
    {
        if (gameSession)
        {
            gameSession.AddToScore(scoreValue);
        }

        SetGhostCanNotShoot();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(ghostDeathSound, Camera.main.transform.position, ghostDeathSoundVolume);
    }
}
