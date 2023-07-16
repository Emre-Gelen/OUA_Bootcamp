using UnityEngine;

public class ObjectSpawner : BaseTriggerable
{
    [SerializeField] private PoolType _spawnObjectType;

    public override void HandleTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<IDamageable>() != null) SpawnStone();
    }

    private void SpawnStone()
    {
        GameObject stone = ObjectPool.instance.GetObjectFromPool(_spawnObjectType, transform.position, transform.rotation);

        if (stone == null) return;
    }
}
