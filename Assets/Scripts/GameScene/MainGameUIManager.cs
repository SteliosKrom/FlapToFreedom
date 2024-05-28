using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainGameUIManager : MonoBehaviour
{
    public AudioMixer myAudioMixer;
    public PlayerController playerController;

    public GameObject optionsMenu;
    public GameObject mainGame;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("GAMEPLAY")]
    private readonly float quitDelay = 0.5f;
    private readonly float delay = 0.2f;
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
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        optionsMenu.SetActive(false);
        mainGame.SetActive(true);
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
            timerText.text = "TIMER: " + timerText.text;
        }
    }

    public void ShowScoreOnStart()
    {
        playerController.score = 0;
        ScoreText.text = "SCORE: " + playerController.score.ToString();
    }

    public void UpdateScore(int increment = 1)
    {
        playerController.score += increment;
        ScoreText.text = "SCORE: " + playerController.score.ToString();
    }

    public void InputForPauseMenuScreen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (RoundManager.Instance.currentState == GameState.Playing)
            {
                RoundManager.Instance.PauseGame();
                playerController.informPlayerForPowerUp.SetActive(false);
                playerController.informPlayerIncreasePowerUp.SetActive(false);
                playerController.plusOneScoreGameObject.SetActive(false);
                playerController.plusTwoScoreGameObject.SetActive(false);
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
        yield return new WaitForSecondsRealtime(quitDelay);
        Application.Quit();
        Debug.Log("Game quits and editor play mode stops and our values reset to default!");
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
        mainGame.SetActive(false);
        optionsMenu.SetActive(true);
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
        mainGame.SetActive(true);
    }

    public void OnPointerEnter()
    {
        AudioManager.Instance.PlaySound(onPointerEnterAudioSource, onPointerEnterAudioClip);
    }
}
