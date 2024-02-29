using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float time;
    private float timer = 0f;
    private float interval = 1f;

    private void Update()
    {
        //TimeScore();
    }
    public void TimeScore()
    {
        Debug.Log("Game Over bool: " + RoundManager.Instance.IsGameOver);
        if (!RoundManager.Instance.IsGameOver)
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
}
