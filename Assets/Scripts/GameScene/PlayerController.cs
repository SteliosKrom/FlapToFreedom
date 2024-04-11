using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("PARTICLES")]
    public GameObject gemParticle;
    public GameObject collisionParticle;
    public GameObject powerUpParticle;
    public GameObject playerPowerUpAuraParticle;
    public GameObject playerPowerUpAuraIncreaseScoreByTwoParticle;
    public GameObject powerUpIncreaseScoreByTwoParticle;

    [Header("UI")]
    public GameObject plusOneScoreGameObject;
    public GameObject plusTwoScoreGameObject;
    public GameObject informPlayerForPowerUp;
    public GameObject informPlayerIncreasePowerUp;

    [Header("MANAGERS")]
    public MainGameUIManager mainGameUIManager;

    [Header("GAMEPLAY")]
    public Transform startingPoint;
    [SerializeField] private float speed;
    public Rigidbody2D playerRb;
    public float startJumpForce;
    [SerializeField] private float jumpForce;
    public int score;
    private float startTime;
    private readonly float onTriggerExitDelay = 2f;
    private bool hasShield = false;
    [SerializeField] private float shieldDuration;
    [SerializeField] private float powerUpIncreaseDuration;
    private float shieldTimer = 0f;
    private bool hasPowerUpIncrease = false;
    private float powerUpIncreaseTimer = 0f;


    [Header("AUDIO SOURCES")]
    public AudioSource gameOverAudioSource;
    public AudioSource jumpSoundAudioSource;
    public AudioSource gemTriggerAudioSource;
    public AudioSource powerUpAudioSource;


    [Header("AUDIO CLIPS")]
    public AudioClip gameOverAudioClip;
    public AudioClip jumpSoundAudioClip;
    public AudioClip gemTriggerAudioClip;
    public AudioClip powerUpAudioClip;

    private void Awake()
    {
        mainGameUIManager = GameObject.Find("MainGameUIManager").GetComponent<MainGameUIManager>();
        playerRb = this.GetComponent<Rigidbody2D>();
        AddForceToPlayer();
        startTime = Time.time;
        plusOneScoreGameObject.SetActive(false);
        plusTwoScoreGameObject.SetActive(false);
        hasShield = false;
        hasPowerUpIncrease = false;
        informPlayerForPowerUp.SetActive(false);
        informPlayerIncreasePowerUp.SetActive(false);
    }

    private void Update()
    {
        UpdateShield();
        UpdatePowerUpIncreaseScoreByTwo();
        InputForPlayerMovement();
    }

    public void UpdatePowerUpIncreaseScoreByTwo()
    {
        if (hasPowerUpIncrease)
        {
            powerUpIncreaseTimer -= Time.deltaTime;
            if (powerUpIncreaseTimer <= 0f)
            {
                hasPowerUpIncrease = false;
                ResetPowerUpIncreaseByTwo();
            }
        }
    }

    public void ActivatePowerUpIncreaseByTwo()
    {
        hasPowerUpIncrease = true;
        powerUpIncreaseTimer += powerUpIncreaseDuration;
    }

    public void ResetPowerUpIncreaseByTwo()
    {
        hasPowerUpIncrease = false;
        powerUpIncreaseTimer = 0f;

        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("PlayerBoostAuraIncrease"))
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }

    private void UpdateShield()
    {
        if (hasShield)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0)
            {
                hasShield = false;
                ResetShield();
                informPlayerForPowerUp.SetActive(false);
            }
        }
    }

    public void ActivateShield()
    {
        hasShield = true;
        shieldTimer += shieldDuration;
    }

    public void ResetShield()
    {
        hasShield = false;
        shieldTimer = 0f;

        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("PlayerPowerUpAura"))
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }

    public void InputForPlayerMovement()
    {
        if (RoundManager.Instance.currentState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                AudioManager.Instance.PlaySound(jumpSoundAudioSource, jumpSoundAudioClip);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            if (hasPowerUpIncrease == true)
            {
                Vector3 gemSpawnPos = other.transform.position;
                Instantiate(gemParticle, gemSpawnPos, Quaternion.identity);
                other.gameObject.SetActive(false);
                AudioManager.Instance.PlaySound(gemTriggerAudioSource, gemTriggerAudioClip);
                mainGameUIManager.UpdateScore(2);
                plusTwoScoreGameObject.SetActive(true);
            }
            else
            {
                Vector3 gemSpawnPos = other.transform.position;
                Instantiate(gemParticle, gemSpawnPos, Quaternion.identity);
                other.gameObject.SetActive(false);
                AudioManager.Instance.PlaySound(gemTriggerAudioSource, gemTriggerAudioClip);
                mainGameUIManager.UpdateScore();
                plusOneScoreGameObject.SetActive(true);
            }
        }
        else if (other.gameObject.CompareTag("Logs"))
        {
            if (hasShield == false)
            {
                Vector3 collisionSpawnPos = gameObject.transform.position;
                Instantiate(collisionParticle, collisionSpawnPos, Quaternion.identity);
                gameObject.SetActive(false);
                RoundManager.Instance.GameOver();
                AudioManager.Instance.PlaySound(gameOverAudioSource, gameOverAudioClip);
                BestTimeAndScoreManager.Instance.CheckSaveBestScore(score);
                float currentTime = Time.time - startTime;
                BestTimeAndScoreManager.Instance.CheckSaveBestScore(score);
                BestTimeAndScoreManager.Instance.CheckSaveBestTime(currentTime);
            }
        }
        else if (other.gameObject.CompareTag("Bounds"))
        {
            gameObject.SetActive(false);
            RoundManager.Instance.GameOver();
            AudioManager.Instance.PlaySound(gameOverAudioSource, gameOverAudioClip);
            float currentTime = Time.time - startTime;
            BestTimeAndScoreManager.Instance.CheckSaveBestScore(score);
            BestTimeAndScoreManager.Instance.CheckSaveBestTime(currentTime);
        }
        else if (other.gameObject.CompareTag("PowerUp"))
        {
            other.gameObject.SetActive(false);
            Vector3 powerUpSpawnPos = other.transform.position;
            GameObject aura = Instantiate(playerPowerUpAuraParticle, transform.position, Quaternion.identity);
            aura.transform.parent = transform;
            Instantiate(powerUpParticle, powerUpSpawnPos, Quaternion.identity);
            AudioManager.Instance.PlaySound(powerUpAudioSource, powerUpAudioClip);
            ActivateShield();
            informPlayerForPowerUp.SetActive(true);
        }
        else if (other.gameObject.CompareTag("PowerUpIncreaseScoreBy2"))
        {
            other.gameObject.SetActive(false);
            Vector3 powerUpIncreaseScoreByTwoPos = other.transform.position;
            GameObject aura = Instantiate(playerPowerUpAuraIncreaseScoreByTwoParticle, transform.position, Quaternion.identity);
            aura.transform.parent = transform;
            Instantiate(powerUpIncreaseScoreByTwoParticle, powerUpIncreaseScoreByTwoPos, Quaternion.identity);
            AudioManager.Instance.PlaySound(powerUpAudioSource, powerUpAudioClip);
            ActivatePowerUpIncreaseByTwo();
            informPlayerIncreasePowerUp.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            StartCoroutine(OnTriggerExit2DWithDelay());
        }
        else if (other.gameObject.CompareTag("PowerUp"))
        {
            StartCoroutine(OnTriggerExit2DWithDelay());
        }
        else if (other.gameObject.CompareTag("PowerUpIncreaseScoreBy2"))
        {
            StartCoroutine(OnTriggerExit2DWithDelay());
        }
    }

    private IEnumerator OnTriggerExit2DWithDelay()
    {
        yield return new WaitForSeconds(onTriggerExitDelay);
        plusOneScoreGameObject.SetActive(false);
        plusTwoScoreGameObject.SetActive(false);
        informPlayerForPowerUp.SetActive(false);
        informPlayerIncreasePowerUp.SetActive(false);
    }

    public void AddForceToPlayer()
    {
        playerRb.AddForce(Vector2.up * startJumpForce, ForceMode2D.Impulse);
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startingPoint.position;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (startTime - startTime) * speed;
        float fractionOfJourney = distanceCovered / journeyLength;

        while (fractionOfJourney < 1)
        {
            RoundManager.Instance.currentState = GameState.Intro;
            distanceCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }
        RoundManager.Instance.currentState = GameState.Playing;
        mainGameUIManager.ShowScoreOnStart();
    }
}
