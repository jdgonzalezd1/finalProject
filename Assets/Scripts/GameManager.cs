using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int waveEnemies;
    [SerializeField] private int waveCount;

    private void Awake()
    {
        waveCount = 1;        
    }

    private void Update()
    {
        waveEnemies = CheckEnemies();
    }
   

    
    private int CheckEnemies()
    {
        int currentEnemies = FindObjectsByType<EnemyNavigation>(FindObjectsSortMode.None).Length;
        return currentEnemies;
    }
    

    public int WaveEnemies
    {
        get { return waveEnemies; }
        set {  waveEnemies = value; }
    }

    public int WaveCount
    {
        get { return waveCount; }
        set { waveCount = value; }
    }
}
