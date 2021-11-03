using System.Collections.Generic;

public class ChessPlayer
{
    public readonly string TeamColor;
    public List<Piece> PlayerPieces { get; private set; } = new List<Piece>();

    public ChessPlayer(string teamColor)
    {
        TeamColor = teamColor;
    }

    public void UpdatePiecesSupposedMoves()
    {
        foreach (var piece in PlayerPieces)
        {
            var pieceSquare = GameManager.Instance.GameBoard.SquareWithPiece(piece);
            piece.UpdateSupposedMoves(pieceSquare);
        }
    }

    public void AddPiece(Piece piece)
    {
        if (!PlayerPieces.Contains(piece))
            PlayerPieces.Add(piece);
    }

    public void RemovePiece(Piece piece)
    {
        if (PlayerPieces.Contains(piece))
            PlayerPieces.Remove(piece);
    }
}