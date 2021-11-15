public class PiecePlacer
{
    private MoveCache _moveCache = new MoveCache();
    
    public void PlacePieceFromSquareToOther(Square fromSquare, Square toSquare)
    {
        var fromPiece = fromSquare.PieceOnIt;
        if (fromPiece == null) return;
        
        var toPiece = toSquare.PieceOnIt;
        
        _moveCache.DoCache(fromSquare, toSquare);

        fromSquare.PieceOnIt = null;
        toSquare.PieceOnIt = fromPiece;

        if (toPiece != null)
        {
            var toPieceColor = toPiece.ColorCode;
            var player = Game.Instance.GetPlayerBasedOnColorCode(toPieceColor);
            player.RemovePiece(toPiece);
        }
    }
    
    public void UNDO_PlacePieceFromSquareToOther()
    {
        _moveCache.ToSquare.PieceOnIt = _moveCache.ToPiece;
        _moveCache.FromSquare.PieceOnIt = _moveCache.FromPiece;

        if (_moveCache.ToPiece != null)
        {
            var toPieceColor = _moveCache.ToPiece.ColorCode;
            var player = Game.Instance.GetPlayerBasedOnColorCode(toPieceColor);
            player.AddPiece(_moveCache.ToPiece);
        }
    }
}

public class MoveCache
{
    public Piece FromPiece;
    public Square FromSquare;

    public Piece ToPiece;
    public Square ToSquare;

    public bool IsFirstMove;

    public void DoCache(Square fromSquare, Square toSquare)
    {
        ToSquare = toSquare;
        FromSquare = fromSquare;
        ToPiece = toSquare.PieceOnIt;
        FromPiece = fromSquare.PieceOnIt;
        IsFirstMove = FromPiece.IsFirstMove;
    }
}