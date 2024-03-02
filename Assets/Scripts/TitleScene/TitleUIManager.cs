using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public bool isPressed = false;

    // Initialization of some variables here!
    public TextMeshProUGUI pressAnyKeyToPlayTest;
    public float blinkInterval = 1.0f;
    public float timer;

    private AudioManager audioManager;  //use instance reference instead

    private void Start()
    {
        timer = 0f;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkInterval)
        {
            pressAnyKeyToPlayTest.enabled = !pressAnyKeyToPlayTest.enabled;
            timer = 0f;
            Debug.Log("The text is actually blinking!");
        }

        PressLeftMouseButtonToStartAndLoadMainMenuScene();
    }

    public void PressLeftMouseButtonToStartAndLoadMainMenuScene()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isPressed)
        {
            isPressed = true;
            SceneManager.LoadScene("MainMenuScene");
            // audioManager.PressButtonSound();
            Debug.Log("Main menu scene loads!");
        }

        else
        {
            isPressed = false;
        }
    }
}
