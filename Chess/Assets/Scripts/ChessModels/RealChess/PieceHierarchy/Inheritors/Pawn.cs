using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece, IPawnDirection
{
    public override PieceType MyType => PieceType.Pawn;
    public override string TypeCode => "p";

    public int MoveDirection
    {
        get
        {
            var isWhite = ColorCode == "w";
            return isWhite ? 1 : -1;
        }
    }

    public override List<IRealSquare> GetAttacks(IRealSquare square)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;

        var supposedAttackMoves = new List<IRealSquare>();

        var firstSquare = square.Board.GetSquareWithCoordinates(x + 1 * MoveDirection, y + 1 * MoveDirection);
        var secondSquare = square.Board.GetSquareWithCoordinates(x - 1 * MoveDirection, y + 1 * MoveDirection);

        if (PieceStandsOnSquare(firstSquare) && PieceOnSquareHasOppositeColor(firstSquare))
            supposedAttackMoves.Add(firstSquare);

        if (PieceStandsOnSquare(secondSquare) && PieceOnSquareHasOppositeColor(secondSquare))
            supposedAttackMoves.Add(secondSquare);

        return /*MovesWithoutCheck(square, supposedAttackMoves, ActionType.Attack);*/ supposedAttackMoves;
    }
    
    public override List<IRealSquare> GetMoves(IRealSquare square)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;
        
        var supposedMoves = new List<IRealSquare>();
        var board = square.Board;

        var firstMoveSquare = board.GetSquareWithCoordinates(x, y + 1 * MoveDirection);
        if (!TryAddSupposedMoveToList(firstMoveSquare, supposedMoves) || !_isFirstMove)
            return /*MovesWithoutCheck(square, supposedMoves, ActionType.Movement);*/ supposedMoves;
        
        var secondMoveSquare = board.GetSquareWithCoordinates(x, y + 2 * MoveDirection);
        TryAddSupposedMoveToList(secondMoveSquare, supposedMoves);
        return /*MovesWithoutCheck(square, supposedMoves, ActionType.Movement);*/ supposedMoves;
    }

    private bool TryAddSupposedMoveToList(IRealSquare square, List<IRealSquare> supposedMoves)
    {
        if (PieceStandsOnSquare(square))
            return false;

        supposedMoves.Add(square);
        return true;
    }
}