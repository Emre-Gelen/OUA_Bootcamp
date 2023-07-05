using System;
using UnityEngine;

public abstract class BaseTriggerable : MonoBehaviour
{
    public abstract void HandleTriggerEnter(Collider collider);
    public virtual void HandleTriggerExit(Collider collider) { }
}
