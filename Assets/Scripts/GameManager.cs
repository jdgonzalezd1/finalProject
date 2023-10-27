using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private GameObject gameOverUI;

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
}
