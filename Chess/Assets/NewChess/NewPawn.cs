using System.Collections.Generic;

public class NewPawn : NewPiece
{
    private int _verticalMoveMultiplier => MySignature.MyColor == NewPieceColor.White ? 1 : -1;

    public override void UpdateSupposedMoves(NewSquare squareWithPiece)
    {
        base.UpdateSupposedMoves(squareWithPiece);
        
        var x = squareWithPiece.Coordinates.x;
        var y = squareWithPiece.Coordinates.y;
        
        var firstAttackSquare = squareWithPiece.Board.SquareWithCoordinates(x + 1, y + 1 * _verticalMoveMultiplier);
        var secondAttackSquare = squareWithPiece.Board.SquareWithCoordinates(x - 1, y + 1 * _verticalMoveMultiplier);
        
        TryAddSquareThatPawnAttacks(firstAttackSquare, SupposedMoves);
        TryAddSquareThatPawnAttacks(secondAttackSquare, SupposedMoves);

        var firstMoveSquare = squareWithPiece.Board.SquareWithCoordinates(x, y + 1 * _verticalMoveMultiplier);
        if (firstMoveSquare == null) return;
        if (firstMoveSquare.PieceOnIt == null) SupposedMoves.Add(firstMoveSquare);
        else return;

        if (!MySignature.IsFirstMove) return;
        
        var secondMoveSquare = squareWithPiece.Board.SquareWithCoordinates(x, y + 2 * _verticalMoveMultiplier);
        if (secondMoveSquare == null) return;
        if (secondMoveSquare.PieceOnIt == null) SupposedMoves.Add(secondMoveSquare);
    }

    private void TryAddSquareThatPawnAttacks(NewSquare square, List<NewSquare> supposedSquaresForMove)
    {
        if (square == null) return;
        if (square.PieceOnIt != null && square.PieceOnIt.MySignature.MyColor != MySignature.MyColor)
            supposedSquaresForMove.Add(square);
    }
}