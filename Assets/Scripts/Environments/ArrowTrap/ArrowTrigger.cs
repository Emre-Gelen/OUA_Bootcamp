using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    [SerializeField, SerializeReference] private List<BaseTriggerable> _triggerableObjects;

    private void OnTriggerEnter(Collider other)
    {
        foreach (BaseTriggerable item in _triggerableObjects)
        {
            item.HandleTrigger(other);
        }
    }
}
