﻿using System.Collections.Generic;

public class Pawn : Piece
{
    public override string TypeCode => "p";

    private int _verticalMoveMultiplier => ColorCode == "w" ? 1 : -1;

    public override void UpdateSupposedMoves(Square squareWithPiece)
    {
        if (squareWithPiece == null) return;
        SupposedMoves.Clear();

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

        if (!IsFirstMove) return;
        
        var secondMoveSquare = squareWithPiece.Board.SquareWithCoordinates(x, y + 2 * _verticalMoveMultiplier);
        if (secondMoveSquare == null) return;
        if (secondMoveSquare.PieceOnIt == null) SupposedMoves.Add(secondMoveSquare);
    }

    private void TryAddSquareThatPawnAttacks(Square square, List<Square> supposedSquaresForMove)
    {
        if (square == null) return;
        if (square.PieceOnIt != null && square.PieceOnIt.ColorCode != ColorCode)
            supposedSquaresForMove.Add(square);
    }
}