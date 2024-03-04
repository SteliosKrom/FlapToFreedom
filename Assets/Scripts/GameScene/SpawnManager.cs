using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject treeLogsPrefab;

    private readonly float upperBoundY = 3.5f;
    private readonly float lowerBoundY = -3.5f;
    private readonly float zBounds = 10f;
    private readonly float xBounds = -15f;
    private readonly float startDelay = 1f;
    private readonly float spawnRate = 3f;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //Pool all your objects here
        GameObject gemParticleObj = playerController.gemParticlePooling.GetPooledObject();
        GameObject collisionParticleObj = playerController.collisionParticlePooling.GetPooledObject();
        gemParticleObj.SetActive(false);
        collisionParticleObj.SetActive(false);
        StartCoroutine(SpawnObjectsCouroutine());
    }

    //re work this
    public void SpawnObjects() //this should fetch from object pooling and enable in correct position
    {
        var spawnPos = new Vector3(xBounds, Random.Range(lowerBoundY, upperBoundY), zBounds);
        GameObject logsPoolingObj = Instantiate(treeLogsPrefab, spawnPos, treeLogsPrefab.transform.rotation);
        logsPoolingObj.SetActive(true);
    }

    private IEnumerator SpawnObjectsCouroutine()
    {
        yield return new WaitForSeconds(startDelay);

        if (RoundManager.Instance.currentState == GameState.Playing)
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnRate);
                SpawnObjects();
            }
        }
    }
}
