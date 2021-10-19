using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override PieceType MyType => PieceType.Bishop;
    
    private List<Square> _attackTurns = new List<Square>();

    public override List<Square> GetPossibleAttackTurns(Square square)
    {
        _attackTurns.Clear();

        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        Vector2Int upRightDir = new Vector2Int(1, 1);
        Vector2Int upLeftDir = new Vector2Int(1, -1);
        Vector2Int downRightDir = new Vector2Int(-1, -1);
        Vector2Int downLeftDir = new Vector2Int(-1, 1);

        FindPossibleAttackTurns(square.Coordinates, upRightDir);
        FindPossibleAttackTurns(square.Coordinates, upLeftDir);
        FindPossibleAttackTurns(square.Coordinates, downLeftDir);
        FindPossibleAttackTurns(square.Coordinates, downRightDir);

        return _gameManager.Analyzer.GetMovesWithoutCheck(SingletonRegistry.Instance.Board, square, _attackTurns, ActionType.AttackMove); ;
    }

    private void FindPossibleAttackTurns(Vector2Int pieceCoordinats, Vector2Int rowDirection)
    {
        for (int i = 1; i < SingletonRegistry.Instance.Board.Size.x; i++)
        {
            var square = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(pieceCoordinats.x + i * rowDirection.x, pieceCoordinats.y + i * rowDirection.y);

            if (square == SingletonRegistry.Instance.Board.GhostSquare)
                break;

            if (PieceStandsOnSquare(square))
            {
                if (PieceOnSquareHasOppositeColor(square))
                    _attackTurns.Add(square);

                break;
            }
        }
    }

    public override List<Square> GetPossibleMoveTurns(Square square)
    {
        _attackTurns = new List<Square>();

        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        List<Square> turns = new List<Square>();

        Vector2Int upRightDir = new Vector2Int(1, 1);
        Vector2Int upLeftDir = new Vector2Int(1, -1);
        Vector2Int downRightDir = new Vector2Int(-1, -1);
        Vector2Int downLeftDir = new Vector2Int(-1, 1);

        AddPossibleTurnsInDiagonal(turns, square.Coordinates, upRightDir);
        AddPossibleTurnsInDiagonal(turns, square.Coordinates, upLeftDir);
        AddPossibleTurnsInDiagonal(turns, square.Coordinates, downLeftDir);
        AddPossibleTurnsInDiagonal(turns, square.Coordinates, downRightDir);

        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetMovesWithoutCheck(SingletonRegistry.Instance.Board, square, turns, ActionType.Movement);
        
        return moves;
    }

    private void AddPossibleTurnsInDiagonal(List<Square> turns, Vector2Int pieceCoordinats, Vector2Int rowDirection)
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