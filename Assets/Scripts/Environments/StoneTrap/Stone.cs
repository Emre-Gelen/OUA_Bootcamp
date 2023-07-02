using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IPoolable
{
    public PoolType poolType => PoolType.Stone;

    [SerializeField] private float _damage = 40f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable otherGO)) otherGO.OnDamage(_damage);
    }

    //IEnumerator AddToPool()
    //{
    //    yield return new WaitForSeconds(5f);
    //    ObjectPool.instance.AddObjectToPool(poolType, gameObject);
    //}
}
