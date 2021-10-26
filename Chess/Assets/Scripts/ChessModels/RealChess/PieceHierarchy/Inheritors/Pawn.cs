using System.Collections.Generic;

public class Pawn : RealPiece, IPawnDirection
{
    public override string TypeCode => "p";
    public int MoveDirection => ColorCode == "w" ? 1 : -1;

    public override List<ISquare> GetAttacks(ISquare square)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;

        var supposedAttackMoves = new List<ISquare>();

        var firstSquare = square.Board.SquareWithCoordinates(x + 1 * MoveDirection, y + 1 * MoveDirection);
        var secondSquare = square.Board.SquareWithCoordinates(x - 1 * MoveDirection, y + 1 * MoveDirection);

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

        var firstMoveSquare = square.Board.SquareWithCoordinates(x, y + 1 * MoveDirection);
        if (!TryAddSupposedMoveToList(firstMoveSquare, supposedMoves) || !_isFirstMove)
            return supposedMoves;
        
        var secondMoveSquare = square.Board.SquareWithCoordinates(x, y + 2 * MoveDirection);
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