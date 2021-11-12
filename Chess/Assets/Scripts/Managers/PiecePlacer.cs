public class PiecePlacer
{
    private Cache _cache = new Cache();
    
    public void PlacePieceFromSquareToOther(Square fromSquare, Square toSquare)
    {
        var fromPiece = fromSquare.PieceOnIt;
        if (fromPiece == null) return;
        
        var toPiece = toSquare.PieceOnIt;
        
        _cache.DoCache(fromSquare, toSquare);

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
        _cache.ToSquare.PieceOnIt = _cache.ToPiece;
        _cache.FromSquare.PieceOnIt = _cache.FromPiece;

        if (_cache.ToPiece != null)
        {
            var toPieceColor = _cache.ToPiece.ColorCode;
            var player = Game.Instance.GetPlayerBasedOnColorCode(toPieceColor);
            player.AddPiece(_cache.ToPiece);
        }
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