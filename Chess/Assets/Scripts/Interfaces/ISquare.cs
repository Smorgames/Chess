using UnityEngine;

public interface ISquare
{
    bool IsGhost { get; }
    Vector2Int Coordinates { get; }
    IPiece PieceOnIt { get; }
    IChessBoard Board { get; }
}

public interface IRealSquare : ISquare
{
    Transform MyTransform { get; }
    IRealChessBoard RealBoard { get; }
    IRealPiece RealPieceOnIt { get; set; }
    IHighlightable DisplayComponent { get; }
}

public interface IAbsSquare : ISquare
{
    
}