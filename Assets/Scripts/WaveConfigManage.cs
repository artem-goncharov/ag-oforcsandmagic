using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]

public class WaveConfigManage : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] public float timeBetweenSpawns = 0.8f;
    [SerializeField] public float moveSpeed = 100f;
    [SerializeField] int numberOfEnemies = 10;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetMoveSpeed() { return moveSpeed; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }
}
