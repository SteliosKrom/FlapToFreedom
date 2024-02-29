using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PressAnyKeyToStartFunctionality : MonoBehaviour
{
    public bool isPressed = false;

    private AudioManager audioManager;  //use instance reference instead

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
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
