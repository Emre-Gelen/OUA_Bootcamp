using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour, IPoolable 
{
    public PoolType poolType { get { return PoolType.Arrow; } }
    [SerializeField] private float _damage = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable otherGO)) otherGO.OnDamage(_damage);
        StartCoroutine(AddToPool());
    }

    IEnumerator AddToPool()
    {
        yield return new WaitForSeconds(1.2f);
        ObjectPool.instance.AddObjectToPool(poolType, gameObject);
    }
}
