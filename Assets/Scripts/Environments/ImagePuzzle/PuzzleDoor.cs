using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    [SerializeField] private float _direction;
    [SerializeField] private float _duration;

    private bool _isOpen;

    public void OnOpening()
    {
        if (!_isOpen)
        {
            transform.DOLocalMoveY(_direction, _duration);
        }
    }
}
