using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Intro,
    Playing,
    Pause,
    GameOver,
    OnSettings
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

    private void Start()
    {
        Cursor.visible = false;
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        gameOverMenuScreen.SetActive(true);
        AudioManager.Instance.PlaySound(playerController.gameOverAudioSource, playerController.gameOverAudioClip);
        MainGameMusicAudioSource.Stop();
        PlayerController.plusOneScoreGameObject.SetActive(false);
        PlayerController.plusTwoScoreGameObject.SetActive(false);
        PlayerController.informPlayerForPowerUp.SetActive(false);
        PlayerController.informPlayerIncreasePowerUp.SetActive(false);
        PlayerController.powerUpIncreaseScoreByTwoParticle.SetActive(false);
        PlayerController.powerUpParticle.SetActive(false);
        PlayerController.gemParticle.SetActive(false);
        PlayerController.collisionParticle.SetActive(false);
        Cursor.visible = true;
    }

    public void PauseGame()
    {
        pauseMenuScreen.SetActive(true);
        mainGameMusicAudioSource.Pause();
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        Time.timeScale = 0f;
        currentState = GameState.Pause;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenuScreen.SetActive(false);
        mainGameMusicAudioSource.UnPause();
        AudioManager.Instance.PlaySound(pressButtonSoundAudioSource, pressButtonSoundAudioClip);
        Time.timeScale = 1f;
        currentState = GameState.Playing;
        Cursor.visible = false;
    }    
}
