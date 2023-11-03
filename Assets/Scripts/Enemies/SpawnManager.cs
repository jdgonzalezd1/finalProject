using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform[] spawnLocations;


    private void Update()
    {
        if (gameManager.WaveEnemies == 0 && gameManager.WaveCount < 3)
        {
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        if (gameManager.WaveCount <= 0)
        {
            gameManager.WaveCount = 1;
        }
        else
        {
            gameManager.WaveCount++;
        }

        foreach (var item in spawnLocations)
        {
            for (int i = 0; i < gameManager.WaveCount; i++)
            {
                Instantiate(enemy, item.position, enemy.transform.rotation);
            }
        }
    }
}
