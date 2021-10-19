using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override PieceType MyType => PieceType.Queen;
    
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
        Vector2Int upRightDir = new Vector2Int(1, 1);
        Vector2Int upLeftDir = new Vector2Int(1, -1);
        Vector2Int downRightDir = new Vector2Int(-1, -1);
        Vector2Int downLeftDir = new Vector2Int(-1, 1);

        AddPossibleTurnsInLine(turns, square.Coordinates, upDir);
        AddPossibleTurnsInLine(turns, square.Coordinates, downDir);
        AddPossibleTurnsInLine(turns, square.Coordinates, rightDir);
        AddPossibleTurnsInLine(turns, square.Coordinates, leftDir);
        AddPossibleTurnsInLine(turns, square.Coordinates, upRightDir);
        AddPossibleTurnsInLine(turns, square.Coordinates, upLeftDir);
        AddPossibleTurnsInLine(turns, square.Coordinates, downLeftDir);
        AddPossibleTurnsInLine(turns, square.Coordinates, downRightDir);
        
        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetMovesWithoutCheck(SingletonRegistry.Instance.Board, square, turns, ActionType.Movement);

        return moves;
    }

    private void AddPossibleTurnsInLine(List<Square> turns, Vector2Int pieceCoordinats, Vector2Int rowDirection)
    {
        for (int i = 1; i < SingletonRegistry.Instance.Board.Size.x; i++)
        {
            Square square = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(pieceCoordinats.x + i * rowDirection.x, pieceCoordinats.y + i * rowDirection.y);

            if (square == SingletonRegistry.Instance.Board.GhostSquare)
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