using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    [SerializeField] private List<ITriggerable> _triggerableObjects;

    private void OnTriggerEnter(Collider other)
    {
        foreach (ITriggerable item in _triggerableObjects)
        {
            item.HandleTrigger(other);
        }
    }
}
