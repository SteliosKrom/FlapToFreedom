using System;
using System.Collections;
using TMPro;
using UnityEngine;

//REMOVE THIS COMPLETELY
public class GameManager : MonoBehaviour
{
    [Header("MANAGERS")]


    [Header("GAMEPLAY")]
    public Transform startingPoint;
    private readonly float speed = 10f;
    public float time;
    private float timer = 0f;
    private float interval = 1f;
    public int bestScore;
    public int bestTime;


    //THIS WILL GO TO PLAYER CONTROLLER
    public void AddForceToPlayer()
    {
       // playerController.playerRb.AddForce(Vector2.up * playerController.startJumpForce, ForceMode2D.Impulse);
       // StartCoroutine(PlayIntro());
    }


    public void GameOver()
    {
        Debug.Log("Game Over Was Played");
      //  gameOverMenuScreen.SetActive(true);
       // audioManager.mainGameMusicAudioSource.Stop();
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
        //time += Time.deltaTime;
        //int minutes = Mathf.FloorToInt(time / 60);
        //int seconds = Mathf.FloorToInt(time % 60);
        //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //timerText.text = "Timer: " + timerText.text;         
    }
}





