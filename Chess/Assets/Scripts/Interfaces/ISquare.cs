using UnityEngine;

public interface ISquare
{
    bool IsGhost { get; set; }
    Vector2Int Coordinates { get; }
    IPiece PieceOnIt { get; }
    IChessBoard Board { get; }
}