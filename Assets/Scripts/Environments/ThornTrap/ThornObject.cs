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

    [Space]
    [SerializeField] private bool _isTimedTrigger;
    [SerializeField] private float _triggerAfterSeconds = 2.5f;

    private void Start()
    {
        if (_isTimedTrigger)
        {
            TimedTrigger.CreateTimer(_triggerAfterSeconds, HandleTimedTrigger);
        }   
    }

    private void HandleTimedTrigger()
    {
        transform.DOLocalMoveY(_direction, _upTransitionDuration);
        StartCoroutine(HandleMove(-_direction, _downTransitionDuration, 1f));
    }

    public override void HandleTriggerEnter(Collider collider)
    {
        StartCoroutine(HandleMove(_direction, _upTransitionDuration, _transitionDelay));
    }

    public override void HandleTriggerExit(Collider collider)
    {
        StartCoroutine(HandleMove(-_direction, _downTransitionDuration, _transitionDelay));
    }

    IEnumerator HandleMove(float direction, float duration, float transitionDelay)
    {
        yield return new WaitForSeconds(transitionDelay);
        transform.DOLocalMoveY(direction, duration);
    }
}