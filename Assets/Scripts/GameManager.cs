using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int waveCount;
    [SerializeField] private int waveEnemies;
    [SerializeField] private int finalWave;

    [SerializeField] private HUD hud;

    private void Start()
    {        
        hud = FindAnyObjectByType<HUD>();
        finalWave = 3;
    }



    private void Update()
    {
        waveEnemies = CheckEnemies();
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (waveCount >= finalWave && waveEnemies == 0)
        {
            hud.Win();
        }
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

    public int FinalWave
    {
        get { return finalWave; }
    }
}
