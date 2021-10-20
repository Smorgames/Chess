using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece, IPawnDirection
{
    public override PieceType MyType => PieceType.Pawn;
    public override string TypeCodeValue => "p";

    public int MoveDirection
    {
        get
        {
            var isWhite = MyColor == PieceColor.White;
            return isWhite ? 1 : -1;
        }
    }

    public override List<Square> GetPossibleAttackTurns(Square square)
    {
        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        List<Square> supposedAttackMoves = new List<Square>();

        Square firstSquare = square.Board.GetSquareWithCoordinates(x + 1 * MoveDirection, y + 1 * MoveDirection);
        Square secondSquare = square.Board.GetSquareWithCoordinates(x - 1 * MoveDirection, y + 1 * MoveDirection);

        if (PieceStandsOnSquare(firstSquare) && PieceOnSquareHasOppositeColor(firstSquare))
            supposedAttackMoves.Add(firstSquare);

        if (PieceStandsOnSquare(secondSquare) && PieceOnSquareHasOppositeColor(secondSquare))
            supposedAttackMoves.Add(secondSquare);

        return MovesWithoutCheck(square, supposedAttackMoves, ActionType.Attack);
    }
    
    public override List<Square> GetPossibleMoveTurns(Square square)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;
        
        var supposedMoves = new List<Square>();
        var board = square.Board;

        var firstMoveSquare = board.GetSquareWithCoordinates(x, y + 1 * MoveDirection);
        if (!TryAddSupposedMoveToList(firstMoveSquare, supposedMoves) || !_isFirstMove)
            return MovesWithoutCheck(square, supposedMoves, ActionType.Movement);
        
        var secondMoveSquare = board.GetSquareWithCoordinates(x, y + 2 * MoveDirection);
        TryAddSupposedMoveToList(secondMoveSquare, supposedMoves);
        return MovesWithoutCheck(square, supposedMoves, ActionType.Movement);
    }

    private bool TryAddSupposedMoveToList(Square square, List<Square> supposedMoves)
    {
        if (PieceStandsOnSquare(square))
            return false;

        supposedMoves.Add(square);
        return true;
    }

    private List<Square> MovesWithoutCheck(Square squareWithPiece, List<Square> supposedMoves, ActionType actionType)
    {
        return _gameManager.Analyzer.GetMovesWithoutCheck(squareWithPiece, supposedMoves, actionType);
    }
}