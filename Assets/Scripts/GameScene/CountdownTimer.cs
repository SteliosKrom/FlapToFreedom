using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private float currentInvicibilityTime = 0f;
    [SerializeField] private float startingInvicibilityTime = 10f;

    [SerializeField] private float currentDoubleScoreTime = 0f;
    [SerializeField] private float startingDoubleScoreTime = 5f;

    public TextMeshProUGUI invicibilityCountdownText;
    public GameObject invicibilityText;
    public GameObject invicibilityCountdown;

    public TextMeshProUGUI doubleScoreCountdownText;
    public GameObject doubleScoreText;
    public GameObject doubleScoreCountdown;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        currentInvicibilityTime = startingInvicibilityTime;
        currentDoubleScoreTime = startingDoubleScoreTime;

        invicibilityCountdown.SetActive(false);
        invicibilityText.SetActive(false);
        doubleScoreCountdown.SetActive(false);
        doubleScoreText.SetActive(false);
    }

    void Update()
    {
        if (playerController.hasShield && RoundManager.Instance.currentState == GameState.Playing)
        {
            InvicibilityCountdownTimer();
        }

        if (playerController.hasPowerUpIncrease && RoundManager.Instance.currentState == GameState.Playing)
        {
            DoubleScoreCountdownTimer();
        }
    }

    public void InvicibilityCountdownTimer()
    {
        currentInvicibilityTime -= 1 * Time.deltaTime;
        invicibilityCountdownText.text = currentInvicibilityTime.ToString("0");

        if (currentInvicibilityTime < 3)
        {
            invicibilityCountdownText.color = Color.red;
        }

        if (currentInvicibilityTime <= 0)
        {
            currentInvicibilityTime = 10f;
            invicibilityCountdownText.color = Color.white;
            invicibilityCountdown.SetActive(false);
            invicibilityText.SetActive(false);
        }
    }

    public void DoubleScoreCountdownTimer()
    {
        currentDoubleScoreTime -= 1 * Time.deltaTime;
        doubleScoreCountdownText.text = currentDoubleScoreTime.ToString("0");

        if (currentDoubleScoreTime < 3)
        {
            doubleScoreCountdownText.color = Color.red;
        }

        if (currentDoubleScoreTime <= 0)
        {
            currentDoubleScoreTime = 5f;
            doubleScoreCountdownText.color = Color.white;
            doubleScoreCountdown.SetActive(false);
            doubleScoreText.SetActive(false);
        }
    }
}
