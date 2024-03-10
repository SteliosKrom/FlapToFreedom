using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainGameUIManager : MonoBehaviour
{
    public AudioMixer myAudioMixer;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("GAMEPLAY")]
    private readonly float quitButtonAfterDelay = 0.1f;
    private readonly float resumeButtonDelay = 0.1f;
    private readonly float homeButtonDelay = 0.1f;
    private readonly float restartButtonDelay = 0.2f;

    [Header("AUDIO SOURCES")]
    public AudioSource onPointerEnterAudioSource;
    public AudioSource pressButtonSoundAudioSource;

    [Header("AUDIO CLIPS")]
    public AudioClip onPointerEnterAudioClip;
    public AudioClip pressButtonSoundAudioClip;

    public TextMeshProUGUI ScoreText { get => scoreText; set => scoreText = value; }

    private void Update()
    {
        InputForPauseMenuScreen();
    }

    public void InputForPauseMenuScreen()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.Instance.currentState != GameState.GameOver)
        {
            if (RoundManager.Instance.currentState == GameState.Playing)   
            {
                RoundManager.Instance.PauseGame();
            }
            else if (RoundManager.Instance.currentState == GameState.Pause) 
            {
                RoundManager.Instance.ResumeGame();
            }
        }
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(QuitAfterDelay());
    }

    IEnumerator QuitAfterDelay()
    {
        yield return new WaitForSecondsRealtime(quitButtonAfterDelay);

        PlayerPrefs.SetFloat("MasterVolume", 1.0f);
        PlayerPrefs.SetFloat("SoundsVolume", 1.0f);
        PlayerPrefs.SetFloat("MenuMusicVolume", 1.0f);
        PlayerPrefs.SetInt("QualityDropdownValue", 0);

        Application.Quit();
    }

    public void ResumeGameButton()
    {
        StartCoroutine(ResumeGameButtonAfterDelay());
    }


    IEnumerator ResumeGameButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(resumeButtonDelay);
        RoundManager.Instance.ResumeGame();
    }


    public void HomeBlackButton()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(HomeBlackButtonAfterDelay());
    }

    IEnumerator HomeBlackButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(homeButtonDelay);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void RestartButton()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(RestartButtonAfterDelay());
    }

    IEnumerator RestartButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(restartButtonDelay);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

    /*public void LoadSettingsButton()
    {
        StartCoroutine(LoadSettingsButtonAfterDelay());
    }

    IEnumerator LoadSettingsButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(settingsButtonDelay);
    }*/

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }

}
