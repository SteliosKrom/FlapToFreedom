using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public float blinkInterval = 1.0f;
    public float timer;
    public bool isPressed = false;

    [Header("UI")]
    public TextMeshProUGUI pressAnyKeyToPlayTest;

    private void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= blinkInterval)
        {
            pressAnyKeyToPlayTest.enabled = !pressAnyKeyToPlayTest.enabled;
            timer = 0f;
        }
        PressLeftMouseButtonToStartAndLoadMainMenuScene();
    }

    public void PressLeftMouseButtonToStartAndLoadMainMenuScene()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isPressed)
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
