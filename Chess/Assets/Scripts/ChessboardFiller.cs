using UnityEngine;

public class ChessboardFiller : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private int _chessBoardLength;
    [SerializeField] private int _chessBoardHeight;

    [SerializeField] private Vector2 _offset;

    [SerializeField] private GameObject[] _squarePrefabs;

    public void InitializeChessboard(Chessboard chessboard)
    {
        var size = new Vector2Int(_chessBoardLength, _chessBoardHeight);
        chessboard.InitializeChessboard(size);
        
        for (int x = 0; x < _chessBoardLength; x++)
        {
            for (int y = 0; y < _chessBoardHeight; y++)
            {
                var coordinates = new Vector2Int(x, y);
                
                CreateAndSetSquare(coordinates, chessboard);
            }
        }

        SingletonRegistry.Instance.Board.DeactivateAllSquares();
    }

    private void CreateAndSetSquare(Vector2Int coordinates, Chessboard chessboard)
    {
        var index = (coordinates.x + coordinates.y) % _squarePrefabs.Length;
        var square = Instantiate(_squarePrefabs[index], transform.position, Quaternion.identity).GetComponent<Square>();
        var squareName = $"Square {coordinates.x * _chessBoardLength + coordinates.y}";
        SetSquare(square, coordinates, squareName, chessboard);

        chessboard.Squares[coordinates.x, coordinates.y] = square;
    }

    private void SetSquare(Square square, Vector2Int coordinates, string squareName, Chessboard chessboard)
    {
        square.SetCoordinates(coordinates.x, coordinates.y);
        square.name = squareName;

        square.transform.position = transform.position + new Vector3(coordinates.x * _offset.x, coordinates.y * _offset.y, transform.position.z);
        square.transform.parent = chessboard.transform;
    }
}