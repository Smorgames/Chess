using UnityEngine;

public class ChessToken
{
    public readonly Vector2Int Coordinates;
    public readonly PieceColor MyPieceColor;
    public readonly PieceType MyPieceType;

    public ChessToken(Vector2Int coordinates, PieceColor myPieceColor, PieceType myPieceType)
    {
        Coordinates = coordinates;
        MyPieceColor = myPieceColor;
        MyPieceType = myPieceType;
    }
}

public enum PieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

public enum PieceColor
{
    White,
    Black
}