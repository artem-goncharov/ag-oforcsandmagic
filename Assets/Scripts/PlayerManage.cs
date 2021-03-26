using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManage : MonoBehaviour
{
    GameObject player;
    Sprite playerLookLeft;
    Sprite playerLookRight;

    int screenWidth;
    int playerPosition;

    [Header("Player Health")]
    [SerializeField] public int maxHealth = 5000;
    int currentHealth;

    [Header("Player Projectile")]
    [SerializeField] GameObject surikenPrefab;
    [SerializeField] float projectileSpeed = 900f;
    [SerializeField] float surikenFiringPeriod = 0.1f;
    [SerializeField] AudioClip playerShootSound;
    [SerializeField] [Range(0, 1)] float playerShootSoundVolume = 0.25f;

    [Header("HealthBar")]
    [SerializeField] HealthBarManage healthBarPlayer;
    [SerializeField] AudioClip playerHurtSound;
    [SerializeField] [Range(0, 1)] float playerHurtSoundVolume = 0.75f;
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] [Range(0, 1)] float playerDeathSoundVolume = 0.75f;

    Coroutine firingRightCoroutine;
    Coroutine firingLeftCoroutine;

    // Current game session
    GameSessionManage gameSession;

    GameObject CanvasVictory;
    GameObject CanvasEpicFail;

    void Start()
    {
        screenWidth = Screen.width;
        playerPosition = screenWidth / 2;
        //Debug.Log(playerPosition);

        playerLookLeft = Resources.Load<Sprite>("Player/Player Look Left");
        playerLookRight = Resources.Load<Sprite>("Player/Player Look Right");

        player = GameObject.Find("Player");
        player.GetComponent<SpriteRenderer>().sprite = playerLookLeft;

        currentHealth = maxHealth;
        healthBarPlayer.SetMaxHealth(maxHealth);

        gameSession = FindObjectOfType<GameSessionManage>();

        CanvasVictory = GameObject.Find("CanvasVictory");
        CanvasEpicFail = GameObject.Find("CanvasEpicFail");
    }

    void Update()
    {
        if (Input.mousePosition.x >= playerPosition)
        {
            player.GetComponent<SpriteRenderer>().sprite = playerLookRight;
            
            if (firingLeftCoroutine != null)
            {
                StopCoroutine(firingLeftCoroutine);
            }
            FireRight();
        }
        else
        {
            player.GetComponent<SpriteRenderer>().sprite = playerLookLeft;
            
            if (firingRightCoroutine != null)
            {
                StopCoroutine(firingRightCoroutine);
            }
            FireLeft();
        }
        CheckForVictory();
    }

    public void FireRight()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingRightCoroutine = StartCoroutine(FireRightContinuously());
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingRightCoroutine);
        }
    }

    public void FireLeft()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingLeftCoroutine = StartCoroutine(FireLeftContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingLeftCoroutine);
        }
    }

    IEnumerator FireRightContinuously()
    {
        while (true)
        {
            GameObject suriken = Instantiate(surikenPrefab, transform.position + new Vector3(20, 0, 0), Quaternion.identity) as GameObject;
            suriken.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
            AudioSource.PlayClipAtPoint(playerShootSound, Camera.main.transform.position, playerShootSoundVolume);

            yield return new WaitForSeconds(surikenFiringPeriod);
        }
    }

    IEnumerator FireLeftContinuously()
    {
        while (true)
        {
            GameObject suriken = Instantiate(surikenPrefab, transform.position + new Vector3(-20, 0, 0), Quaternion.identity) as GameObject;
            suriken.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * -1f, 0);
            AudioSource.PlayClipAtPoint(playerShootSound, Camera.main.transform.position, playerShootSoundVolume);

            yield return new WaitForSeconds(surikenFiringPeriod);
        }
    }

    private void CheckForVictory()
    {
        if (gameSession.score > 5000)
        {
            // Show Popup text if player wins = Score reaches 5000            
            CanvasVictory.transform.position = new Vector3(0, 40, 350);
            Destroy(CanvasEpicFail);

            FindObjectOfType<LevelManage>().LoadMenuScene();
        }
    }

    // Player gets damage
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealerManage damageDealer = other.gameObject.GetComponent<DamageDealerManage>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealerManage damageDealer)
    {
        // Destroy Bomb or Spell
        damageDealer.Hit();

        // Decrease Player health and destroy it        
        currentHealth -= damageDealer.GetDamage();
        // Debug.Log(currentHealth);
        healthBarPlayer.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Destroy(healthBarPlayer.gameObject);
            AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);

            // Show Popup text if player dies
            CanvasEpicFail.transform.position = new Vector3(0, 40, 350);
            Destroy(CanvasVictory);

            FindObjectOfType<LevelManage>().LoadMenuScene();
        }
        else
        {
            AudioSource.PlayClipAtPoint(playerHurtSound, Camera.main.transform.position, playerHurtSoundVolume);
        }
    }

    public void OrcAttacsPlayer(int orcDamage)
    {
        currentHealth -= orcDamage;
        healthBarPlayer.SetHealth(currentHealth);
        AudioSource.PlayClipAtPoint(playerHurtSound, Camera.main.transform.position, playerHurtSoundVolume);

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Destroy(healthBarPlayer.gameObject);
            AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);

            // Show Popup text if player dies            
            CanvasEpicFail.transform.position = new Vector3(0, 40, 350);
            Destroy(CanvasVictory);

            FindObjectOfType<LevelManage>().LoadMenuScene();
        }
        else
        {
            AudioSource.PlayClipAtPoint(playerHurtSound, Camera.main.transform.position, playerHurtSoundVolume);
        }
    }
}
