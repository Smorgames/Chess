using System.Collections.Generic;

[System.Serializable]
public class ChessPlayer
{
    public readonly string TeamColor;

    public King MyKing { get; set; }
    public List<Piece> MyPieces { get; } = new List<Piece>();

    public ChessPlayer(string teamColor)
    {
        TeamColor = teamColor;
    }

    public void UpdatePiecesSupposedMoves()
    {
        foreach (var piece in MyPieces)
        {
            var pieceSquare = Game.Instance.GameBoard.SquareWithPiece(piece);
            piece.UpdateSupposedMoves(pieceSquare);
        }
    }

    public void DeactivatePiecesWhoCanNotMove()
    {
        foreach (var piece in MyPieces)
        {
            var pieceSquare = Game.Instance.GameBoard.SquareWithPiece(piece);
            piece.UpdateSupposedMoves(pieceSquare);
            var possibleMoves = CheckMateAnalyser.MovesWithoutCheckForKing(pieceSquare, piece.SupposedMoves);

            if (possibleMoves.Count <= 0) 
                piece.Deactivate();
        }
    }

    public void ActivatePieces()
    {
        foreach (var piece in MyPieces)
            piece.Activate();
    }
    
    public void DeactivatePieces()
    {
        foreach (var piece in MyPieces)
            piece.Deactivate();
    }

    public void AddPiece(Piece piece)
    {
        if (!MyPieces.Contains(piece))
            MyPieces.Add(piece);
    }

    public void RemovePiece(Piece piece)
    {
        if (MyPieces.Contains(piece))
            MyPieces.Remove(piece);
    }
}