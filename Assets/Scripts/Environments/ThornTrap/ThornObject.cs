using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ThornObject : BaseTriggerable
{
    [SerializeField] private float _transitionDelay = .5f;
    [SerializeField] private float _upTransitionDuration = .1f;
    [SerializeField] private float _downTransitionDuration = .3f;

    [Space(10)]
    [SerializeField] private float _direction = 1.2f;

    public override void HandleTriggerEnter(Collider collider)
    {
        StartCoroutine(HandleMove(_direction, _upTransitionDuration));
    }

    public override void HandleTriggerExit(Collider collider)
    {
        StartCoroutine(HandleMove(-_direction, _downTransitionDuration));
    }

    IEnumerator HandleMove(float direction, float duration)
    {
        yield return new WaitForSeconds(_transitionDelay);
        transform.DOLocalMoveY(direction, duration);
    }
}