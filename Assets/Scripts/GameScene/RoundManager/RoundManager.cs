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
    public static RoundManager Instance;
    [Header("Gameplay")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] TextMeshProUGUI timerText;
    public Transform startingPoint;
    private readonly float speed = 10f;
    public float time;
    private float timer = 0f;
    private float interval = 1f;
    public int bestScore;
    public int bestTime;

    public GameState currentState;

    public PlayerController PlayerController { get => playerController; set => playerController = value; }

    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        if(currentState == GameState.Playing)
        {
            TimeScore();
        }
    }
    public void TimeScore()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            time += 1;
            timer = 0f;
        }
        timerText.text = "Timer: " + time.ToString();      
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        //  gameOverMenuScreen.SetActive(true);
        // audioManager.mainGameMusicAudioSource.Stop();

    }
}
