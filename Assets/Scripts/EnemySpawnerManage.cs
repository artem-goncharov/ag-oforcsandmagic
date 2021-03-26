using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManage : MonoBehaviour
{
    [SerializeField] List<WaveConfigManage> waveConfigs;
    [SerializeField] int startingWave;
    [SerializeField] bool looping = false;

    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        startingWave = 0;

        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            // Print new wave information
            // Debug.Log("Started wave with index: [ " + waveIndex + " ] Wave name: < " + currentWave + " > ");
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfigManage waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathingManage>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
