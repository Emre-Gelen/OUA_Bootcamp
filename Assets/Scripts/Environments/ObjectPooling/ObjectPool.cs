using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [SerializeField] private List<Pool> _pools;
    [SerializeField] private Dictionary<PoolType, Queue<GameObject>> _poolDictionary;
    private void Start()
    {
        _poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();

        foreach (Pool pool in _pools)
        {
            Queue<GameObject> objects = new Queue<GameObject>();

            Array.ForEach(Enumerable.Range(0, pool.size).ToArray(), i =>
            {
                GameObject createdObject = Instantiate(pool.prefab);
                createdObject.SetActive(false);
                objects.Enqueue(createdObject);
            });

            _poolDictionary.Add(pool.poolType, objects);
        }
    }

    public GameObject GetObjectFromPool(PoolType poolType, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(poolType) || _poolDictionary[poolType].Count == 0) return null;

        GameObject createdObject = _poolDictionary[poolType].Dequeue();

        createdObject.SetActive(true);
        createdObject.transform.position = position;
        createdObject.transform.rotation = rotation;

        return createdObject;
    }

    public void AddObjectToPool(PoolType poolType, GameObject returnedObject)
    {
        returnedObject.SetActive(false);
        if (returnedObject.TryGetComponent(out Rigidbody objectsRB)) objectsRB.velocity = new Vector3(0, 0, 0);

        _poolDictionary[poolType].Enqueue(returnedObject);
    }
}