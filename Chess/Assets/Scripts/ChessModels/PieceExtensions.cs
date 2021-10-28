using System.Collections.Generic;
using UnityEngine;

public static class PieceExtensions
{
    private static Vector2Int _up = Vector2Int.up;
    private static Vector2Int _down = Vector2Int.down;
    private static Vector2Int _left= Vector2Int.left;
    private static Vector2Int _right = Vector2Int.right;

    private static Vector2Int _upRight = new Vector2Int(1, 1); 
    private static Vector2Int _upLeft = new Vector2Int(-1, 1); 
    private static Vector2Int _downRight = new Vector2Int(1, -1); 
    private static Vector2Int _downLeft = new Vector2Int(-1, -1);

    private static bool PieceStandsOnSquare(IPiece piece, ISquare square) => square.PieceOnIt != null;
    private static bool PieceOnSquareHasOppositeColor(IPiece piece, ISquare square) => square.PieceOnIt.ColorCode != piece.ColorCode;
    
    // Pawn moves and attacks
    public static List<ISquare> PawnSquaresForAction(this IPawn pawn, ISquare square, ActionType actionType)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;
        var supposedSquares = new List<ISquare>();

        if (actionType == ActionType.Attack)
        {
            var firstSquare = square.Board.SquareWithCoordinates(x + 1, y + 1 * pawn.MoveDirection);
            var secondSquare = square.Board.SquareWithCoordinates(x - 1, y + 1 * pawn.MoveDirection);

            if (PieceStandsOnSquare(pawn.MyIPiece, firstSquare) && PieceOnSquareHasOppositeColor(pawn.MyIPiece, firstSquare))
                supposedSquares.Add(firstSquare);

            if (PieceStandsOnSquare(pawn.MyIPiece, secondSquare) && PieceOnSquareHasOppositeColor(pawn.MyIPiece, secondSquare))
                supposedSquares.Add(secondSquare);

            return Analyzer.MovesWithoutCheckForKing(square, supposedSquares, actionType);
        }

        var firstMoveSquare = square.Board.SquareWithCoordinates(x, y + 1 * pawn.MoveDirection);
        if (!TryAddSupposedMoveToList(firstMoveSquare, supposedSquares) || !pawn.MyIPiece.IsFirstMove)
            return Analyzer.MovesWithoutCheckForKing(square, supposedSquares, actionType);
        
        var secondMoveSquare = square.Board.SquareWithCoordinates(x, y + 2 * pawn.MoveDirection);
        TryAddSupposedMoveToList(secondMoveSquare, supposedSquares);
        return Analyzer.MovesWithoutCheckForKing(square, supposedSquares, actionType);
    }
    private static bool TryAddSupposedMoveToList(ISquare square, List<ISquare> supposedMoves)
    {
        if (square.PieceOnIt != null) return false;

        supposedMoves.Add(square);
        return true;
    }
    
    // King moves and attacks
    public static List<ISquare> KingSquaresForAction(this IKing king, ISquare square, ActionType actionType)
    {
        var directions = new List<Vector2Int>() { _up, _down, _left, _right, _upRight, _upLeft, _downRight, _downLeft };
        return SquaresForActionsOfKingAndKnight(king, square, directions, actionType);
    }
    private static List<ISquare> SquaresForActionsOfKingAndKnight(IOneSquareMovingPiece piece, ISquare square, List<Vector2Int> directions, ActionType actionType)
    {
        var kingCoordinates = square.Coordinates;
        var supposedSquares = new List<ISquare>();

        foreach (var direction in directions)
        {
            var supposed = square.Board.SquareWithCoordinates(kingCoordinates + direction);

            if (supposed.IsGhost) continue;
            
            if (actionType == ActionType.Attack)
            {
                if (PieceStandsOnSquare(piece.MyIPiece, supposed) && PieceOnSquareHasOppositeColor(piece.MyIPiece, supposed))
                    supposedSquares.Add(supposed);
            }
            else
            {
                if (!PieceStandsOnSquare(piece.MyIPiece, supposed))
                    supposedSquares.Add(supposed);
            }
        }

        return Analyzer.MovesWithoutCheckForKing(square, supposedSquares, actionType);
    }
    
    // Knight moves and attacks
    public static List<ISquare> KnightSquaresForAction(this IKnight knight, ISquare square, ActionType actionType)
    {
        var directions = new List<Vector2Int>()
        {
            _upRight + _up, _upRight + _right,
            _upLeft + _up, _upLeft + _left,
            _downRight + _down, _downRight + _right,
            _downLeft + _down, _downLeft + _left
        };

        return SquaresForActionsOfKingAndKnight(knight, square, directions, actionType);
    }

    // Rook moves and attacks
    public static List<ISquare> RookSquaresForAction(this IRook rook, ISquare square, ActionType actionType)
    {
        var directions = new List<Vector2Int>() { _up, _down, _right, _left };
        return IterativeSquareAddition(rook, square, directions, actionType);
    }
    
    // Bishop moves and attacks
    public static List<ISquare> BishopSquaresForAction(this IBishop bishop, ISquare square, ActionType actionType)
    {
        var directions = new List<Vector2Int>() { _upRight, _upLeft, _downRight, _downLeft };
        return IterativeSquareAddition(bishop, square, directions, actionType);
    }
    
    // Queen moves and attacks
    public static List<ISquare> QueenSquaresForAction(this IQueen queen, ISquare square, ActionType actionType)
    {
        var directions = new List<Vector2Int>() { _up, _down, _right, _left, _upRight, _upLeft, _downRight, _downLeft };
        return IterativeSquareAddition(queen, square, directions, actionType);
    }

    private static List<ISquare> IterativeSquareAddition(IIterativelyMovingPiece piece, ISquare square, List<Vector2Int> directions, ActionType actionType)
    {
        var supposedSquares = new List<ISquare>();
        var longestBoardSide = square.Board.Size.x >= square.Board.Size.y ? square.Board.Size.x : square.Board.Size.y;
        
        foreach (var direction in directions)
        {
            for (int i = 1; i < longestBoardSide; i++)
            {
                var supposed = square.Board.SquareWithCoordinates(square.Coordinates.x + direction.x * i, square.Coordinates.y + direction.y * i);

                if (supposed.IsGhost) break;
                
                if (PieceStandsOnSquare(piece.MyIPiece, supposed))
                {
                    if (PieceOnSquareHasOppositeColor(piece.MyIPiece, supposed) && actionType == ActionType.Attack)
                        supposedSquares.Add(supposed);

                    break;
                }
                
                if (actionType == ActionType.Movement)
                    supposedSquares.Add(supposed);
            }
        }

        return Analyzer.MovesWithoutCheckForKing(square, supposedSquares, actionType);
    }
}