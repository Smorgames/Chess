using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece, IPawnDirection
{
    public override PieceType MyType => PieceType.Pawn;

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

        List<Square> attackTurns = new List<Square>();

        Square firstSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 1 * MoveDirection, y + 1 * MoveDirection);
        Square secondSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 1 * MoveDirection, y + 1 * MoveDirection);

        if (PieceStandsOnSquare(firstSquare) && IsPieceOnSquareHasOppositeColor(firstSquare))
            attackTurns.Add(firstSquare);

        if (PieceStandsOnSquare(secondSquare) && IsPieceOnSquareHasOppositeColor(secondSquare))
            attackTurns.Add(secondSquare);

        return attackTurns;
    }
    
    public override List<Square> GetPossibleMoveTurns(Square square)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;
        
        var supposedMoves = new List<Square>();
        var board = SingletonRegistry.Instance.Board;

        var firstMoveSquare = board.GetSquareWithCoordinates(x, y + 1 * MoveDirection);
        if (!TryAddSupposedMoveToList(firstMoveSquare, supposedMoves) || !_isFirstMove)
            return MovesWithoutCheck(board, square, supposedMoves);
        
        var secondMoveSquare = board.GetSquareWithCoordinates(x, y + 2 * MoveDirection);
        TryAddSupposedMoveToList(secondMoveSquare, supposedMoves);
        return MovesWithoutCheck(board, square, supposedMoves);
    }

    private bool TryAddSupposedMoveToList(Square square, List<Square> supposedMoves)
    {
        if (PieceStandsOnSquare(square))
            return false;

        supposedMoves.Add(square);
        return true;
    }

    private List<Square> MovesWithoutCheck(Chessboard board, Square squareWithPiece, List<Square> supposedMoves)
    {
        return _gameManager.Analyzer.GetMovesWithoutCheck(board, squareWithPiece, supposedMoves);
    }
}