using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
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
        InvokeRepeating("SpawnObjects", startDelay, spawnRate);  //CANCEL INVOKE ON GAME OVER
    }

    public void SpawnObjects()
    {
        var spawnPos = new Vector3(xBounds, Random.Range(lowerBoundY, upperBoundY), zBounds);
        GameObject boundObj = Instantiate(treeLogsPrefab, spawnPos, treeLogsPrefab.transform.rotation);
        DestroyOutOfBounds treeBounds = boundObj.GetComponent<DestroyOutOfBounds>();
        treeBounds.SetPlayerReference(RoundManager.Instance.PlayerController);
    }
}
