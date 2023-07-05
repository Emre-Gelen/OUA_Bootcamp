using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField, SerializeReference] private List<BaseTriggerable> _triggerableObjects;

    private void OnTriggerEnter(Collider other)
    {
        foreach (BaseTriggerable item in _triggerableObjects)
        {
            item.HandleTriggerEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (BaseTriggerable item in _triggerableObjects)
        {
            item.HandleTriggerExit(other);
        }
    }
}
