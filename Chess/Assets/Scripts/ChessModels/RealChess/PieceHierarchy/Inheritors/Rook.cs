using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override PieceType MyType => PieceType.Rook;
    private List<Square> _attackTurns = new List<Square>();

    public override List<Square> GetPossibleAttackTurns(Square square)
    {
        return _gameManager.Analyzer.GetMovesWithoutCheck(SingletonRegistry.Instance.Board, square, _attackTurns, ActionType.AttackMove); ;
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

        AddPossibleTurnsInRow(turns, square.Coordinates, upDir);
        AddPossibleTurnsInRow(turns, square.Coordinates, downDir);
        AddPossibleTurnsInRow(turns, square.Coordinates, rightDir);
        AddPossibleTurnsInRow(turns, square.Coordinates, leftDir);

        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetMovesWithoutCheck(SingletonRegistry.Instance.Board, square, turns, ActionType.Movement);
        
        return moves;
    }

    private void AddPossibleTurnsInRow(List<Square> turns, Vector2Int pieceCoordinats, Vector2Int rowDirection)
    {
        for (int i = 1; i < SingletonRegistry.Instance.Board.Size.x; i++)
        {
            Square square = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(pieceCoordinats.x + i * rowDirection.x, pieceCoordinats.y + i * rowDirection.y);

            if (square == SingletonRegistry.Instance.Board.GhostSquare)
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