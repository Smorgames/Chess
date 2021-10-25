using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override PieceType MyType => PieceType.Bishop;
    public override string TypeCode => "b";

    private List<IRealSquare> _attackTurns = new List<IRealSquare>();

    public override List<IRealSquare> GetAttacks(IRealSquare square)
    {
        _attackTurns.Clear();

        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        Vector2Int upRightDir = new Vector2Int(1, 1);
        Vector2Int upLeftDir = new Vector2Int(1, -1);
        Vector2Int downRightDir = new Vector2Int(-1, -1);
        Vector2Int downLeftDir = new Vector2Int(-1, 1);

        FindPossibleAttackTurns(square, upRightDir);
        FindPossibleAttackTurns(square, upLeftDir);
        FindPossibleAttackTurns(square, downLeftDir);
        FindPossibleAttackTurns(square, downRightDir);

        return _attackTurns;
    }

    private void FindPossibleAttackTurns(IRealSquare squareWithPiece, Vector2Int rowDirection)
    {
        for (int i = 1; i < squareWithPiece.Board.Size.x; i++)
        {
            var square = squareWithPiece.Board.GetSquareWithCoordinates(squareWithPiece.Coordinates.x + i * rowDirection.x, squareWithPiece.Coordinates.y + i * rowDirection.y);

            if (square == squareWithPiece.Board.GhostSquare)
                break;

            if (PieceStandsOnSquare(square))
            {
                if (PieceOnSquareHasOppositeColor(square))
                    _attackTurns.Add(square);

                break;
            }
        }
    }

    public override List<IRealSquare> GetMoves(IRealSquare square)
    {
        _attackTurns = new List<IRealSquare>();

        var x = square.Coordinates.x;
        var y = square.Coordinates.y;

        var supposedMoves = new List<IRealSquare>();

        var upRightDir = new Vector2Int(1, 1);
        var upLeftDir = new Vector2Int(1, -1);
        var downRightDir = new Vector2Int(-1, -1);
        var downLeftDir = new Vector2Int(-1, 1);

        AddPossibleTurnsInDiagonal(supposedMoves, square, upRightDir);
        AddPossibleTurnsInDiagonal(supposedMoves, square, upLeftDir);
        AddPossibleTurnsInDiagonal(supposedMoves, square, downLeftDir);
        AddPossibleTurnsInDiagonal(supposedMoves, square, downRightDir);

        return supposedMoves;
    }

    private void AddPossibleTurnsInDiagonal(List<IRealSquare> turns, IRealSquare squareWithPiece, Vector2Int rowDirection)
    {
        for (int i = 1; i < squareWithPiece.Board.Size.x; i++)
        {
            var square = squareWithPiece.Board.GetSquareWithCoordinates(squareWithPiece.Coordinates.x + i * rowDirection.x, squareWithPiece.Coordinates.y + i * rowDirection.y);

            if (square == squareWithPiece.Board.GhostSquare)
                break;

            if (PieceStandsOnSquare(square))
            {
                if (PieceOnSquareHasOppositeColor(square))
                {
                    _attackTurns.Add(square);
                    break;
                }
                else
                    break;
            }

            turns.Add(square);
        }
    }

    protected override void ResetAttackTurns()
    {
        _attackTurns.Clear();
    }
}