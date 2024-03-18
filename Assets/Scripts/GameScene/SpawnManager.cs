using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject treeLogsPrefab;

    private readonly float upperBoundY = 4f;
    private readonly float lowerBoundY = -4f;
    private readonly float zBounds = 10f;
    private readonly float xBounds = -15f;
    private readonly float startDelay = 1f;
    private readonly float spawnRate = 3;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        StartCoroutine(SpawnObjectsCouroutine());
    }

    private void Update()
    {
        if (RoundManager.Instance.currentState == GameState.GameOver)
        {
            StopAllCoroutines();
        }
    }

    public void SpawnObjects()
    {
        var spawnPos = new Vector3(xBounds, Random.Range(lowerBoundY, upperBoundY), zBounds);
        Instantiate(treeLogsPrefab, spawnPos, treeLogsPrefab.transform.rotation);
    }

    private IEnumerator SpawnObjectsCouroutine()
    {
        yield return new WaitForSeconds(startDelay);

        while (RoundManager.Instance.currentState == GameState.Playing)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnObjects();
            Debug.Log("Objects are spawning", gameObject);
        }
    }
}
