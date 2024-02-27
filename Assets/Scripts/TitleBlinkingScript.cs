using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class TitleBlinkingScript : MonoBehaviour
{
    // Initialization of some variables here!
    public TextMeshProUGUI pressAnyKeyToPlayTest;
    public float blinkInterval = 1.0f;
    public float timer;


    // Start is called before the first frame update
    void Start()
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
            Debug.Log("The text is actually blinking!");
        }
    }
}
