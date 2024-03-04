using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("REFERENCES")]
    public ObjectPooling gemParticlePooling;
    public ObjectPooling collisionParticlePooling;


    [Header("MANAGERS")]
    public MainGameUIManager mainGameUIManager;


    [Header("GAMEPLAY")]
    public Transform startingPoint;
    private readonly float speed = 10f;
    public Rigidbody2D playerRb;
    public float startJumpForce;
    private readonly float jumpForce = 1f;
    public int score;


    [Header("AUDIO SOURCES")]
    public AudioSource gameOverAudioSource;
    public AudioSource jumpSoundAudioSource;


    [Header("AUDIO CLIPS")]
    public AudioClip gameOverAudioClip;
    public AudioClip jumpSoundAudioClip;



    private void Awake()
    {
        playerRb = this.GetComponent<Rigidbody2D>();
        AddForceToPlayer();
    }


    private void Update()
    {
        InputForPlayerMovement();
        RoundManager.Instance.CheckSaveBestTime();
        RoundManager.Instance.CheckSaveBestScore();
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

    public void ShowScoreOnStart()
    {
        score = 0;
        mainGameUIManager.ScoreText.text = "Score: " + score.ToString();
    }

    public void UpdateScore()
    {
        score++;
        mainGameUIManager.ScoreText.text = "Score: " + score;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            GameObject gemParticle = gemParticlePooling.GetPooledObject();
            gemParticle.transform.position = other.transform.position;
            gemParticle.SetActive(true);
            other.gameObject.SetActive(false);
            UpdateScore();
        }
        else if (other.gameObject.CompareTag("Logs"))
        {
            //make sure it grabs correct pooling objects
            GameObject collisionParticle = collisionParticlePooling.GetPooledObject();
            collisionParticle.transform.position = gameObject.transform.position;
            collisionParticle.SetActive(true);
            gameObject.SetActive(false);
            RoundManager.Instance.GameOver();
            AudioManager.Instance.PlaySound(gameOverAudioSource, gameOverAudioClip);
            RoundManager.Instance.CheckSaveBestScore();
            RoundManager.Instance.CheckSaveBestTime();
        }
        else if (other.gameObject.CompareTag("Bounds"))
        {
            gameObject.SetActive(false);
            RoundManager.Instance.GameOver();
            AudioManager.Instance.PlaySound(gameOverAudioSource, gameOverAudioClip);
        }
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
        ShowScoreOnStart();
    }
}
