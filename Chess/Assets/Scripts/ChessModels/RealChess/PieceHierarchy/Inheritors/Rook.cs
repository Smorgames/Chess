using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override PieceType MyType => PieceType.Rook;
    public override string TypeCode => "r";
    private List<IRealSquare> _attackTurns = new List<IRealSquare>();

    public override List<IRealSquare> GetAttacks(IRealSquare square)
    {
        return _attackTurns;
    }

    public override List<IRealSquare> GetMoves(IRealSquare square)
    {
        _attackTurns.Clear();

        var x = square.Coordinates.x;
        var y = square.Coordinates.y;

        var supposedMoves = new List<IRealSquare>();

        var upDir = new Vector2Int(0, 1);
        var downDir = new Vector2Int(0, -1);
        var rightDir = new Vector2Int(1, 0);
        var leftDir = new Vector2Int(-1, 0);

        AddPossibleTurnsInRow(supposedMoves, square, upDir);
        AddPossibleTurnsInRow(supposedMoves, square, downDir);
        AddPossibleTurnsInRow(supposedMoves, square, rightDir);
        AddPossibleTurnsInRow(supposedMoves, square, leftDir);
        
        return supposedMoves;
    }

    private void AddPossibleTurnsInRow(List<IRealSquare> turns, IRealSquare squareWithPiece, Vector2Int rowDirection)
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

            if (!turns.Contains(square))
                turns.Add(square);
        }
    }

    protected override void ResetAttackTurns()
    {
        _attackTurns.Clear();
    }
}