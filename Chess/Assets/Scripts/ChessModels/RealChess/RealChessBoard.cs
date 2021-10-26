using UnityEngine;

public class RealChessBoard : MonoBehaviour, IChessBoard
{
    public Vector2Int Size => _size;
    private Vector2Int _size;

    public ISquare GhostSquare => _ghostRealSquare;
    public RealSquare RealGhostSquare { get => _ghostRealSquare; set => _ghostRealSquare = value;}
    private RealSquare _ghostRealSquare;

    public string WhoseTurn { get; set; }

    public ISquare[,] Squares
    {
        get
        {
            if (_squares != null) return _squares;
            
            var length = _realSquares.GetLength(0);
            var height = _realSquares.GetLength(1);
            _squares = new ISquare[length, height];

            for (int x = 0; x < length; x++)
            for (int y = 0; y < height; y++)
                _squares[x, y] = _realSquares[x, y];

            return _squares;
        }
    }
    private ISquare[,] _squares;

    public RealSquare[,] RealSquares => _realSquares;
    private RealSquare[,] _realSquares;

    public void InitializeChessboard(Vector2Int size)
    {
        _size.x = size.x;
        _size.y = size.y;
        
        _realSquares = new RealSquare[_size.x, _size.y];
    }

    public RealSquare GetRealSquareWithPiece(RealPiece realPiece)
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                var square = _realSquares[x, y];

                if (square.RealRealPieceOnIt == realPiece)
                    return square;
            }
        }

        return _ghostRealSquare;
    }

    public ISquare SquareWithCoordinates(Vector2Int coordinates) => SquareWithCoordinates(coordinates.x, coordinates.y);
    public ISquare SquareWithCoordinates(int x, int y) => RealSquareWithCoordinates(x, y);
    public RealSquare RealSquareWithCoordinates(Vector2Int coordinates) => RealSquareWithCoordinates(coordinates.x, coordinates.y);
    private RealSquare RealSquareWithCoordinates(int x, int y)
    {
        if (x >= 0 && x < _size.x && y >= 0 && y < _size.y)
            return _realSquares[x, y];
        
        return _ghostRealSquare;
    }
}