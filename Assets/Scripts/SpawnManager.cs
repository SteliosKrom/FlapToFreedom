using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Managers variables!
    public GameManager gameManager;

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

        InvokeRepeating("SpawnObjects", startDelay, spawnRate);
    }

    public void SpawnObjects()
    {
        var spawnPos = new Vector3(xBounds, Random.Range(lowerBoundY, upperBoundY), zBounds);
        GameObject boundObj = Instantiate(treeLogsPrefab, spawnPos, treeLogsPrefab.transform.rotation);
        DestroyOutOfBounds treeBounds = boundObj.GetComponent<DestroyOutOfBounds>();
        treeBounds.SetPlayerReference(gameManager.PlayerController);
    }
}
