using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [Header("POOLING FIELDS")]
    public GameObject objectPrefab;
    [SerializeField] private int poolSize = 50;
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

    public GameObject GetPooledObject()
    {
        GameObject pooledObject = null;
        foreach (GameObject obj in objectPoolingList)
        {
            if (obj != null && !obj.activeInHierarchy)
            {
                pooledObject = obj;
                break;
            }
        }
        if (pooledObject == null)
        {
            if (autoGrow)
            {     
                pooledObject = CreateObject();
            }
            else
            {
                Debug.LogWarning("Unable to create object but it is required");
            }
        }
        return pooledObject;
    }
}
