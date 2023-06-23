using System;
using UnityEngine;

public class Movable : MonoBehaviour, IMovable
{
    private Rigidbody _rigidbody;

    public void Start()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }
}

