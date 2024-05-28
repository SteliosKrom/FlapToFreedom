using TMPro;
using UnityEngine;

public class BestTimeAndScoreManager : MonoBehaviour
{
    public static BestTimeAndScoreManager Instance;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI bestTimeText;

    [Header("GAMEPLAY")]
    public int bestScore;
    public float bestTime;

    private void Awake()
    {
        Instance = this;
        LoadBestScore();
        LoadBestTime();
    }

    public void CheckSaveBestScore(int currentScore)
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            bestScoreText.text = "Best Score: " + bestScore;
        }
    }

    public void CheckSaveBestTime(float currentTime)
    {
        if (currentTime > bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
            int minutes = Mathf.FloorToInt(bestTime / 60);
            int seconds = Mathf.FloorToInt(bestTime % 60);
            bestTimeText.text = "Best Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + bestScore;
    }

    private void LoadBestTime()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", 0f);
        int minutes = Mathf.FloorToInt(bestTime / 60);
        int seconds = Mathf.FloorToInt(bestTime % 60);
        bestTimeText.text = "Best Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
