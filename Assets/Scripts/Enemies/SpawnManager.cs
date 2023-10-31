using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int wave;
    [SerializeField] private int enemies;
    
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform[] spawnLocations;
    
    private void Update()
    {
        wave = gameManager.WaveCount;
        enemies = gameManager.WaveEnemies;
        if (enemies == 0 && wave < 3)
        {
            SpawnWave(wave);
        }    
    }

    private void SpawnWave(int wave)
    {
        foreach (var item in spawnLocations)
        {
            for(int i = 0; i < wave; i++)
            {
                Instantiate(enemy, item.position, enemy.transform.rotation);
            }
        }
        this.wave++;
    }
}
