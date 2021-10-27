using System.Collections.Generic;
using UnityEngine;

public abstract class LinearlyMovingRealPiece : RealPiece
{
    protected static Vector2Int UpDirection = new Vector2Int(0, 1);
    protected static Vector2Int DownDirection = new Vector2Int(0, -1);
    protected static Vector2Int RightDirection = new Vector2Int(1, 0);
    protected static Vector2Int LeftDirection = new Vector2Int(-1, 0);
    
    protected static Vector2Int UpRightDirection = new Vector2Int(1, 1);
    protected static Vector2Int UpLeftDirection = new Vector2Int(-1, 1);
    protected static Vector2Int DownRightDirection = new Vector2Int(1, -1);
    protected static Vector2Int DownLeftDirection = new Vector2Int(-1, -1);
    
    protected List<ISquare> Attacks = new List<ISquare>();
    protected List<ISquare> Moves = new List<ISquare>();

    protected void FillAttacksAndMovesSquares(ISquare squareWithPiece, List<Vector2Int> directions)
    {
        ClearAttacksAndMoves();

        foreach (var direction in directions)
            FillAttacksAndMovesSquaresBasedOnOneDirection(squareWithPiece, direction);

        Attacks = Analyzer.MovesWithoutCheckForKing(squareWithPiece, Attacks, ActionType.Attack);
        Moves = Analyzer.MovesWithoutCheckForKing(squareWithPiece, Moves, ActionType.Movement);
    }

    private void FillAttacksAndMovesSquaresBasedOnOneDirection(ISquare squareWithPiece, Vector2Int direction)
    {
        var boardSize = squareWithPiece.Board.Size;
        var longestBoardSide = boardSize.x >= boardSize.y ? boardSize.x : boardSize.y;

        for (int i = 1; i < longestBoardSide; i++)
        {
            var x = squareWithPiece.Coordinates.x + i * direction.x;
            var y = squareWithPiece.Coordinates.y + i * direction.y;
            var square = squareWithPiece.Board.SquareWithCoordinates(x, y);

            if (square == squareWithPiece.Board.GhostSquare) break;

            if (PieceStandsOnSquare(square))
            {
                if (PieceOnSquareHasOppositeColor(square))
                    Attacks.Add(square);
                
                break;
            }

            if (!Moves.Contains(square))
                Moves.Add(square);
        }
    }

    protected void ClearAttacksAndMoves()
    {
        Attacks.Clear();
        Moves.Clear();
    }
}