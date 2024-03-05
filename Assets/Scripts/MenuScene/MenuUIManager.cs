using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [Header("GAMEOBJECTS")]
    public GameObject mainMenu;
    public GameObject optionsMenu;

    [Header("GAMEPLAY")]
    private readonly float quitButtonDelay = 0.2f;
    private readonly float settingsButtonDelay = 0.1f;
    private readonly float playButtonDelay = 0.1f;

    [Header("AUDIO SOURCES")]
    public AudioSource onPointerEnterAudioSource;
    public AudioSource pressButtonAudioSource;

    [Header("AUDIO CLIPS")]
    public AudioClip onPointerEnterAudioClip;
    public AudioClip pressButtonAudioClip;


    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySound(pressButtonAudioSource, pressButtonAudioClip);
        StartCoroutine(QuitAfterDelay());
    }


    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(quitButtonDelay);

        PlayerPrefs.SetFloat("MasterVolume", 1.0f);
        PlayerPrefs.SetFloat("SoundsVolume", 1.0f);
        PlayerPrefs.SetFloat("MenuMusicVolume", 1.0f);

        PlayerPrefs.SetInt("QualityDropdownValue", 0);

        Application.Quit();
        Debug.Log("Game quits and editor play mode stops and our values reset to default!");
    }

    public void LoadSettingsScene()
    {
        AudioManager.Instance.PlaySound(pressButtonAudioSource, pressButtonAudioClip);
        StartCoroutine(LoadSettingsAfterDelay());
    }

    IEnumerator LoadSettingsAfterDelay()
    {
        yield return new WaitForSecondsRealtime(settingsButtonDelay);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void LoadMainGameScene()
    {
        AudioManager.Instance.PlaySound(pressButtonAudioSource, pressButtonAudioClip);
        StartCoroutine(LoadMainGameAfterDelay());
    }

    IEnumerator LoadMainGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(playButtonDelay);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }
}
