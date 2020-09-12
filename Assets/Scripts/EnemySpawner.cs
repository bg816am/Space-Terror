using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> _waveConfigs;
    [SerializeField] private bool looping = false;
    [SerializeField] int startingWave = 0;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves()); 
        } while (looping);
        
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveCount = startingWave; waveCount < _waveConfigs.Count; waveCount++)
        {
            var currentWave = _waveConfigs[waveCount];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
            {
                for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
                {
                    var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                        waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
                    newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
                    yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
                }
            }
        
    
}

