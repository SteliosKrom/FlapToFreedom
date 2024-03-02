using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [Header("MANAGERS")]
    public PlayerController playerController;

    [Header("POOLING FIELDS")]
    public GameObject gemPrefab;
    public GameObject treeLogsPrefab;
    public GameObject collisionParticlePrefab;
    public GameObject gemParticlePrefab;

    public int gemPoolSize = 20;
    public int gemParticlePoolSize = 20;
    public int treeLogsPoolSize = 20;
    public int collisionParticlePoolSize = 20;

    public readonly List<GameObject> gemPool = new List<GameObject>();
    public readonly List<GameObject> treeLogsPool = new List<GameObject>();
    public readonly List<GameObject> collisionParticlePool = new List<GameObject>();
    public readonly List<GameObject> gemParticlePool = new List<GameObject>();

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        InitializeObjectPool(gemPrefab, gemPool, gemPoolSize);
        InitializeObjectPool(treeLogsPrefab, treeLogsPool, treeLogsPoolSize);
        InitializeObjectPool(collisionParticlePrefab, collisionParticlePool, collisionParticlePoolSize);
        InitializeObjectPool(gemParticlePrefab, gemParticlePool, gemParticlePoolSize);
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
                playerController.UpdateScore();
                RoundManager.Instance.CheckSaveBestScore();
                RoundManager.Instance.CheckSaveBestTime();
                Debug.Log($"{name}Player collected an object!", gameObject);
            }
        }
        else
        {
            Debug.LogWarning("Gem object is null!");
        }
        if (obj != null)
        {
            if (obj.CompareTag(""))
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

        else if (obj.CompareTag("") || obj.CompareTag(""))
        {
            treeLogsPool.Add(obj);
        }
        else if (obj.CompareTag(""))
        {
            collisionParticlePool.Add(obj);
        }

        else if (obj.CompareTag(""))
        {
            gemParticlePool.Add(obj);
        }
    }
}
