using System;
using UnityEngine;

public abstract class BaseTriggerable : MonoBehaviour
{
    public abstract void HandleTrigger(Collider collider);
}
