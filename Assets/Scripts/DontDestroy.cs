using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class DontDestroy : MonoBehaviour
{
    // Variables initialization
    private void Awake()
    {
        GameObject[] mainMenuBackgroundMusic = GameObject.FindGameObjectsWithTag("MainMenuBackgroundMusic");

        if (mainMenuBackgroundMusic.Length > 1)
        {
            Destroy(this.gameObject);
            Debug.Log("Main menu background music game object is destroyed on load!");
        }

        else
        {
            DontDestroyOnLoad(this.gameObject);
            Debug.Log("Main menu background music game object is not destroyed on load!");
        }
    }
}
