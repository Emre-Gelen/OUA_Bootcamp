using UnityEngine;

public class PoolableCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPoolable poolableObject)) PoolObject(other.gameObject, poolableObject.poolType);
    }

    private void PoolObject(GameObject poolableObject, PoolType poolType)
    {
        ObjectPool.instance.AddObjectToPool(poolType, poolableObject);
    }
}
