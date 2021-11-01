using UnityEngine;

public class NewBoard : MonoBehaviour
{
    public Vector2Int Size => _size;
    private Vector2Int _size;

    public NewSquare[,] Squares => _squares;
    private NewSquare[,] _squares;

    private Cache _cache = new Cache();

    public void Initialize(Vector2Int boardSize)
    {
        _size = boardSize;
        _squares = new NewSquare[_size.x, _size.y];
    }

    public NewSquare SquareWithCoordinates(int x, int y)
    {
        if (x >= 0 && x < _size.x && y >= 0 && y < _size.y) return _squares[x, y];
        return null;
    }
    public NewSquare SquareWithCoordinates(Vector2Int coordinates) => SquareWithCoordinates(coordinates.x, coordinates.y);

    public NewSquare SquareWithPiece(NewPiece piece)
    {
        for (int x = 0; x < _size.x; x++)
        for (int y = 0; y < _size.y; y++)
        {
            var square = _squares[x, y];
            
            if (square != null && Equals(square.PieceOnIt, piece)) return square;
        }

        return null;
    }
    
    public void PlacePieceFromSquareToOther(NewSquare oldSquare, NewSquare newSquare)
    {
        var piece = oldSquare.PieceOnIt;
        if (piece == null) return;
        
        _cache.DoCache(oldSquare, newSquare);

        oldSquare.PieceOnIt = null;
        newSquare.PieceOnIt = piece;
    }
    
    public void UNDO_PlacePieceFromSquareToOther()
    {
        _cache.OldSquare.PieceOnIt = _cache.OldPiece;
        _cache.NewSquare.PieceOnIt = _cache.NewPiece;
    }
    
    private class Cache
    {
        public NewPiece NewPiece;
        public NewSquare NewSquare;

        public NewPiece OldPiece;
        public NewSquare OldSquare;

        public void DoCache(NewSquare oldSquare, NewSquare newSquare)
        {
            OldSquare = oldSquare;
            NewSquare = newSquare;
            OldPiece = oldSquare.PieceOnIt;
            NewPiece = newSquare.PieceOnIt;
        }
    }
}