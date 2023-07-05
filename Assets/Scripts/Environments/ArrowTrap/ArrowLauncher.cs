using UnityEngine;

public class ArrowLauncher : BaseTriggerable
{
    [SerializeField] private int _arrowCount;
    [SerializeField] private bool _hasInfiniteArrow;
    [SerializeField] private float _launcherForce = 40f;

    //Use it if arrow will be launched after a few trigger.
    [SerializeField] private int _triggerCount;
    private int _remainingTriggerCount;

    public override void HandleTriggerEnter(Collider collider)
    {
        if (_remainingTriggerCount == 0 && collider.gameObject.GetComponent<IDamageable>() != null) LaunchArrow();
        else if(_remainingTriggerCount > 0)
        {
            _remainingTriggerCount--;
        }
    }

    private void LaunchArrow()
    {
        if (!_hasInfiniteArrow && _arrowCount == 0) return;

        GameObject arrow = ObjectPool.instance.GetObjectFromPool(PoolType.Arrow, transform.position, transform.rotation);

        if (arrow == null) return;
        arrow.GetComponent<Rigidbody>().AddForce(Vector3.forward * _launcherForce, ForceMode.Impulse);

        _remainingTriggerCount = _triggerCount;
        _arrowCount--;
    }
}
