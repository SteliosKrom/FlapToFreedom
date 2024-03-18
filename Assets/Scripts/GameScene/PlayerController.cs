using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("PARTICLES")]
    public ParticleSystem gemParticle;
    public ParticleSystem collisionParticle;

    [Header("UI")]
    [SerializeField] private GameObject plusOneScoreGameObject;

    [Header("MANAGERS")]
    public BestTimeAndScoreManager bestTimeAndScoreManager;
    public MainGameUIManager mainGameUIManager;


    [Header("GAMEPLAY")]
    public Transform startingPoint;
    private readonly float speed = 10f;
    public Rigidbody2D playerRb;
    public float startJumpForce;
    private readonly float jumpForce = 1f;
    public int score;
    private float startTime;
    private float onTriggerExitDelay = 0.5f;


    [Header("AUDIO SOURCES")]
    public AudioSource gameOverAudioSource;
    public AudioSource jumpSoundAudioSource;
    public AudioSource gemTriggerAudioSource;


    [Header("AUDIO CLIPS")]
    public AudioClip gameOverAudioClip;
    public AudioClip jumpSoundAudioClip;
    public AudioClip gemTriggerAudioClip;



    private void Awake()
    {
        startTime = Time.time;
        playerRb = this.GetComponent<Rigidbody2D>();
        plusOneScoreGameObject.SetActive(false);
        AddForceToPlayer();
    }


    private void Update()
    {
        InputForPlayerMovement();
    }

    public void InputForPlayerMovement()
    {
        if (RoundManager.Instance.currentState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
            Vector3 gemSpawnPos = other.transform.position;
            Instantiate(gemParticle, gemSpawnPos, Quaternion.identity);
            other.gameObject.SetActive(false);
            AudioManager.Instance.PlaySound(gemTriggerAudioSource, gemTriggerAudioClip);
            mainGameUIManager.UpdateScore();
            plusOneScoreGameObject.SetActive(true);
        }
        else if (other.gameObject.CompareTag("Logs"))
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
        else if (other.gameObject.CompareTag("Bounds"))
        {
            gameObject.SetActive(false);
            RoundManager.Instance.GameOver();
            AudioManager.Instance.PlaySound(gameOverAudioSource, gameOverAudioClip);
            float currentTime = Time.time - startTime;
            BestTimeAndScoreManager.Instance.CheckSaveBestScore(score);
            BestTimeAndScoreManager.Instance.CheckSaveBestTime(currentTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            StartCoroutine(OnTriggerExit2DWithDelay());
        }
    }

    private IEnumerator OnTriggerExit2DWithDelay()
    {
        yield return new WaitForSeconds(onTriggerExitDelay);
        plusOneScoreGameObject.SetActive(false);
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
