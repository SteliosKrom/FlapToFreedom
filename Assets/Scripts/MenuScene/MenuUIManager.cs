using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [Header("GAMEPLAY")]
    public float delay = 1.0f;

    [Header("MANAGERS")]
    public AudioManager audioManager;

    [Header("AUDIO SOURCES")]
    public AudioSource onPointerEnterAudioSource;

    [Header("AUDIO CLIPS")]
    public AudioClip onPointerEnterAudioClip;

    private void Start()
    {

    }

    public void QuitGame()
    {
        StartCoroutine(QuitAfterDelay());
    }


    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);

        PlayerPrefs.SetFloat("MasterVolume", 1.0f);
        PlayerPrefs.SetFloat("SoundsVolume", 1.0f);
        PlayerPrefs.SetFloat("MenuMusicVolume", 1.0f);

        PlayerPrefs.SetInt("QualityDropdownValue", 0);

        Application.Quit();
        Debug.Log("Game quits and editor play mode stops and our values reset to default!");
    }

    public void LoadOptionsScene()
    {
        StartCoroutine(LoadOptionsSceneAfterDelay());
        Debug.Log("Options scene is being loaded!");
    }



    IEnumerator LoadOptionsSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("OptionsScene");
        Debug.Log("Options Button is pressed after a delay!");
    }

    public void LoadMainGameScene()
    {
        StartCoroutine(LoadMainGameAfterDelay());
        Destroy(GameObject.FindWithTag("MainMenuBackgroundMusic"));
    }


    IEnumerator LoadMainGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        Debug.Log("Play Button is pressed after a delay!");
    }

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }
}
