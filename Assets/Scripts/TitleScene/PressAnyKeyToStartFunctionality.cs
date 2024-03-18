using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PressAnyKeyToStartFunctionality : MonoBehaviour
{
    public bool isPressed = false;

    // Update is called once per frame
    void Update()
    {
        PressAnyKeyToStartAndLoadMainMenuScene();
    }

    public void PressAnyKeyToStartAndLoadMainMenuScene()
    {
        if (Input.anyKeyDown && !isPressed)
        {
            isPressed = true;
            SceneManager.LoadScene("MainMenuScene");
            Debug.Log("Main menu scene loads!");
        }

        else
        {
            isPressed = false;
        }
    }
}
