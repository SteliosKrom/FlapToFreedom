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

    public GameObject gameOverMenuScreen;
    public GameObject pauseMenuScreen;


    [Header("GAMEPLAY")]
    public Transform startingPoint;


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
    private void Awake()
    {
        Instance = this;  
    }
    

    public void GameOver()
    {
        currentState = GameState.GameOver;
        gameOverMenuScreen.SetActive(true);
        AudioManager.Instance.PlaySound(playerController.gameOverAudioSource, playerController.gameOverAudioClip);
        mainGameMusicAudioSource.Stop();
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
}
