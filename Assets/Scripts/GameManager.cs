using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Text variables!
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI bestTimeText;


    // Managers variables!
    private PlayerController playerController;
    private AudioManager audioManager;
    public UIManager uiManager;


    // Game object variables!
    public GameObject pauseMenuScreen;
    public GameObject gameOverMenuScreen;


    // Transform variables!
    public Transform startingPoint;


    // Animator variables!
    public Animator animator;


    // Float variables!
    private readonly float speed = 10f;
    public float time;


    // Int variables!
    public int bestScore;
    public int bestTime;


    // Game state variables!
    public GameState currentState; //= GameState.Playing;

    public bool gameIsOver = false;


    private void Awake()
    {
        //currentState = GameState.Playing;
        state = GameState1.Playing;

        // Assign playerController first
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        // Assign other components and perform null checks
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        animator = GetComponent<Animator>();

        // Check other null references
        CheckPlayerControllerNullReference();

        // Load best score and time
        LoadBestScoreOnStartGame();
        LoadBestTimeOnStartGame();

        // Update best score and time
        UpdateBestTime(bestTime);
        UpdateBestScore(bestScore);

        // Check for null bestTimeText
        if (bestTimeText == null)
        {
            Debug.Log("bestTimeText is null in Awake!");
        }

        Debug.Log(currentState + "Calling awake!");
    }

    private void Update()
    {
        /**switch (currentState)
        {
            case GameState.Playing:
                Debug.Log("Entering playing state!");
                uiManager.InputForPauseMenuScreen();
                Time.timeScale = 1f;
                UpdateTimer();
                break;

            case GameState.Paused:
                //uiManager.InputForPauseMenuScreen();
                //Time.timeScale = 0f;
                break;

            case GameState.GameOver:
                Debug.Log("Entering game over state!");
                GameOver();
                break;
        }*/


       

        if (gameIsOver == false)
        {
            UpdateTimer1();
        }
    }

    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
    }

    public enum GameState1
    {
        Playing,
        Paused,
        GameOver,
    }
    public GameState1 state;



    public void CheckPlayerControllerNullReference()
    {
        if (playerController != null)
        {
            playerController.playerRb.AddForce(Vector2.up * playerController.startJumpForce, ForceMode2D.Impulse);
            StartCoroutine(PlayIntro());
            Debug.Log("The game started!");
        }

        else
        {
            Debug.LogError($"{name} PlayerController not found!", gameObject);
        }
    }


    // We use an IEnumerator to make out starting point animation 
    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerController.transform.position;
        Vector3 endPos = startingPoint.position;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (startTime - startTime) * speed;
        float fractionOfJourney = distanceCovered / journeyLength;

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distanceCovered / journeyLength;

            if (playerController != null)
            {
                playerController.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            }

            else
            {
                Debug.LogError($"{name} PlayerController not found!", gameObject);
            }
            yield return null;
        }
    }

    public void GameOver()
    {
        gameOverMenuScreen.SetActive(true);
        audioManager.mainGameMusicAudioSource.Stop();
        //state = GameState1.GameOver;
        gameIsOver = true;
    }


    public void PauseGame()
    {
        if (currentState == GameState.Playing)
        {
            //currentState = GameState.Paused;
            pauseMenuScreen.SetActive(true);
            audioManager.mainGameMusicAudioSource.Pause();
            audioManager.PressButtonSound();
            Time.timeScale = 0f;
            Debug.Log("Pause menu screen is enabled after delay!");
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            //currentState = GameState.Playing;
            pauseMenuScreen.SetActive(false);
            audioManager.mainGameMusicAudioSource.UnPause();
            audioManager.PressButtonSound();
            Time.timeScale = 1f;
            Debug.Log("Pause menu screen is disabled after delay");
        }
    }

    public void UpdateTimer1()
    {
        Debug.Log(state);

        //if (state == GameState1.Playing)
        //{
            if (timerText != null)
            {
                time += Time.deltaTime;
                int minutes = Mathf.FloorToInt(time / 60);
                int seconds = Mathf.FloorToInt(time % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                timerText.text = "Timer: " + timerText.text;
               
            }

            else
            {
                Debug.Log("Timer text is null and time doesn't change!", timerText);
            }
        //}
    }

    public void CheckSaveBestScore()
    {
        if (playerController.score > bestScore)
        {
            bestScore = playerController.score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            Debug.Log("Best score is: " + bestScore);
            Debug.Log("Checked and saved best score!");
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
            Debug.Log("Best time is: " + bestTime);
            Debug.Log("Checked and saved best score!");
            UpdateBestTime(bestTime);
        }
        bestTimeText.text = bestTime.ToString();
    }

    public void UpdateBestTime(int bestTime)
    {
        if (bestTimeText != null)
        {
            int minutes = Mathf.FloorToInt(bestTime / 60);
            int seconds = Mathf.FloorToInt(bestTime % 60);
            bestTimeText.text = "Best Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
            Debug.Log("Best time updates: " + bestTime);
        }

        else
        {
            Debug.LogError($"{name}bestTimeText is null in UpdateBestTime!", gameObject);
        }
    }

    public void UpdateBestScore(int bestScore)
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best Score: " + bestScore;
            Debug.Log("Best score updates: " + bestScore);
        }

        else
        {
            Debug.LogError($"{name}Best score text not found!", gameObject);
        }
    }


    public void LoadBestScoreOnStartGame()
    {
        bestScore = PlayerPrefs.GetInt("BestScore");
        UpdateBestScore(bestScore);
        Debug.Log("Loading of the best score value: " + bestScore);
    }

    public void LoadBestTimeOnStartGame()
    {
        bestTime = PlayerPrefs.GetInt("BestTime");
        UpdateBestTime(bestTime);
        Debug.Log("Loading of the best time value: " + bestTime);
    }
}





