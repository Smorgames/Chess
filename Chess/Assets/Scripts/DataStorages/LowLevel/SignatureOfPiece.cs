using UnityEngine;

public class SignatureOfPiece
{
    public readonly Vector2Int SquareCoordinates;
    public readonly Piece Piece;

    public SignatureOfPiece(Vector2Int squareCoordinates, Piece piece)
    {
        SquareCoordinates = squareCoordinates;
        Piece = piece;
    }
}