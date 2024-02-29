using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerController : MonoBehaviour
{
    [Header("MANAGERS")]
    public PlayerController playerController;
    public MainGameUIManager mainGameUIManager;


    [Header("UI")]
    [SerializeField] TextMeshProUGUI scoreText;


    [Header("GAMEPLAY")]
    public Transform startingPoint;
    private readonly float speed = 10f;
    public Rigidbody2D playerRb;
    public float startJumpForce;
    private readonly float jumpForce = 1f;
    public int score;
    public GameObject player;
    public GameObject treeLogs;
    [SerializeField] private float lowerYRange;
    [SerializeField] private float upperYRange;
    [SerializeField] private float xBounds;


    [Header("AUDIO SOURCES")]
    public AudioSource gameOverAudioSource;
    public AudioSource jumpSoundAudioSource;


    [Header("AUDIO CLIPS")]
    public AudioClip gameOverAudioClip;
    public AudioClip jumpSoundAudioClip;



    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        playerRb = this.GetComponent<Rigidbody2D>();

        AddForceToPlayer();

        if (mainGameUIManager == null)
        {
            mainGameUIManager = GameObject.FindObjectOfType<MainGameUIManager>();

            if (mainGameUIManager == null)
            {
                Debug.Log($"{name}Main game ui manager not found!", gameObject);
            }
        }

        score = 0;
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {
        if (RoundManager.Instance.currentState == GameState.Playing)
        {
            InputForPlayerMovement();
        }
    }


    public void InputForPlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySound(jumpSoundAudioSource, jumpSoundAudioClip);
        }
    }


    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        //ADD CHECK OUT OF BOUNDS ON TRIGGER ENTER COLLISION
        if (other.gameObject.CompareTag("Gem"))
        {
            ObjectPooling.Instance.CollectObject(other.gameObject);

            GameObject gemParticle = ObjectPooling.Instance.GetPooledObject(ObjectPooling.Instance.gemParticlePool);

            if (gemParticle != null)
            {
                gemParticle.transform.position = other.transform.position;
                gemParticle.SetActive(true);
            }
            else
            {
                Debug.LogError($"{name} Gem particle not found!", gameObject);
            }

        }
        else if (other.gameObject.CompareTag("TreeTrunkDown") || other.gameObject.CompareTag("TreeTrunkUp"))
        {

            GameObject collisionParticle = ObjectPooling.Instance.GetPooledObject(ObjectPooling.Instance.collisionParticlePool);


            if (collisionParticle != null)
            {
                collisionParticle.transform.position = player.transform.position;
                collisionParticle.SetActive(true);
            }

            player.SetActive(false);
            RoundManager.Instance.GameOver();
            AudioManager.Instance.PlaySound(gameOverAudioSource, gameOverAudioClip);
            RoundManager.Instance.CheckSaveBestScore();
            RoundManager.Instance.CheckSaveBestTime();
        }
        else if (other.gameObject.CompareTag("UpDownBound"))
        {
            player.SetActive(false);
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
            distanceCovered = (Time.time - startTime) * speed;
            fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);

            yield return null;
        }
        RoundManager.Instance.currentState = GameState.Playing;
    }


    public void DestroyPlayerOutOfBoundaries()
    {
        if (transform.position.y <= lowerYRange)
        {
            
        }

        else if (transform.position.y >= upperYRange)
        {
            playerController.player.SetActive(false);
            RoundManager.Instance.GameOver();
            AudioManager.Instance.PlaySound(gameOverAudioSource, gameOverAudioClip);
        }
    }

    /**public void DestroyLogsOutOfBoundaries()
    {
        if (gameObject.CompareTag("TreeLogs") && transform.position.x <= -XBounds)
        {
            
        }
    }**/
}
