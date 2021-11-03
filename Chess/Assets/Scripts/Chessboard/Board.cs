using UnityEngine;

public class Board : MonoBehaviour
{
    public Vector2Int Size => _size;
    private Vector2Int _size;

    public Square[,] Squares => _squares;
    private Square[,] _squares;

    private Cache _cache = new Cache();

    public void Initialize(Vector2Int boardSize)
    {
        _size = boardSize;
        _squares = new Square[_size.x, _size.y];
    }

    public Square SquareWithCoordinates(int x, int y)
    {
        if (x >= 0 && x < _size.x && y >= 0 && y < _size.y) return _squares[x, y];
        return null;
    }
    public Square SquareWithCoordinates(Vector2Int coordinates) => SquareWithCoordinates(coordinates.x, coordinates.y);

    public Square SquareWithPiece(Piece piece)
    {
        for (int x = 0; x < _size.x; x++)
        for (int y = 0; y < _size.y; y++)
        {
            var square = _squares[x, y];
            if (square != null && Equals(square.PieceOnIt, piece)) return square;
        }

        return null;
    }
    
    public void PlacePieceFromSquareToOther(Square fromSquare, Square toSquare)
    {
        var piece = fromSquare.PieceOnIt;
        if (piece == null) return;
        
        _cache.DoCache(fromSquare, toSquare);

        fromSquare.PieceOnIt = null;
        toSquare.PieceOnIt = piece;
    }
    
    public void UNDO_PlacePieceFromSquareToOther()
    {
        _cache.ToSquare.PieceOnIt = _cache.ToPiece;
        _cache.FromSquare.PieceOnIt = _cache.FromPiece;
    }
    
    private class Cache
    {
        public Piece FromPiece;
        public Square FromSquare;

        public Piece ToPiece;
        public Square ToSquare;

        public void DoCache(Square fromSquare, Square toSquare)
        {
            ToSquare = toSquare;
            FromSquare = fromSquare;
            ToPiece = toSquare.PieceOnIt;
            FromPiece = fromSquare.PieceOnIt;
        }
    }
}