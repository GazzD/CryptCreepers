using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject checkPointPrefab;
    [SerializeField] GameObject[] powerUpPrefabs;
    [SerializeField] int checkpointSpawnDelay = 7;
    [SerializeField] int powerUpSpawnDelay = 10;
    [SerializeField] float spawnRadious = 10;

    void Start()
    {
        StartCoroutine(SpawnCheckpointRoutine());
        StartCoroutine(SpawnPowerUpRoutine());

    }

    
    IEnumerator SpawnCheckpointRoutine() {
        while (true) { 
            yield return new WaitForSeconds(checkpointSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadious;

            Instantiate(checkPointPrefab, randomPosition, Quaternion.identity);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpSpawnDelay);
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadious;

            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], randomPosition, Quaternion.identity);
        }
    }

}
