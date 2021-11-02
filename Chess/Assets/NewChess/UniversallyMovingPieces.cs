using System.Collections.Generic;
using UnityEngine;

public abstract class UniversallyMovingPieces : NewPiece
{
    protected static Vector2Int _up = Vector2Int.up;
    protected static Vector2Int _down = Vector2Int.down;
    protected static Vector2Int _left= Vector2Int.left;
    protected static Vector2Int _right = Vector2Int.right;

    protected static Vector2Int _upRight = new Vector2Int(1, 1); 
    protected static Vector2Int _upLeft = new Vector2Int(-1, 1); 
    protected static Vector2Int _downRight = new Vector2Int(1, -1); 
    protected static Vector2Int _downLeft = new Vector2Int(-1, -1);

    protected static List<NewSquare> IterativelyAddedSquares(NewSquare square, List<Vector2Int> directions)
    {
        var supposedSquares = new List<NewSquare>();
        var size = square.Board.Size;
        var longestBoardSide = size.x >= size.y ? size.x : size.y;
        
        foreach (var direction in directions)
        {
            for (int i = 1; i < longestBoardSide; i++)
            {
                var supposedMove = square.Board.SquareWithCoordinates(square.Coordinates.x + direction.x * i, square.Coordinates.y + direction.y * i);

                if (supposedMove == null) break;
                
                if (supposedMove.PieceOnIt != null)
                {
                    if (supposedMove.PieceOnIt.ColorCode != square.PieceOnIt.ColorCode)
                        supposedSquares.Add(supposedMove);
                    break;
                }
                
                supposedSquares.Add(supposedMove);
            }
        }

        return supposedSquares;
    }

    protected static List<NewSquare> OneDirectionOneMove(NewSquare square, List<Vector2Int> directions)
    {
        var supposedMoves = new List<NewSquare>();
        
        foreach (var direction in directions)
        {
            var supposedMove = square.Board.SquareWithCoordinates(square.Coordinates + direction);
            if (supposedMove == null) continue;

            if (supposedMove.PieceOnIt == null)
            {
                supposedMoves.Add(supposedMove);
                continue;
            }
            
            if (supposedMove.PieceOnIt.ColorCode != square.PieceOnIt.ColorCode) supposedMoves.Add(supposedMove);
        }

        return supposedMoves;
    }
}