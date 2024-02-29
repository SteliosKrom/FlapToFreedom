using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsUIManager : MonoBehaviour
{
    private float goBackDelay = 0.1f;

    public AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();    
    }

    public void GoBackButton()
    {
        StartCoroutine(GoBackToMainMenuSceneAfterDelay());
    }

    IEnumerator GoBackToMainMenuSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(goBackDelay);
        SceneManager.LoadScene("MainMenuScene");
        Debug.Log("Left back button is pressed after a delay and back button is pressed!");
    }

    public void OnPointerEnter()
    {
        // audioManager.onPointerEnterAudioSource.PlayOneShot(audioManager.onPointerEnterAudioClip);
    }
}
