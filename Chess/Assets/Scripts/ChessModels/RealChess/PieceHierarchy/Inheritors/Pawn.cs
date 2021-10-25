using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece, IPawnDirection
{
    public override PieceType MyType => PieceType.Pawn;
    public override string TypeCode => "p";
    public int MoveDirection => ColorCode == "w" ? 1 : -1;

    public override List<IRealSquare> GetRealAttacks(IRealSquare realSquare)
    {
        var x = realSquare.Coordinates.x;
        var y = realSquare.Coordinates.y;

        var supposedAttackMoves = new List<IRealSquare>();

        var firstSquare = realSquare.RealBoard.GetRealSquareWithCoordinates(x + 1 * MoveDirection, y + 1 * MoveDirection);
        var secondSquare = realSquare.RealBoard.GetRealSquareWithCoordinates(x - 1 * MoveDirection, y + 1 * MoveDirection);

        if (PieceStandsOnSquare(firstSquare) && PieceOnSquareHasOppositeColor(firstSquare))
            supposedAttackMoves.Add(firstSquare);

        if (PieceStandsOnSquare(secondSquare) && PieceOnSquareHasOppositeColor(secondSquare))
            supposedAttackMoves.Add(secondSquare);

        return supposedAttackMoves;
    }
    
    public override List<IRealSquare> GetRealMoves(IRealSquare realSquare)
    {
        var x = realSquare.Coordinates.x;
        var y = realSquare.Coordinates.y;
        
        var supposedMoves = new List<IRealSquare>();

        var firstMoveSquare = realSquare.RealBoard.GetRealSquareWithCoordinates(x, y + 1 * MoveDirection);
        if (!TryAddSupposedMoveToList(firstMoveSquare, supposedMoves) || !_isFirstMove)
            return supposedMoves;
        
        var secondMoveSquare = realSquare.RealBoard.GetRealSquareWithCoordinates(x, y + 2 * MoveDirection);
        TryAddSupposedMoveToList(secondMoveSquare, supposedMoves);
        return supposedMoves;
    }
    
    private bool TryAddSupposedMoveToList(IRealSquare realSquare, List<IRealSquare> supposedMoves)
    {
        if (PieceStandsOnSquare(realSquare))
            return false;

        supposedMoves.Add(realSquare);
        return true;
    }

    public override List<ISquare> GetAttacks(ISquare square)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;

        var supposedAttackMoves = new List<ISquare>();

        var firstSquare = square.Board.GetSquareWithCoordinates(x + 1 * MoveDirection, y + 1 * MoveDirection);
        var secondSquare = square.Board.GetSquareWithCoordinates(x - 1 * MoveDirection, y + 1 * MoveDirection);

        if (PieceStandsOnSquare(firstSquare) && PieceOnSquareHasOppositeColor(firstSquare))
            supposedAttackMoves.Add(firstSquare);

        if (PieceStandsOnSquare(secondSquare) && PieceOnSquareHasOppositeColor(secondSquare))
            supposedAttackMoves.Add(secondSquare);

        return supposedAttackMoves;
    }
    
    public override List<ISquare> GetMoves(ISquare square)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;
        
        var supposedMoves = new List<ISquare>();

        var firstMoveSquare = square.Board.GetSquareWithCoordinates(x, y + 1 * MoveDirection);
        if (!TryAddSupposedMoveToList(firstMoveSquare, supposedMoves) || !_isFirstMove)
            return supposedMoves;
        
        var secondMoveSquare = square.Board.GetSquareWithCoordinates(x, y + 2 * MoveDirection);
        TryAddSupposedMoveToList(secondMoveSquare, supposedMoves);
        return supposedMoves;
    }
    
    private bool TryAddSupposedMoveToList(ISquare square, List<ISquare> supposedMoves)
    {
        if (PieceStandsOnSquare(square))
            return false;

        supposedMoves.Add(square);
        return true;
    }
}