using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("GAMEPLAY")]
    private readonly float continueButtondelay = 0f;
    private readonly float homeButtonDelay = 0.1f;
    private readonly float restartButtonDelay = 0.2f;
    private readonly float quitButtonDelay = 0.1f;

    [Header("AUDIO SOURCES")]
    public AudioSource onPointerEnterAudioSource;
    public AudioSource pressButtonSoundAudioSource;

    [Header("AUDIO CLIPS")]
    public AudioClip onPointerEnterAudioClip;
    public AudioClip pressButtonSoundAudioClip;
    

    public TextMeshProUGUI ScoreText { get => scoreText; set => scoreText = value; }
    private void Start()
    {
        
    }

    private void Update()
    {
        InputForPauseMenuScreen();
    }

  

    public void InputForPauseMenuScreen()
    {
        // Check the current game state!
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
        AudioManager.Instance.PlaySound(RoundManager.Instance.PressButtonSoundAudioSource, RoundManager.Instance.PressButtonSoundAudioClip);
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

    public void ContinueGameButton()
    {
        StartCoroutine(ContinueGameAfterDelay());
    }


    IEnumerator ContinueGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(continueButtondelay);
        RoundManager.Instance.ResumeGame();
        Debug.Log("Continue button is pressed and pause menu screen gone!");
    }


    public void HomeBlackButton()
    {
        AudioManager.Instance.PlaySound(RoundManager.Instance.PressButtonSoundAudioSource, RoundManager.Instance.PressButtonSoundAudioClip);
        StartCoroutine(HomeBlackButtonAfterDelay());
    }

    IEnumerator HomeBlackButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(homeButtonDelay);
        SceneManager.LoadScene("MainMenuScene");     
        Debug.Log("Main menu scene is loaded and home black button is pressed!");
    }

    public void RestartButton()
    {
        AudioManager.Instance.PlaySound(RoundManager.Instance.PressButtonSoundAudioSource, RoundManager.Instance.PressButtonSoundAudioClip);
        StartCoroutine(RestartButtonAfterDelay());
    }

    IEnumerator RestartButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(restartButtonDelay);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        Debug.Log("Restart button pressed and loaded main game scene and game over menu scree gone!");
    }



    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }

}
