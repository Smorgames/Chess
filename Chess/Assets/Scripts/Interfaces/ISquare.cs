using UnityEngine;

public interface ISquare
{
    bool IsGhost { get; }
    Vector2Int Coordinates { get; }
    IPiece PieceOnIt { get; set; }
    IChessBoard Board { get; }
}

public interface IRealSquare : ISquare
{
    new IRealChessBoard Board { get; }
    new IRealPiece PieceOnIt { get; set; }
    IHighlightable DisplayComponent { get; }
}

public interface IAbsSquare : ISquare
{
    new IAbsChessBoard Board { get; }
    new IAbsPiece PieceOnIt { get; set; }
}