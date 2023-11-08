using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public Transform cam;
    public Transform[] cameraPositions;
    public float smoothSpeed = 0.5f;
    public bool gameStarted;
    public Transform[] doors;
    private Quaternion targetRotation = Quaternion.Euler(0, 90, 0);

    public GameObject mainUI;
    public GameObject creditsUI;
    public GameObject optionsUI;

    public Animator animUI;

    public void Start()
    {
        animUI.SetBool("StartGame", false);
        mainUI.SetActive(true);
        creditsUI.SetActive(false);
        optionsUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void Update()
    {
        if(creditsUI.activeSelf == true)
        {
            cam.transform.position = Vector3.Lerp(cam.position, cameraPositions[1].position, smoothSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.RotateTowards(cam.rotation, cameraPositions[1].rotation, smoothSpeed * Time.deltaTime);
        }
        if(mainUI.activeSelf == true)
        {
            cam.transform.position = Vector3.Lerp(cam.position, cameraPositions[2].position, smoothSpeed * Time.deltaTime);
            cam.transform.rotation = Quaternion.RotateTowards(cam.rotation, cameraPositions[2].rotation, smoothSpeed * Time.deltaTime);
        }
        if (gameStarted == true)
        {
            StartGame();
            StartCoroutine(StartGameDelay());
        }
    }

    public void StartGame()
    {
        gameStarted = true;        
        cam.transform.position = Vector3.Lerp(cam.position, cameraPositions[0].position, smoothSpeed * Time.deltaTime);
        OpenDoor();
    }

    IEnumerator StartGameDelay()
    {
        mainUI.SetActive(false);
        yield return new WaitForSeconds(1);
        animUI.SetBool("StartGame", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public void OpenDoor()
    {
        doors[0].transform.rotation = Quaternion.RotateTowards(doors[0].rotation, targetRotation, -40f * Time.deltaTime);
        doors[1].transform.rotation = Quaternion.RotateTowards(doors[1].rotation, targetRotation, 40f * Time.deltaTime);
    }

    public void CreditUI()
    {
        mainUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void StartUI()
    {
        mainUI.SetActive(true);
        creditsUI.SetActive(false);  
        optionsUI.SetActive(false);
    }

    public void OptionsUI()
    {
        mainUI.SetActive(false);
        optionsUI.SetActive(true);
    }
}