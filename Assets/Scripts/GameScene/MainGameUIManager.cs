using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainGameUIManager : MonoBehaviour
{
    public AudioMixer myAudioMixer;
    public PlayerController playerController;

    [Header("GAME OBJECTS")]
    public List<GameObject> treeLogsList;
    public List<GameObject> gemParticleList;
    public GameObject optionsMenu;
    public GameObject mainGame;
    public GameObject groundLayers;
    public GameObject backgroundLayers;
    public GameObject mainMenu;
    public GameObject player;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;

    [Header("GAMEPLAY")]
    private readonly float delay = 0.1f;
    private float timer = 0f;
    public float goBackDelay = 0.1f;

    [Header("AUDIO SOURCES")]
    public AudioSource onPointerEnterAudioSource;
    public AudioSource pressButtonSoundAudioSource;

    [Header("AUDIO CLIPS")]
    public AudioClip onPointerEnterAudioClip;
    public AudioClip pressButtonSoundAudioClip;

    public TextMeshProUGUI ScoreText { get => scoreText; set => scoreText = value; }

    private void Awake()
    {
        mainMenu.SetActive(false);
    }
    private void Update()
    {
        if (RoundManager.Instance.currentState == GameState.Playing)
        {
            TimeScore();
        }
        InputForPauseMenuScreen();
    }

    public void TimeScore()
    {
        if (RoundManager.Instance.currentState != GameState.GameOver)
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = "Timer: " + timerText.text;
        }
    }

    public void ShowScoreOnStart()
    {
        playerController.score = 0;
        ScoreText.text = "Score: " + playerController.score.ToString();
    }

    public void UpdateScore()
    {
        playerController.score++;
        ScoreText.text = "Score: " + playerController.score;
    }

    public void InputForPauseMenuScreen()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.Instance.currentState != GameState.GameOver && RoundManager.Instance.currentState != GameState.OnSettings)
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
        yield return new WaitForSecondsRealtime(delay);

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
        yield return new WaitForSecondsRealtime(delay);
        RoundManager.Instance.ResumeGame();
    }


    public void HomeBlackButton()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(HomeBlackButtonAfterDelay());
    }

    IEnumerator HomeBlackButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void RestartButton()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(RestartButtonAfterDelay());
    }

    IEnumerator RestartButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

    public void LoadSettingsButton()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(LoadSettingsButtonAfterDelay());
    }

    IEnumerator LoadSettingsButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        RoundManager.Instance.currentState = GameState.OnSettings;
        SetObjectsInactiveActive(false, false, false, false, false, true);

        foreach (GameObject treeLog in treeLogsList)
        {
            treeLog.SetActive(false);
        }
        foreach (GameObject gemParticle in gemParticleList)
        {
            if (gemParticle != null)
            {
                gemParticle.SetActive(false);
            }  
        }
    }

    public void SetObjectsInactiveActive(bool mainGameActive, bool backgroundLayersActive, bool groundLayersActive, bool playerActive, bool mainMenuActive, bool optionsMenuActive)
    {
        optionsMenu.SetActive(optionsMenuActive);
        mainGame.SetActive(mainGameActive);
        mainMenu.SetActive(mainMenuActive);
        backgroundLayers.SetActive(backgroundLayersActive);
        groundLayers.SetActive(groundLayersActive);
        player.SetActive(playerActive);
    }

    public void GoBackToGameButton()
    {
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        StartCoroutine(GoBackToGameButtonAfterDelay());
    }

    IEnumerator GoBackToGameButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(goBackDelay);
        RoundManager.Instance.currentState = GameState.Pause;
        optionsMenu.SetActive(false);
        SetObjectsActive(true, true, true, true, false, false, true);

        foreach (GameObject treeLog in treeLogsList)
        {
            treeLog.SetActive(true);
        }
        foreach (GameObject gemParticle in gemParticleList)
        {
            if (gemParticle != null)
            {
                gemParticle.SetActive(true);
            }
        }
    }

    public void SetObjectsActive(bool pauseMenuScreenActive, bool groundLayersActive, bool backgroundLayersActive, bool mainGameActive, bool mainMenuActive, bool optionsMenuActive, bool playerActive)
    {
        RoundManager.Instance.pauseMenuScreen.SetActive(pauseMenuScreenActive);
        mainGame.SetActive(mainGameActive);
        optionsMenu.SetActive(optionsMenuActive);
        backgroundLayers.SetActive(backgroundLayersActive);
        groundLayers.SetActive(groundLayersActive);
        player.SetActive(playerActive);
    }

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }
}
