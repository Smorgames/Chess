using UnityEngine;

public class ChessboardFiller : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private int _chessBoardLength;
    [SerializeField] private int _chessBoardHeight;

    [SerializeField] private Vector2 _offset;

    [SerializeField] private GameObject[] _squarePrefabs;

    [Header("References")]
    [SerializeField] private SquareHandler _squareHandler;

    private void InitializeChessBoardParameters()
    {
        _squareHandler.Board.InitializeChessboard(_chessBoardLength, _chessBoardHeight);
    }

    private void FillChessBoardCellArray()
    {
        for (int x = 0; x < _chessBoardLength; x++)
        {
            for (int y = 0; y < _chessBoardHeight; y++)
            {
                Square square = 
                    Instantiate(_squarePrefabs[(x + y) % _squarePrefabs.Length], transform.position, Quaternion.identity).GetComponent<Square>();

                SquareSetup(square, x, y);

                _squareHandler.Board.Squares[x, y] = square;
            }
        }

        _squareHandler.DeactivateAllSquares();
    }

    private void SquareSetup(Square square, int x, int y)
    {
        square.SetCoordinates(x, y);
        square.transform.position = transform.position + new Vector3(x * _offset.x, y * _offset.y, transform.position.z);
        square.transform.parent = _squareHandler.Board.transform;
        square.name = $"Cell №{x * _chessBoardLength + y}";
    }

    private void Start()
    {
        InitializeChessBoardParameters();
        FillChessBoardCellArray();
    }
}