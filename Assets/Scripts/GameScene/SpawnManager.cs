using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    // Game object variables!
    public GameObject treeLogsPrefab;

    // Float variables!
    private readonly float upperBoundY = 3.5f;
    private readonly float lowerBoundY = -3.5f;
    private readonly float zBounds = 10f;
    private readonly float xBounds = -10f;
    private readonly float startDelay = 2f;
    private readonly float spawnRate = 3f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObjects", startDelay, spawnRate); //CANCEL INVOKE ON GAME OVER
    }

    public void SpawnObjects()
    {
        if (RoundManager.Instance.currentState == GameState.Playing)
        {
            var spawnPos = new Vector3(xBounds, Random.Range(lowerBoundY, upperBoundY), zBounds);
            Instantiate(treeLogsPrefab, spawnPos, treeLogsPrefab.transform.rotation);
        }
        else
        {
            CancelInvoke("SpawnObjects");
        }

    }
}
