using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("MANAGERS")]
    public PlayerController playerController; //remove player controller reference
    //remove unused tags
    public MainGameUIManager mainGameUIManager;  //unused reference

    [Header("Object Pooling")]
    public ObjectPooling gemParticlePooling;


    [Header("UI")]
    [SerializeField] TextMeshProUGUI scoreText;     //this should not be on player it should be on UI so move it out of here


    [Header("GAMEPLAY")]
    public Transform startingPoint;
    private readonly float speed = 10f;
    public Rigidbody2D playerRb;
    public float startJumpForce;
    private readonly float jumpForce = 1f;
    public int score;
    [SerializeField] private GameObject player;     //Remove unncessary
    [SerializeField] private float lowerYRange; //unused
    [SerializeField] private float upperYRange; //unused
    [SerializeField] private float xBounds; //unused


    [Header("AUDIO SOURCES")]
    public AudioSource gameOverAudioSource;
    public AudioSource jumpSoundAudioSource;


    [Header("AUDIO CLIPS")]
    public AudioClip gameOverAudioClip;
    public AudioClip jumpSoundAudioClip;



    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        mainGameUIManager = GameObject.Find("MainGameUIManager").GetComponent<MainGameUIManager>();
        gemParticlePooling = GameObject.Find("GemParticlePooling").GetComponent<ObjectPooling>();
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

    public void ShowScoreOnStart()
    {
        if (RoundManager.Instance.currentState == GameState.Playing)
        {
            score = 0;
            scoreText.text = "Score: " + score.ToString();
        }     
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            gemParticlePooling.CollectObject(other.gameObject);

            GameObject gemParticle = gemParticlePooling.GetPooledObject();
            gemParticle.transform.position = other.transform.position;
            gemParticle.SetActive(true);
            
            other.gameObject.SetActive(false);

        }
        else if (other.gameObject.CompareTag("Logs"))
        {
            //make sure it grabs correct pooling objects
            GameObject collisionParticle = gemParticlePooling.GetPooledObject();

            collisionParticle.transform.position = player.transform.position;
            collisionParticle.SetActive(true);
            
            player.SetActive(false);
            RoundManager.Instance.GameOver();
            AudioManager.Instance.PlaySound(gameOverAudioSource, gameOverAudioClip);
            RoundManager.Instance.CheckSaveBestScore();
            RoundManager.Instance.CheckSaveBestTime();
        }
        else if (other.gameObject.CompareTag("Bounds"))
        {
            player.gameObject.SetActive(false);
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
