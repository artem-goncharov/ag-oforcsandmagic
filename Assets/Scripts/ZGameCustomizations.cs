using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZGameCustomizations : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] PlayerManage player;
    GameObject healthPlayer;
    [SerializeField] DamageDealerManage playerAttack;
    GameObject attackPlayer;

    [Header("Orc Stats")]
    [SerializeField] EnemyManageOrc orc;
    GameObject healthOrc;
    [SerializeField] DamageDealerManage orcAttack;
    GameObject attackOrc;
    [SerializeField] WaveConfigManage orcSpeedSpawn;
    GameObject speedOrc;
    GameObject spawnOrc;

    [Header("Ghost Stats")]
    [SerializeField] EnemyManageGhost ghost;
    GameObject healthGhost;
    [SerializeField] DamageDealerManage ghostAttack;
    GameObject attackGhost;
    [SerializeField] WaveConfigManage ghostSpeedSpawn;
    GameObject speedGhost;
    GameObject spawnGhost;

    [Header("Bomb Stats")]
    [SerializeField] EnemyManageBomb bomb;
    GameObject healthBomb;
    [SerializeField] DamageDealerManage bombAttack;
    GameObject attackBomb;
    [SerializeField] WaveConfigManage bombSpeedSpawn;
    GameObject speedBomb;
    GameObject spawnBomb;

    void Start()
    {
        // Show Player stats
        healthPlayer = GameObject.Find("HealthPlayer");
        healthPlayer.GetComponent<TMP_InputField>().text = player.maxHealth.ToString();
        attackPlayer = GameObject.Find("AttackPlayer");
        attackPlayer.GetComponent<TMP_InputField>().text = playerAttack.damage.ToString();

        // Show Orc stats
        healthOrc = GameObject.Find("HealthOrc");
        healthOrc.GetComponent<TMP_InputField>().text = orc.maxHealth.ToString();
        attackOrc = GameObject.Find("AttackOrc");
        attackOrc.GetComponent<TMP_InputField>().text = orcAttack.damage.ToString();
        speedOrc = GameObject.Find("SpeedOrc");
        speedOrc.GetComponent<TMP_InputField>().text = orcSpeedSpawn.moveSpeed.ToString();
        spawnOrc = GameObject.Find("SpawnOrc");
        spawnOrc.GetComponent<TMP_InputField>().text = orcSpeedSpawn.timeBetweenSpawns.ToString();

        // Show Ghost stats
        healthGhost = GameObject.Find("HealthGhost");
        healthGhost.GetComponent<TMP_InputField>().text = ghost.maxHealth.ToString();
        attackGhost = GameObject.Find("AttackGhost");
        attackGhost.GetComponent<TMP_InputField>().text = ghostAttack.damage.ToString();
        speedGhost = GameObject.Find("SpeedGhost");
        speedGhost.GetComponent<TMP_InputField>().text = ghostSpeedSpawn.moveSpeed.ToString();
        spawnGhost = GameObject.Find("SpawnGhost");
        spawnGhost.GetComponent<TMP_InputField>().text = ghostSpeedSpawn.timeBetweenSpawns.ToString();

        // Show Bomb stats
        healthBomb = GameObject.Find("HealthBomb");
        healthBomb.GetComponent<TMP_InputField>().text = bomb.maxHealth.ToString();
        attackBomb = GameObject.Find("AttackBomb");
        attackBomb.GetComponent<TMP_InputField>().text = bombAttack.damage.ToString();
        speedBomb = GameObject.Find("SpeedBomb");
        speedBomb.GetComponent<TMP_InputField>().text = bombSpeedSpawn.moveSpeed.ToString();
        spawnBomb = GameObject.Find("SpawnBomb");
        spawnBomb.GetComponent<TMP_InputField>().text = bombSpeedSpawn.timeBetweenSpawns.ToString();

    }

    void Update()
    {

    }
}
