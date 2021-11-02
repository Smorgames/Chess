using System.Collections.Generic;

public class ChessPlayer
{
    public NewPieceColor TeamColor { get; set; }
    public List<NewPiece> PlayerPieces { get; private set; }

    public ChessPlayer(NewPieceColor teamColor)
    {
        TeamColor = teamColor;
    }

    public void AddPiece(NewPiece piece)
    {
        if (!PlayerPieces.Contains(piece))
            PlayerPieces.Add(piece);
    }

    public void RemovePiece(NewPiece piece)
    {
        if (PlayerPieces.Contains(piece))
            PlayerPieces.Remove(piece);
    }
}