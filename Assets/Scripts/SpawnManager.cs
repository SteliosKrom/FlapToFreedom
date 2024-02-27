using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Managers variables!
    public GameManager gameManager;
    private PlayerController playerController;

    // Game object variables!
    public GameObject treeLogsPrefab;

    // Float variables!
    private float upperBoundY = 3.5f;
    private float lowerBoundY = -3.5f;
    private float zBounds = 10f;
    private float xBounds = -10f;
    private float startDelay = 2f;
    private float spawnRate = 3f;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("SpawnObjects", startDelay, spawnRate);
        Debug.Log("Spawn objects with a delay!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObjects()
    {
        if (treeLogsPrefab != null && gameManager.currentState == GameManager.GameState.Playing)
        {
            var spawnPos = new Vector3(xBounds, Random.Range(lowerBoundY, upperBoundY), zBounds);
            Instantiate(treeLogsPrefab, spawnPos, treeLogsPrefab.transform.rotation);
            Debug.Log("Instantiating objects between boundaries");
        }

        else
        {
            Debug.Log("Game is over objects are not spawning!");
        }
    }
}
