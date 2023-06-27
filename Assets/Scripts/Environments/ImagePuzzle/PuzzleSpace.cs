using DG.Tweening;
using UnityEngine;
using static UnityEditor.Progress;

public class PuzzleSpace : MonoBehaviour
{
    [SerializeField] private LayerMask _puzzlePieceLayer;

    [Space(10)]
    [SerializeField] private float _pullingCenterThreshold = .15f;
    [SerializeField] private float _pullingCenterRadius = .2f;

    private void Update()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, _pullingCenterRadius, _puzzlePieceLayer);
        if (items.Length > 0)
        {
            PuzzlePiece puzzlePiece = items[0].gameObject.GetComponent<PuzzlePiece>();
            if (!puzzlePiece.IsHeld && Vector3.Distance(items[0].transform.position, transform.position) > _pullingCenterThreshold)
            {
                puzzlePiece.transform.DOMoveX(transform.position.x, 1f);
                puzzlePiece.transform.DOMoveZ(transform.position.z, 1f);
            }
        }
    }
}
