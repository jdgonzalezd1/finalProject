using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void StartGame()
    {
        //Debug.Log(SceneManager.GetSceneByName("Prototype").name);
        SceneManager.LoadScene(1);
    }
}
