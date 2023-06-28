using UnityEngine;
using UnityEngine.UIElements;

public class PuzzlePiece : Movable
{
    public bool IsOnRightPlace;

    [SerializeField] private GameObject _rightPlace;
    [SerializeField] private float _threshold = .4f;

    private void Update()
    {
        bool newValue = Vector3.Distance(transform.position, _rightPlace.transform.position) < _threshold;
        if (!IsOnRightPlace && newValue) PuzzleManager.instance.CheckPiecesPlaces = true;
        IsOnRightPlace = newValue;
    }

    public void SetRightPlace(GameObject RightPlace)
    {
        _rightPlace = RightPlace;
    }
}
