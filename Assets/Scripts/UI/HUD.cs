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

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject gameManager;

    [SerializeField] private PlayerHealth playerResources;

    private void Start()
    {
        playerResources = FindAnyObjectByType<PlayerHealth>();
        UpdateHealthBar(playerResources.health);        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(playerResources.canBeHurtDelay(10));
        }
        EaseBar();
    }

    private void EaseBar()
    {
        if(healthBar.value != easeBar.value)
        {
            easeBar.value = Mathf.Lerp(easeBar.value, healthBar.value, lerpSpeed);
        }
    }

    public void UpdateHealth(int health)
    {
        healthText.text = "Health: " + health;
    }

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

    public void UpdateMana(int mana)
    {
        manaText.text = "Mana: " + mana;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Win()
    {
        winUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
}
