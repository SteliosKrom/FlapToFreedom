using TMPro;
using UnityEngine;

public class BestTimeAndScoreManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI bestTimeText;

    [Header("MANAGERS")]
    public PlayerController playerController;

    [Header("GAMEPLAY")]
    public int bestScore;
    public int bestTime;
    public float time;


    public void CheckSaveBestScore()
    {
        if (playerController.score > bestScore)
        {
            bestScore = playerController.score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            bestScoreText.text = "Best Score: " + bestScore;
        }
    }

    public void CheckSaveBestTime()
    {
        if (time > bestTime)
        {
            bestTime = (int)time;
            PlayerPrefs.SetInt("BestTime", bestTime);
            PlayerPrefs.Save();
            bestTimeText.text = "Best Time: " + bestTime;
        }
    }
}
