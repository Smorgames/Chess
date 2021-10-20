using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override PieceType MyType => PieceType.Rook;
    public override string TypeCodeValue => "r";
    private List<Square> _attackTurns = new List<Square>();

    public override List<Square> GetPossibleAttackTurns(Square square)
    {
        return _gameManager.Analyzer.GetMovesWithoutCheck(square, _attackTurns, ActionType.Attack); ;
    }

    public override List<Square> GetPossibleMoveTurns(Square square)
    {
        _attackTurns.Clear();

        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        List<Square> turns = new List<Square>();

        Vector2Int upDir = new Vector2Int(0, 1);
        Vector2Int downDir = new Vector2Int(0, -1);
        Vector2Int rightDir = new Vector2Int(1, 0);
        Vector2Int leftDir = new Vector2Int(-1, 0);

        AddPossibleTurnsInRow(turns, square, upDir);
        AddPossibleTurnsInRow(turns, square, downDir);
        AddPossibleTurnsInRow(turns, square, rightDir);
        AddPossibleTurnsInRow(turns, square, leftDir);

        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetMovesWithoutCheck(square, turns, ActionType.Movement);
        
        return moves;
    }

    private void AddPossibleTurnsInRow(List<Square> turns, Square squareWithPiece, Vector2Int rowDirection)
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