using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    // Object variables!
    public GameObject treeLogs;
    [SerializeField] private float lowerYRange;
    [SerializeField] private float upperYRange;
    [SerializeField] private float xBounds;


    // Managers variables!
    private AudioManager audioManager;
    private GameManager gameManager;
    [SerializeField] private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        // Managers references!
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //if (playerController != null)
            //playerController = GameObject.Find("Player").GetComponent<PlayerController>();  
    }

    // Update is called once per frame
    void Update()
    {
        DestroyPlayerOutOfBoundaries();
        DestroyLogsOutOfBoundaries();
    }

    public void DestroyPlayerOutOfBoundaries()
    {
        if (transform.position.y <= lowerYRange)
        {
            Debug.Log("Destroying object due to lower bound");
            playerController.player.SetActive(false);
            gameManager.GameOver();
            audioManager.GameOverSound();
            Debug.Log("Player game object is destroyed!");
        }

        else if (transform.position.y >= upperYRange)
        {
            Debug.Log("Destroying object due to upper bound");
            playerController.player.SetActive(false);
            gameManager.GameOver();
            audioManager.GameOverSound();
            Debug.Log("Player game object is destroyed!");
        }
    }

    public void DestroyLogsOutOfBoundaries()
    {
        if (gameObject.CompareTag("TreeLogs") && transform.position.x <= -xBounds)
        {
            playerController.CollectObject(gameObject);
            Debug.Log("Logs are destroyed!");
        }
    }
}
