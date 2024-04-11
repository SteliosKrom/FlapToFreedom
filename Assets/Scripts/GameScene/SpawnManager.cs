using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("MANAGERS")]
    public PlayerController playerController;
    public MainGameUIManager mainGameUIManager;

    [Header("GAMEPLAY")]
    public GameObject[] obstaclePrefabs;
    public GameObject powerUpPrefabs;
    public GameObject powerUpIncreaseScoreByTwoPrefabs;
    private float upperLogsBoundY = 5f;
    private float lowerLogsBoundY = -3f;
    private float powerUpBoundX = 4f;
    private float powerUpBoundZ = 4.1f;
    private float lowerPowerUpBoundY = -10f;
    private float upperPowerUpBoundY = 0f;
    private float zBounds = 10f;
    private float xBounds = -20;
    [SerializeField] private float startDelay;
    [SerializeField] private float startPowerUpDelay;
    [SerializeField] private float startPowerUpIncreaseByTwoDelay;
    [SerializeField] private float spawnRate;
    [SerializeField] private float powerUpSpawnRate;
    [SerializeField] private float powerUpIncreaseByTwoSpawnRate;

    [System.Obsolete]
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        mainGameUIManager = GameObject.Find("MainGameUIManager").GetComponent<MainGameUIManager>();
        StartCoroutine(SpawnObstaclesCouroutine());
        StartCoroutine(SpawnPowerUpCoroutine());
        StartCoroutine(SpawnPowerUpIncreaseScoreByTwoCoroutine());
    }

    private void Update()
    {
        if (RoundManager.Instance.currentState == GameState.GameOver)
        {
            StopAllCoroutines();
        }
    }

    [System.Obsolete]
    public void SpawnObstacles()
    {
        int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        var spawnPos = new Vector3(xBounds, Random.Range(lowerLogsBoundY, upperLogsBoundY), zBounds);
        Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);

        if (obstacleIndex == 0)
            xBounds = -20f;
        else if (obstacleIndex == 1)
            xBounds = -20f;
        else if (obstacleIndex == 2)
            xBounds = -20f;
        else if (obstacleIndex == 3)
            xBounds = -20f; 
    }

    [System.Obsolete]
    private IEnumerator SpawnObstaclesCouroutine()
    {
        yield return new WaitForSeconds(startDelay);

        while (RoundManager.Instance.currentState == GameState.Playing)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnObstacles();
            Debug.Log("Objects are spawning", gameObject);
        }
    }

    [System.Obsolete]
    private IEnumerator SpawnPowerUpCoroutine()
    {
        yield return new WaitForSeconds(startPowerUpDelay);

        while (RoundManager.Instance.currentState == GameState.Playing)
        {
            var powerUpSpawnPos = new Vector3(powerUpBoundX, Random.RandomRange(lowerPowerUpBoundY, upperPowerUpBoundY), -powerUpBoundZ);
            Instantiate(powerUpPrefabs, powerUpSpawnPos, powerUpPrefabs.transform.rotation);
            yield return new WaitForSeconds(powerUpSpawnRate);
        } 
    }

    [System.Obsolete]
    private IEnumerator SpawnPowerUpIncreaseScoreByTwoCoroutine()
    {
        yield return new WaitForSeconds(startPowerUpIncreaseByTwoDelay);
        
        while (RoundManager.Instance.currentState == GameState.Playing)
        {
            var powerUpIncreaseScoreByTwoPos = new Vector3(powerUpBoundX, Random.Range(lowerPowerUpBoundY, upperPowerUpBoundY), -powerUpBoundZ);
            Instantiate(powerUpIncreaseScoreByTwoPrefabs, powerUpIncreaseScoreByTwoPos, powerUpIncreaseScoreByTwoPrefabs.transform.rotation);
            yield return new WaitForSeconds(powerUpIncreaseByTwoSpawnRate);
        }
    }
}
