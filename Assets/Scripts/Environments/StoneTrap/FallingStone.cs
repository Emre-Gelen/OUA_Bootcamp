using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour, IPoolable
{
    public PoolType poolType => PoolType.FallingStoneType1;

    [SerializeField] private float _damage = 40f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable otherGO)) otherGO.OnDamage(_damage);
    }
}
