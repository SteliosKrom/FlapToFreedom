using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("REFERENCES")]
    public ObjectPooling gemParticlePooling;
    public ObjectPooling collisionParticlePooling;


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
            GameObject gemParticle = gemParticlePooling.GetPooledObject();
            if (gemParticle != null)
            {
                gemParticle.transform.position = gameObject.transform.position;
                gemParticle.SetActive(true);
                other.gameObject.SetActive(false);
                AudioManager.Instance.PlaySound(gemTriggerAudioSource, gemTriggerAudioClip);
                mainGameUIManager.UpdateScore();
            }
            else
            {
                Debug.LogWarning("No gem particle available in the object pool");
            }
            
        }
        else if (other.gameObject.CompareTag("Logs"))
        {
            GameObject collisionParticle = collisionParticlePooling.GetPooledObject();
            if (collisionParticle != null)
            {
                collisionParticle.transform.position = gameObject.transform.position;
                collisionParticle.SetActive(true);
                gameObject.SetActive(false);
                RoundManager.Instance.GameOver();
                AudioManager.Instance.PlaySound(gameOverAudioSource, gameOverAudioClip);
                BestTimeAndScoreManager.Instance.CheckSaveBestScore(score);
                float currentTime = Time.time - startTime; 
                BestTimeAndScoreManager.Instance.CheckSaveBestScore(score);
                BestTimeAndScoreManager.Instance.CheckSaveBestTime(currentTime);
            }  
            else
            {
                Debug.Log("No gem particle available in the object pool");
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
