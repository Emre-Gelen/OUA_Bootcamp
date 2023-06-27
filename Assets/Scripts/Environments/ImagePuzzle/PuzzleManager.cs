using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance = null;

    [SerializeField] private GameObject _puzzlePart;
    [SerializeField] private GameObject _puzzleSpace;

    [Space(10)]
    [SerializeField] private Transform _puzzleArea;
    [SerializeField] private GameObject _puzzleCreatingAnimation;

    [Space(10)]
    [SerializeField] private int _rowCount;
    [SerializeField] private int _columnCount;
    [SerializeField] private float _puzzlePartGap = 1.5f;

    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        CreatePuzzle();
    }

    public void CreatePuzzle()
    {
        CreatePuzzlePartSpaces();
    }

    private void CreatePuzzlePartSpaces()
    {
        for (int column = 0; column < _columnCount; column++)
        {
            for (int row = 0; row < _rowCount; row++)
            {
                GameObject puzzleSpace = Instantiate(_puzzleSpace, new Vector3(_puzzleArea.position.x - ((_rowCount / 2 - row) * _puzzlePartGap), .5f, _puzzleArea.position.z - ((_columnCount / 2 - column) * _puzzlePartGap)), _puzzleArea.rotation);
                puzzleSpace.transform.parent = _puzzleArea;
                CreatePuzzlePiece(puzzleSpace, row, column);
            }
        }
    }

    private void CreatePuzzlePiece(GameObject rightPlace, int row, int column)
    {
        if (_puzzleCreatingAnimation != null) Instantiate(_puzzleCreatingAnimation);

        GameObject puzzlePiece = Instantiate(_puzzlePart, new Vector3(_puzzleArea.position.x - ((_rowCount / 2 - row) * _puzzlePartGap), .5f, _puzzleArea.position.z - ((_columnCount / 2 - column) * _puzzlePartGap)), _puzzleArea.rotation);
        puzzlePiece.transform.parent = _puzzleArea;
        puzzlePiece.GetComponent<PuzzlePiece>().SetRightPlace(rightPlace);
    }
}
