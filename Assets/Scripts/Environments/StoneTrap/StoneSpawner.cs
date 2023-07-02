using UnityEngine;

public class StoneSpawner : BaseTriggerable
{
    public override void HandleTrigger(Collider collider)
    {
        if (collider.gameObject.GetComponent<IDamageable>() != null) SpawnStone();
    }

    private void SpawnStone()
    {
        GameObject stone = ObjectPool.instance.GetObjectFromPool(PoolType.Stone, transform.position, transform.rotation);

        if (stone == null) return;
    }
}
