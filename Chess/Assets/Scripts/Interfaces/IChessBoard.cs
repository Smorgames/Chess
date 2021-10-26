using UnityEngine;

public interface IChessBoard
{
    Vector2Int Size { get; }
    ISquare[,] Squares { get; }
    string WhoseTurn { get; set; }
    ISquare GhostSquare { get; }

    ISquare SquareWithCoordinates(int x, int y);
    ISquare SquareWithCoordinates(Vector2Int coordinates);
}