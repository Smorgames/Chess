using System.Collections.Generic;
using UnityEngine;

public interface IChessBoard
{
    Vector2Int Size { get; }
    ISquare[,] Squares { get; }
    ISquare GhostSquare { get; set; }
    string WhoseTurn { get; set; }

    ISquare GetSquareWithCoordinates(int x, int y);
    ISquare GetSquareWithCoordinates(Vector2Int coordinates);
    ISquare GetSquareWithPiece(IPiece piece);
}

public interface IRealChessBoard : IChessBoard
{
    new IRealSquare[,] Squares { get; }
    List<IHighlightable> HighlightableSquares();
    new IRealSquare GetSquareWithCoordinates(int x, int y);
    new IRealSquare GetSquareWithCoordinates(Vector2Int coordinates);
    new IRealSquare GetSquareWithPiece(IPiece piece);
}

public interface IAbsChessBoard : IChessBoard
{
    new IAbsSquare[,] Squares { get; }
    new IAbsSquare GetSquareWithCoordinates(int x, int y);
    new IAbsSquare GetSquareWithCoordinates(Vector2Int coordinates);
    new IAbsSquare GetSquareWithPiece(IPiece piece);
}