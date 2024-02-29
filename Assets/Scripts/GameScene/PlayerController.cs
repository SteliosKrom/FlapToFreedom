using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("MANAGERS")]
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

    [Header("AUDIO SOURCES")]
    public AudioSource gameOverAudioSource;


    [Header("AUDIO CLIPS")]
    public AudioClip gameOverAudioClip;





    public GameObject gemPrefab;
    public GameObject treeLogsPrefab;
    public GameObject collisionParticlePrefab;
    public GameObject gemParticlePrefab;

    public int gemPoolSize = 20;
    public int gemParticlePoolSize = 20;
    public int treeLogsPoolSize = 20;
    public int collisionParticlePoolSize = 20;

    private readonly List<GameObject> gemPool = new List<GameObject>();
    public readonly List<GameObject> treeLogsPool = new List<GameObject>();
    private readonly List<GameObject> collisionParticlePool = new List<GameObject>();
    private readonly List<GameObject> gemParticlePool = new List<GameObject>();


    private void Awake()
    {
        // Here we give physics to our player and get the components needed!

        playerRb = this.GetComponent<Rigidbody2D>();

        AddForceToPlayer();

        // Check and assign UIManager!
        if (mainGameUIManager == null)
        {
            mainGameUIManager = GameObject.FindObjectOfType<MainGameUIManager>();

            if (mainGameUIManager == null)
            {
                Debug.Log($"{name}Main game ui manager not found!", gameObject);
            }
        }

        // Initialize object pools!
        InitializeObjectPool(gemPrefab, gemPool, gemPoolSize);
        InitializeObjectPool(treeLogsPrefab, treeLogsPool, treeLogsPoolSize);
        InitializeObjectPool(collisionParticlePrefab, collisionParticlePool, collisionParticlePoolSize);
        InitializeObjectPool(gemParticlePrefab, gemParticlePool, gemParticlePoolSize);

        // Initialize other variables as needed!
        score = 0;
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {
        if(RoundManager.Instance.currentState == GameState.Playing)
        {
            InputForPlayerMovement();
        }   
    }

    //Here we give input to our player!
    public void InputForPlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        //    audioManager.PlayerJumpSound();
           // Debug.Log("We give force to the player!");
        }
    }


    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
       // Debug.Log("Score updates: " + score);
    }

    //handle out of bounds/death collision here
    public void OnTriggerEnter2D(Collider2D other)
    {
        //ADD CHECK OUT OF BOUNDS ON TRIGGER ENTER COLLISION
        if (other.gameObject.CompareTag("Gem"))
        {
            CollectObject(other.gameObject);

            // Instantiate gem particle effect if available in the pool!
            GameObject gemParticle = GetPooledObject(gemParticlePool);

            // Check if a gem particle was successfully retrieved from the pool!
            if (gemParticle != null)
            {
                // Set the position and activate the gem particle!
                gemParticle.transform.position = other.transform.position;
                gemParticle.SetActive(true);
            }
            else
            {
                //Debug.LogError($"{name} Gem particle not found!", gameObject);
            }

        }
        else if (other.gameObject.CompareTag("TreeTrunkDown") || other.gameObject.CompareTag("TreeTrunkUp"))
        {

            // Instantiate collision particle effect if available in the pool!
            GameObject collisionParticle = GetPooledObject(collisionParticlePool);


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
    }

    private void InitializeObjectPool(GameObject prefab, List<GameObject> pool, int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    //move pooling to a new Class
    public GameObject GetPooledObject(List<GameObject> pool)
    {
        // Check for an inactive object in the pool!
        foreach (GameObject obj in pool)
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // If no inactive objects are found, create a new one!
        GameObject newObj = Instantiate(pool[0]);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    public void CollectObject(GameObject obj)
    {
        if (obj != null)
        {
            if (obj.CompareTag("Gem"))
            {
                obj.SetActive(false);
                ReturnObjectToPool(obj);
                UpdateScore();
               // GameManager.instance.CheckSaveBestScore();
               // GameManager.instance.CheckSaveBestTime();
                Debug.Log($"{name}Player collected an object!", gameObject);
            }     
        }

        else
        {
            Debug.LogWarning("Gem object is null!");
        }

        if (obj != null)
        {
            if (obj.CompareTag("TreeLogs"))
            {
                obj.SetActive(false);
                ReturnObjectToPool(obj);
            }
        }

        else
        {
            Debug.Log("Tree logs object is null!");
        }
    }


    public void ReturnObjectToPool(GameObject obj)
    {
        if (obj.CompareTag("Gem"))
        {
            gemPool.Add(obj);
        }

        else if (obj.CompareTag("TreeTrunkDown") || obj.CompareTag("TreeTrunkUp"))
        {
            treeLogsPool.Add(obj);
        }

        else if (obj.CompareTag("CollisionParticleEffect"))
        {
            collisionParticlePool.Add(obj);
        }

        else if (obj.CompareTag("GemParticleEffect"))
        {
            gemParticlePool.Add(obj);
        }
    }

    public void AddForceToPlayer()
    {
        playerRb.AddForce(Vector2.up * startJumpForce, ForceMode2D.Impulse); //this should be on your player controller
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
}
