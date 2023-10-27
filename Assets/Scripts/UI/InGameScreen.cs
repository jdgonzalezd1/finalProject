using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExitGame()
    {
        //Debug.Log(SceneManager.GetSceneByBuildIndex(0).name);
        SceneManager.LoadScene(SceneManager.GetSceneByName("Title Screen").name);
    }
}
