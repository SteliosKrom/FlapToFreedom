using System;
using System.Collections;
using TMPro;
using UnityEngine;

//REMOVE THIS COMPLETELY
public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    public static GameManager instance;
    private PlayerController playerController;
    private AudioManager audioManager;

    [Header("UI")]
    public UIManager uiManager;


    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI bestTimeText;

    [Header("Gameplay")]
    public Transform startingPoint;
    private readonly float speed = 10f;
    public float time;
    private float timer = 0f;
    private float interval = 1f;
    public int bestScore;
    public int bestTime;
    [SerializeField] private bool isGameOver;

    [Header("Effects")]
    public Animator animator;

    #region Getters and Setters
    public PlayerController PlayerController { get => playerController; set => playerController = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }
    #endregion

    private void Awake()
    {
        instance = this;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        animator = GetComponent<Animator>();

        AddForceToPlayer();

        LoadBestScoreOnStartGame();
        LoadBestTimeOnStartGame();

        // Update best score and time
        UpdateBestTime(bestTime);
        UpdateBestScore(bestScore);
    }

    private void Start()
    {
        isGameOver = false;
    }

    private void LateUpdate()
    {
        //TimeScore();
    }

    public void AddForceToPlayer()
    {
       // playerController.playerRb.AddForce(Vector2.up * playerController.startJumpForce, ForceMode2D.Impulse);
       // StartCoroutine(PlayIntro());
    }


    // We use an IEnumerator to make out starting point animation 


    public void GameOver()
    {
        Debug.Log("Game Over Was Played");
      //  gameOverMenuScreen.SetActive(true);
       // audioManager.mainGameMusicAudioSource.Stop();
        isGameOver = true;
    }


    public void PauseGame()
    {
     //   pauseMenuScreen.SetActive(true);
      //  audioManager.mainGameMusicAudioSource.Pause();
       // audioManager.PressButtonSound();
    }

    public void ResumeGame()
    {
      //  pauseMenuScreen.SetActive(false);
      //  audioManager.mainGameMusicAudioSource.UnPause();
     //   audioManager.PressButtonSound();
    }

    public void UpdateTimer()
    {
        // time += Time.deltaTime;
        // int minutes = Mathf.FloorToInt(time / 60);
        // int seconds = Mathf.FloorToInt(time % 60);
        // timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // timerText.text = "Timer: " + timerText.text;            
    }

    public void TimeScore()
    {
        Debug.Log("Game Over bool: " + isGameOver);
        if (!isGameOver)
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

    public void CheckSaveBestScore()
    {
        if (playerController.score > bestScore)
        {
            bestScore = playerController.score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            //  Debug.Log("Best score is: " + bestScore);
            //  Debug.Log("Checked and saved best score!");
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
            //  Debug.Log("Best time is: " + bestTime);
            //  Debug.Log("Checked and saved best score!");
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
        //  Debug.Log("Loading of the best score value: " + bestScore);
    }

    public void LoadBestTimeOnStartGame()
    {
        bestTime = PlayerPrefs.GetInt("BestTime");
        UpdateBestTime(bestTime);
    }
}





