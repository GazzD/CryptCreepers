using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefab;

    [Range(1, 10)][SerializeField] float spawnRate = 1f;

    void Start()
    {
        StartCoroutine(SpawnNewEnemy());
        
    }

    void Update()
    {
        
    }

    IEnumerator SpawnNewEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(1/spawnRate);
            float random = Random.Range(0f, 1f);
            if (random < GameManager.Instance.difficulty * 0.1)
            {
                Instantiate(enemyPrefab[0]);
            }
            else {
                Instantiate(enemyPrefab[1]);
            }
        }
    }
}
