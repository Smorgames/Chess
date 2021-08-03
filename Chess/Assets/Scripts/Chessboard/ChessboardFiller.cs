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
                Square cell = Instantiate(_squarePrefabs[(x + y) % _squarePrefabs.Length], transform.position, Quaternion.identity).GetComponent<Square>();

                cell.SetCoordinates(x, y);

                cell.transform.position = transform.position + new Vector3(x * _offset.x, y * _offset.y, transform.position.z);

                cell.transform.parent = _squareHandler.Board.transform;

                cell.name = $"Cell №{x * _chessBoardLength + y}";

                _squareHandler.Board.Squares[x, y] = cell;
            }
        }

        _squareHandler.DeactivateAllSquares();
    }

    private void Start()
    {
        InitializeChessBoardParameters();
        FillChessBoardCellArray();
    }
}