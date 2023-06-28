using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance = null;
    public bool CheckPiecesPlaces = false;

    [SerializeField] private GameObject _puzzlePart;
    [SerializeField] private GameObject _puzzleSpace;

    [Space(10)]
    [SerializeField] private Transform _puzzleArea;
    [SerializeField] private GameObject _puzzleCreatingAnimation;

    [Space(10)]
    [SerializeField] private int _rowCount;
    [SerializeField] private int _columnCount;
    [SerializeField] private float _puzzlePartGap = 1.5f;

    [SerializeField]private Texture2D _image;
    private int _pieceWidth;
    private int _pieceHeight;
    private int[,][] _randomLocations;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        CreatePuzzle();
    }

    private void Update()
    {
        if (CheckPiecesPlaces)
        {
            CheckPiecesPlaces = false;
            Debug.Log(CheckPieces());
            //TODO: Add a door or something to open when the puzzle is done.
        }
    }

    public void CreatePuzzle()
    {
        _pieceWidth = _image.width / _columnCount;
        _pieceHeight = _image.height / _rowCount;
        CreateRandomLocations();
        CreatePuzzlePartSpaces();
    }

    private void CreateRandomLocations()
    {
        _randomLocations = new int[_rowCount, _columnCount][];

        for (int row = 0; row < _rowCount; row++)
        {
            for (int column = 0; column < _columnCount; column++)
            {
                _randomLocations[row, column] = new int[] {row, column};
            }
        }

        Shuffle(new System.Random(), _randomLocations);
    }

    private void Shuffle<T>(System.Random random, T[,] array)
    {
        int lengthRow = array.GetLength(1);

        for (int i = array.Length - 1; i > 0; i--)
        {
            int i0 = i / lengthRow;
            int i1 = i % lengthRow;

            int j = random.Next(i + 1);
            int j0 = j / lengthRow;
            int j1 = j % lengthRow;

            T temp = array[i0, i1];
            array[i0, i1] = array[j0, j1];
            array[j0, j1] = temp;
        }
    }

    private void CreatePuzzlePartSpaces()
    {
        for (int column = -1; column < _columnCount + 1; column++)
        {
            for (int row = -1; row < _rowCount + 1; row++)
            {
                GameObject puzzleSpace = Instantiate(_puzzleSpace, new Vector3(_puzzleArea.position.x - ((_rowCount / 2 - row) * _puzzlePartGap), .5f, _puzzleArea.position.z - ((_columnCount / 2 - column) * _puzzlePartGap)), _puzzleArea.rotation);
                puzzleSpace.transform.parent = _puzzleArea;
                if (column > -1 && column < _columnCount && -1 < row && row < _rowCount) CreatePuzzlePiece(puzzleSpace, row, column);
            }
        }
    }

    private void CreatePuzzlePiece(GameObject rightPlace, int row, int column)
    {
        if (_puzzleCreatingAnimation != null) Instantiate(_puzzleCreatingAnimation);

        GameObject puzzlePiece = Instantiate(_puzzlePart, new Vector3(_puzzleArea.position.x - ((_rowCount / 2 - _randomLocations[row, column][0]) * _puzzlePartGap), .5f, _puzzleArea.position.z - ((_columnCount / 2 - _randomLocations[row, column][1]) * _puzzlePartGap)), _puzzleArea.rotation);
        puzzlePiece.transform.parent = _puzzleArea;
        puzzlePiece.GetComponent<PuzzlePiece>().SetRightPlace(rightPlace);

        Texture2D _croppedTexture = new Texture2D(_image.width / _columnCount, _image.height / _rowCount);
        _croppedTexture.SetPixels(_image.GetPixels(column * _pieceWidth, (_rowCount - (row + 1)) * _pieceHeight, _pieceWidth, _pieceHeight));
        _croppedTexture.Apply();
        puzzlePiece.transform.Find("ImageSurface").GetComponent<Renderer>().material.mainTexture = _croppedTexture;
    }

    private bool CheckPieces()
    {
        return _puzzleArea.GetComponentsInChildren<PuzzlePiece>().All(puzzlePiece => puzzlePiece.IsOnRightPlace);
    }
}
