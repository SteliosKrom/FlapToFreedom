using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    private readonly float quitButtonDelay = 0.1f;
    private readonly float playButtonDelay = 0.2f;
    private readonly float optionsButtonDelay = 0.2f;

    [Header("GAMEOBJECTS")]
    public GameObject mainMenu;
    public GameObject optionsMenu;

    [Header("AUDIO SOURCES")]
    public AudioSource onPointerEnterAudioSource;
    public AudioSource pressButtonSoundAudioSource;


    [Header("AUDIO CLIPS")]
    public AudioClip onPointerEnterAudioClip;
    public AudioClip pressButtonSoundAudioClip;


    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
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
    }

    public void LoadOptionsScene()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(LoadOptionsSceneAfterDelay());
    }

    IEnumerator LoadOptionsSceneAfterDelay()
    {
        yield return new WaitForSecondsRealtime(optionsButtonDelay);
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void LoadMainGameScene()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(LoadMainGameAfterDelay());
    }

    IEnumerator LoadMainGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(playButtonDelay);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        Debug.Log("Play Button is pressed after a delay!");
    }

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }
}
