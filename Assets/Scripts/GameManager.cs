using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private int waveEnemies;

    private void Awake()
    {
        //waveEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
    }

    private void Update()
    {
        //Win(CheckEnemies())
    }
    public void UpdateHealth(int health)
    {
        healthText.text = "Health: " + health;
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

    public void Win(int enemies)
    {
        if (waveEnemies == 0)
        {
            winUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    /*
    private int CheckEnemies()
    {
        waveEnemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        return waveEnemies;
    }
    */
}
