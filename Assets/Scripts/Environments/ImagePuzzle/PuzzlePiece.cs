using UnityEngine;
using UnityEngine.UIElements;

public class PuzzlePiece : Movable
{
    public bool IsOnRightPlace;

    [SerializeField] private GameObject _rightPlace;
    [SerializeField] private float _threshold = .4f;
    private void FixedUpdate()
    {
        IsOnRightPlace = Vector3.Distance(transform.position, _rightPlace.transform.position) < _threshold;
    }

    public void SetRightPlace(GameObject RightPlace)
    {
        _rightPlace = RightPlace;
    }
}
