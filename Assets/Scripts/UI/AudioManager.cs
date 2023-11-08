using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource gameAudio;
    public AudioClip menuAudioclip;
    public AudioClip gameAudioclip;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        SetAudio();
        slider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        gameAudio.volume = slider.value;
    }

    public void SetAudio()
    {        

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Title Screen")
        {
            gameAudio.clip = menuAudioclip;
            gameAudio.Play();
        }
        if (scene.name == "ArenaScene01")
        {
            gameAudio.clip = gameAudioclip;
            gameAudio.Play();
        }
    }
}