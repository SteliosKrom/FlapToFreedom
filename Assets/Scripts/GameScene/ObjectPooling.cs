using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling Instance;

    [Header("POOLING FIELDS")]
    public GameObject objectPrefab;
    [SerializeField] private int poolSize = 50;
    [SerializeField] private List<GameObject> objectPoolingList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
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

    public GameObject GetPooledObject()
    {
        GameObject pooledObject = null;
        foreach (GameObject obj in objectPoolingList)
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                pooledObject = obj;
            }
            break;
        }

        if (pooledObject == null)
        {
            pooledObject = CreateObject();
            Debug.Log("Created new object because pool was empty.");
        }
        else
        {
            Debug.Log("Retrieved object from the pool");
        }
        return pooledObject;
    }

    /*public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }*/
}
