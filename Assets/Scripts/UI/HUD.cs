using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider easeBar;
    [SerializeField] private float lerpSpeed = 0.05f;

    [SerializeField] private Slider manaBar;
    [SerializeField] private Slider staminaBar;

    [SerializeField] private TextMeshProUGUI enemiesCount;
    [SerializeField] private TextMeshProUGUI waveCount;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private PlayerHealth playerResources;

    private void Start()
    {
        playerResources = FindAnyObjectByType<PlayerHealth>();
        gameManager = FindAnyObjectByType<GameManager>();
        UpdateHealthBar(playerResources.health);
        UpdateManaBar(playerResources.mana);
    }    

    private void Update()
    {
        EaseBar();
        UpdateEnemyCount();
        UpdateWaveCount();
    }

    private void EaseBar()
    {
        if(healthBar.value != easeBar.value)
        {
            easeBar.value = Mathf.Lerp(easeBar.value, healthBar.value, lerpSpeed);
        }
    }
    /*
    public void UpdateHealth(int health)
    {
        healthText.text = "Health: " + health;
    }*/

    public void UpdateHealthBar(float health)
    {
        if (healthBar.maxValue < health)
        {
            healthBar.maxValue = health;
            easeBar.maxValue = health;
            easeBar.value = health;
        }

        healthBar.value = health;
    }
    /*
    public void UpdateMana(int mana)
    {
        manaText.text = "Mana: " + mana;
    }*/

    public void UpdateManaBar(int mana)
    {
        if (manaBar.maxValue < mana)
        {
            manaBar.maxValue = mana;
        }
        manaBar.value = mana;
    }

    public void UpdateEnemyCount()
    {
        enemiesCount.text = gameManager.WaveEnemies.ToString();
    }

    public void UpdateWaveCount()
    {
        waveCount.text = "Wave\n" + gameManager.WaveCount.ToString() + "/" + gameManager.FinalWave.ToString();
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Win()
    {
        winUI.SetActive(true);
        waveCount.text = null;
        Cursor.lockState = CursorLockMode.None;
    }
}
