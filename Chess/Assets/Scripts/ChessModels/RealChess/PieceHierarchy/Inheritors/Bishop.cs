using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override PieceType MyType => PieceType.Bishop;
    public override string TypeCodeValue => "b";

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

        FindPossibleAttackTurns(square, upRightDir);
        FindPossibleAttackTurns(square, upLeftDir);
        FindPossibleAttackTurns(square, downLeftDir);
        FindPossibleAttackTurns(square, downRightDir);

        return _gameManager.Analyzer.GetMovesWithoutCheck(square, _attackTurns, ActionType.Attack); ;
    }

    private void FindPossibleAttackTurns(Square squareWithPiece, Vector2Int rowDirection)
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

        AddPossibleTurnsInDiagonal(turns, square, upRightDir);
        AddPossibleTurnsInDiagonal(turns, square, upLeftDir);
        AddPossibleTurnsInDiagonal(turns, square, downLeftDir);
        AddPossibleTurnsInDiagonal(turns, square, downRightDir);

        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetMovesWithoutCheck(square, turns, ActionType.Movement);
        
        return moves;
    }

    private void AddPossibleTurnsInDiagonal(List<Square> turns, Square squareWithPiece, Vector2Int rowDirection)
    {
        for (int i = 1; i < squareWithPiece.Board.Size.x; i++)
        {
            Square square = squareWithPiece.Board.GetSquareWithCoordinates(squareWithPiece.Coordinates.x + i * rowDirection.x, squareWithPiece.Coordinates.y + i * rowDirection.y);

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