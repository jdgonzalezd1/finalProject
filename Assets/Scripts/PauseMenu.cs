using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{ 
    public static PauseMenu instance;   
    public GameObject pauseUI;
    public bool gamePause;

    public GameObject resourcesUI;
    public GameObject waveUI;

    void Start()
    {
        instance = this;
        pauseUI.SetActive(false);
        gamePause = false;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePause = !gamePause;
            PauseGame();
        }
        if(pauseUI.activeInHierarchy == false)
        {
            gamePause = false;
            Time.timeScale = 1;
            waveUI.SetActive(true);
            resourcesUI.SetActive(true);
        }
    }

    public void PauseGame()
    {
        waveUI.SetActive(false);
        resourcesUI.SetActive(false);
        pauseUI.SetActive(gamePause);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        gamePause = true;
    }

    public void ResumeGame()
    {
        waveUI.SetActive(true);
        resourcesUI.SetActive(true);
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        gamePause = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}