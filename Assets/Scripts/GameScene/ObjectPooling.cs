using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [Header("POOLING FIELDS")]
    [SerializeField] private GameObject objectPrefab;

    [SerializeField] private int poolSize = 20;
    [SerializeField] private bool autoGrow = false;
    [SerializeField] private List<GameObject> objectPoolingList = new List<GameObject>();


    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject()
    {
        GameObject obj = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
        objectPoolingList.Add(obj);
        obj.SetActive(false);
        return obj;
    }

    //move pooling to a new Class
    public GameObject GetPooledObject()
    {
        // Check for an inactive object in the pool!
        GameObject pooledObject = null;
        foreach (GameObject obj in objectPoolingList)
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                pooledObject = obj;
                break;
            }
        }

        if(pooledObject == null && autoGrow) 
        {
            pooledObject = CreateObject();
        }
        else
        {
            Debug.LogWarning("Unable to create object but it is required");
        }

        return pooledObject;
    }

    // this shouldnt be on object pooling
    public void CollectObject(GameObject obj)
    {
        if (obj != null)
        {
            if (obj.CompareTag("Gem"))
            {
                obj.SetActive(false);
                ReturnObjectToPool(obj);
               // playerController.UpdateScore();
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

    //This shouldnt be in object pooling
    public void ReturnObjectToPool(GameObject obj)
    {
        if (obj.CompareTag("Gem"))
        {
         //   gemPool.Add(obj);
        }

        else if (obj.CompareTag("") || obj.CompareTag(""))
        {
       //     treeLogsPool.Add(obj);
        }
        else if (obj.CompareTag(""))
        {
         //   collisionParticlePool.Add(obj);
        }

        else if (obj.CompareTag(""))
        {
           // gemParticlePool.Add(obj);
        }
    }
}
