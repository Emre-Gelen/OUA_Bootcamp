using DG.Tweening;
using UnityEngine;

public class PuzzleDoor : MonoBehaviour
{
    [SerializeField] private float _direction;
    [SerializeField] private float _duration;

    private bool _isOpen = false;

    public void HandleOpen()
    {
        if (!_isOpen)
        {
            transform.DOLocalMoveY(_direction, _duration);
        }
    }
}
