using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 1f);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefabs.Length == 0)
        {
            Debug.LogWarning("Spawn points or asteroid prefabs are not set!");
            return;
        }

        int randomEnemy = Random.Range(0, enemyPrefabs.Length);
        int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.Euler(0, 0, 180));
    }
}
