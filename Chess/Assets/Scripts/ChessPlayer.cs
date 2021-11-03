using System.Collections.Generic;
using UnityEngine;

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

    public void SetPiecesTransparency(float alpha)
    {
        foreach (var piece in PlayerPieces)
        {
            var color = piece.Renderer.color;
            color.a = alpha;
            piece.Renderer.color = color;
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