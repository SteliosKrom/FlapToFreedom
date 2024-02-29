using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameUIManager : MonoBehaviour
{
    [Header("GAMEPLAY")]
    private float delay = 1.0f;

    [Header("MANAGERS")]
    public AudioManager audioManager;
    public RoundManager roundManager;

    [Header("AUDIO SOURCES")]
    public AudioSource mainGameMusicAudioSource;


    [Header("AUDIO CLIPS")]
    public AudioClip mainGameMusicAudioClip;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        roundManager = GameObject.Find("RoundManager").GetComponent<RoundManager>();
    }

    private void Update()
    {
        InputForPauseMenuScreen();
    }

    public void InputForPauseMenuScreen()
    {
        // Check the current game state!
        if (roundManager != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (true)
                {
                    roundManager.PauseGame();
                }

                else
                {
                    roundManager.PauseGame();
                }
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
        RoundManager.Instance.mainGameMusicAudioSource.UnPause();
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
        // audioManager.onPointerEnterAudioSource.PlayOneShot(audioManager.onPointerEnterAudioClip);
    }

}
