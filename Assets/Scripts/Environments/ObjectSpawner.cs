using UnityEngine;

public class ObjectSpawner : BaseTriggerable
{
    [SerializeField] private GameObject spawnObject;

    public override void HandleTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<IDamageable>() != null) SpawnStone();
    }

    private void SpawnStone()
    {
        if (spawnObject.TryGetComponent(out IPoolable poolableObject))
        {
            GameObject stone = ObjectPool.instance.GetObjectFromPool(poolableObject.poolType, transform.position, transform.rotation);

            if (stone == null) return;
        }
    }
}
