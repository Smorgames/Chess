using System.Collections.Generic;
using UnityEngine;

public interface IChessBoard
{
    Vector2Int Size { get; }
    ISquare[,] Squares { get; }
    string WhoseTurn { get; set; }
    ISquare GhostSquare { get; }

    ISquare GetSquareWithCoordinates(int x, int y);
    ISquare GetSquareWithCoordinates(Vector2Int coordinates);
}

public interface IRealChessBoard : IChessBoard
{
    IRealSquare[,] RealSquares { get; }
    IRealSquare RealGhostSquare { get; set; }
    List<IHighlightable> HighlightableSquares();
    IRealSquare GetRealSquareWithPiece(IRealPiece realPiece);
    IRealSquare GetRealSquareWithCoordinates(int x, int y);
    IRealSquare GetRealSquareWithCoordinates(Vector2Int coordinates);
}

public interface IAbsChessBoard : IChessBoard
{
    
}