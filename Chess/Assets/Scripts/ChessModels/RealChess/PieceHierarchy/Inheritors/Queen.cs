using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override PieceType MyType => PieceType.Queen;
    public override string TypeCodeValue => "Q";

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
        Vector2Int upRightDir = new Vector2Int(1, 1);
        Vector2Int upLeftDir = new Vector2Int(1, -1);
        Vector2Int downRightDir = new Vector2Int(-1, -1);
        Vector2Int downLeftDir = new Vector2Int(-1, 1);

        AddPossibleTurnsInLine(turns, square, upDir);
        AddPossibleTurnsInLine(turns, square, downDir);
        AddPossibleTurnsInLine(turns, square, rightDir);
        AddPossibleTurnsInLine(turns, square, leftDir);
        AddPossibleTurnsInLine(turns, square, upRightDir);
        AddPossibleTurnsInLine(turns, square, upLeftDir);
        AddPossibleTurnsInLine(turns, square, downLeftDir);
        AddPossibleTurnsInLine(turns, square, downRightDir);
        
        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetMovesWithoutCheck(square, turns, ActionType.Movement);

        return moves;
    }

    private void AddPossibleTurnsInLine(List<Square> turns, Square squareWithPiece, Vector2Int rowDirection)
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