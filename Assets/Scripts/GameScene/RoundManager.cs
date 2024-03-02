using TMPro;
using UnityEngine;

public enum GameState
{
    Intro,
    Playing,
    Pause,
    GameOver
}

public class RoundManager : MonoBehaviour
{
    [Header("MANAGERS")]
    public static RoundManager Instance;
    [SerializeField] private PlayerController playerController;


    [Header("UI")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI bestTimeText;
    public GameObject gameOverMenuScreen;
    public GameObject pauseMenuScreen;


    [Header("GAMEPLAY")]
    public Transform startingPoint;
    [SerializeField] private readonly float speed = 10f;
    public float time;
    private float timer = 0f;
    private readonly float interval = 1f;
    public int bestScore;
    public int bestTime;
    [SerializeField] private bool isGameOver;


    [Header("STATES")]
    public GameState currentState;


    [Header("AUDIO SOURCES")]
    [SerializeField] private AudioSource mainGameMusicAudioSource;
    [SerializeField] private AudioSource pressButtonSoundAudioSource;

    [Header("AUDIO CLIPS")]
    [SerializeField] private AudioClip mainGameMusicAudioClip;
    [SerializeField] private AudioClip pressButtonSoundAudioClip; 


    public AudioSource MainGameMusicAudioSource { get => mainGameMusicAudioSource; set => mainGameMusicAudioSource = value; }
    public PlayerController PlayerController { get => playerController; set => playerController = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    private void Awake()
    {
        Instance = this;

        LoadBestScoreOnStartGame();
        LoadBestTimeOnStartGame();
        UpdateBestTime(bestTime);
        UpdateBestScore(bestScore);
    }

    private void Start()
    {
        isGameOver = false;
    }

    private void LateUpdate()
    {
        if (currentState == GameState.Playing)
        {
            TimeScore();
        }
    }
    public void TimeScore()
    {
        Debug.Log("Game Over bool: " + RoundManager.Instance.IsGameOver);
        if (!RoundManager.Instance.isGameOver)
        {
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                time += 1;
                timer = 0f;
            }
            timerText.text = "Timer: " + time.ToString();
            Debug.Log("Currently Playing");
        }
        else
        {
            Debug.Log("Currently Game Over");
        }
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        gameOverMenuScreen.SetActive(true);
        AudioManager.Instance.PlaySound(playerController.gameOverAudioSource, playerController.gameOverAudioClip);
        isGameOver = true;
    }

    public void PauseGame()
    {
        pauseMenuScreen.SetActive(true);
        mainGameMusicAudioSource.Pause();
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        Time.timeScale = 0f;
        currentState = GameState.Pause;

    }

    public void ResumeGame()
    {
        pauseMenuScreen.SetActive(false);
        mainGameMusicAudioSource.UnPause();
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        Time.timeScale = 1f;
        currentState = GameState.Playing;
    }

    /**public void UpdateTimer()
    {
        //time += Time.deltaTime;
        //int minutes = Mathf.FloorToInt(time / 60);
        //int seconds = Mathf.FloorToInt(time % 60);
        //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //timerText.text = "Timer: " + timerText.text;         
    }**/

    public void CheckSaveBestScore()
    {
        if (playerController.score > bestScore)
        {
            bestScore = playerController.score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            UpdateBestScore(bestScore);
        }
        bestScoreText.text = bestScore.ToString();
    }

    public void CheckSaveBestTime()
    {
        if (time > bestTime)
        {
            bestTime = (int)time;
            PlayerPrefs.SetInt("BestTime", bestTime);
            PlayerPrefs.Save();
            UpdateBestTime(bestTime);
        }
        bestTimeText.text = bestTime.ToString();
    }

    public void UpdateBestTime(int bestTime)
    {
        int minutes = Mathf.FloorToInt(bestTime / 60);
        int seconds = Mathf.FloorToInt(bestTime % 60);
        bestTimeText.text = "Best Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateBestScore(int bestScore)
    {
        bestScoreText.text = "Best Score: " + bestScore;
    }

    public void LoadBestScoreOnStartGame()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        UpdateBestScore(bestScore);
    }

    public void LoadBestTimeOnStartGame()
    {
        bestTime = PlayerPrefs.GetInt("BestTime");
        UpdateBestTime(bestTime);
    }
}
