using System.IO;
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

    [SerializeField]private Texture2D _image;
    private int _pieceWidth;
    private int _pieceHeight;

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
        _pieceWidth = _image.width / _columnCount;
        _pieceHeight = _image.height / _rowCount;
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

        Texture2D _croppedTexture = new Texture2D(_image.width / _columnCount, _image.height / _rowCount);
        _croppedTexture.SetPixels(_image.GetPixels(column * _pieceWidth, (_rowCount - (row + 1)) * _pieceHeight, _pieceWidth, _pieceHeight));
        _croppedTexture.Apply();
        puzzlePiece.transform.Find("ImageSurface").GetComponent<Renderer>().material.mainTexture = _croppedTexture;
    }
}
