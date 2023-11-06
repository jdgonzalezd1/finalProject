using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class PooledObject
    {
        public GameObject prefab;
        public int amount;
    }

    public List<PooledObject> pooledObjects;
    public Transform objectsParent;

    private List<GameObject> pool;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new List<GameObject>();
        foreach (PooledObject pooledObject in pooledObjects)
        {
            for (int i = 0; i < pooledObject.amount; i++)
            {
                GameObject obj = Instantiate(pooledObject.prefab, objectsParent);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
}
