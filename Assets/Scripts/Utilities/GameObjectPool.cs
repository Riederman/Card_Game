using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int numToCreate;

    private int numObjects;

    private Stack<GameObject> pool = new Stack<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < numToCreate; i++)
            CreateInstance();
    }

    private GameObject CreateInstance()
    {
        // Create an instance of the prefab
        GameObject gameObj = Instantiate(prefab);
        gameObj.transform.parent = transform;
        gameObj.SetActive(false);
        pool.Push(gameObj);
        return gameObj;
    }

    public GameObject GetObject()
    {
        // Retrieves an object from the pool
        if (pool.Count > 0)
            return pool.Pop();
        // Or creates a new instance
        else return CreateInstance();
    }

    public void ReturnObject(GameObject gameObj)
    {
        // Returns the object to the pool
        gameObj.transform.parent = transform;
        gameObj.SetActive(false);
        pool.Push(gameObj);
    }
}