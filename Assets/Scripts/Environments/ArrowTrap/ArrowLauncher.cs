using UnityEngine;

public class ArrowLauncher : BaseTriggerable
{
    [SerializeField] private int _arrowCount;
    [SerializeField] private bool _hasInfiniteArrow;
    [SerializeField] private float _launcherForce = 40f;

    //Use it if arrow will be launched after a few trigger.
    [SerializeField] private int _triggerCount;
    private int _remainingTriggerCount;

    private void Start()
    {
        _remainingTriggerCount = _triggerCount;   
    }

    public override void HandleTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<IDamageable>() != null)
        {
            if (--_remainingTriggerCount == 0)
            {
                LaunchArrow();
            }
        }
    }

    private void LaunchArrow()
    {
        if (!_hasInfiniteArrow && _arrowCount == 0) return;

        GameObject arrow = ObjectPool.instance.GetObjectFromPool(PoolType.Arrow, transform.position, Quaternion.Euler(0, 90 + transform.rotation.eulerAngles.y, 90) * transform.rotation);

        if (arrow == null) return;
        arrow.GetComponent<Rigidbody>().AddForce(transform.up * _launcherForce, ForceMode.Impulse);

        _remainingTriggerCount = _triggerCount;
        _arrowCount--;
    }
}
