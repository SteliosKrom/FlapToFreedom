using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameUIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider gameVolumeSlider;
    [SerializeField] private TextMeshProUGUI gameVolumeSliderText;

    [Header("GAMEPLAY")]
    private float delay = 1.0f;

    [Header("MANAGERS")]
    public AudioManager audioManager;   //remove refenrece use instance
    public RoundManager roundManager;  //remove reference use instance

    [Header("AUDIO")]
    public AudioMixer myAudioMixer;
    [SerializeField] private AudioSource onPointerEnterAudioSource;
    [SerializeField] private AudioClip onPointerEnterAudioClip;
    const string gameMusicVol = "GameMusicVolume";


    private void Update()
    {
        InputForPauseMenuScreen();
    }

    public void CheckGameVolumeSliderNullReference()
    {
        if (gameVolumeSlider != null)
        {
            gameVolumeSlider.value = 1.0f;
        }
    }

    public void GameVolumeSlider()
    {
        float gameVolume = gameVolumeSlider.value;
        gameVolumeSliderText.text = gameVolume.ToString("0.0");
        myAudioMixer.SetFloat(gameMusicVol, Mathf.Log10(gameVolumeSlider.value) * 20);
    }

    public void InputForPauseMenuScreen()
    {
        // Check the current game state!
        if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.Instance.currentState != GameState.GameOver)
        {
            if (true)   //check for playing state || intro
            {
                roundManager.PauseGame();
            }
            else //make if else check for paused state
            {
                roundManager.ResumeGame();
            }
        }
        
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

    public void ContinueGameButton()
    {
        StartCoroutine(ContinueGameAfterDelay());
    }


    IEnumerator ContinueGameAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        RoundManager.Instance.pauseMenuScreen.SetActive(false);
        RoundManager.Instance.MainGameMusicAudioSource.UnPause();
        Debug.Log("Continue button is pressed and pause menu screen gone!");
    }


    public void HomeBlackButton()
    {
        StartCoroutine(HomeBlackButtonAfterDelay());
    }

    IEnumerator HomeBlackButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("MainMenuScene");
        Debug.Log("Main menu scene is loaded and home black button is pressed!");
    }

    public void RestartButton()
    {
        StartCoroutine(RestartButtonAfterDelay());
    }

    IEnumerator RestartButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        Debug.Log("Restart button pressed and loaded main game scene and game over menu scree gone!");
    }

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }

}