using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ClosingWall : MonoBehaviour
{
    [SerializeField] private float _transitionTime;
    [SerializeField] private float _openAfterSeconds;
    [SerializeField] private Transform _closePoint;

    private bool _isClosed;
    private Vector3 _basePosition;

    private void Start()
    {
        _basePosition = transform.position;
    }

    public void HandleMove()
    {
        if (!_isClosed)
        {
            transform.DOLocalMove(_closePoint.position, _transitionTime);
            _isClosed = true;
            StartCoroutine(OpenDoor());
        }
      
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(_openAfterSeconds);
        transform.DOLocalMove(_basePosition, _transitionTime);
        _isClosed = false;
    }
}
