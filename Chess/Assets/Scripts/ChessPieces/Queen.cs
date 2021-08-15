using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    private List<Square> _attackTurns;

    public override List<Square> GetPossibleAttackTurns(Square squareWithThis)
    {
        return _attackTurns;
    }

    public override List<Square> GetPossibleMoveTurns(Square squareWithThis)
    {
        _attackTurns = new List<Square>();

        int x = squareWithThis.Coordinates.x;
        int y = squareWithThis.Coordinates.y;

        List<Square> turns = new List<Square>();

        Vector2Int upDir = new Vector2Int(0, 1);
        Vector2Int downDir = new Vector2Int(0, -1);
        Vector2Int rightDir = new Vector2Int(1, 0);
        Vector2Int leftDir = new Vector2Int(-1, 0);
        Vector2Int upRightDir = new Vector2Int(1, 1);
        Vector2Int upLeftDir = new Vector2Int(1, -1);
        Vector2Int downRightDir = new Vector2Int(-1, -1);
        Vector2Int downLeftDir = new Vector2Int(-1, 1);

        AddPossibleTurnsInLine(turns, squareWithThis.Coordinates, upDir);
        AddPossibleTurnsInLine(turns, squareWithThis.Coordinates, downDir);
        AddPossibleTurnsInLine(turns, squareWithThis.Coordinates, rightDir);
        AddPossibleTurnsInLine(turns, squareWithThis.Coordinates, leftDir);
        AddPossibleTurnsInLine(turns, squareWithThis.Coordinates, upRightDir);
        AddPossibleTurnsInLine(turns, squareWithThis.Coordinates, upLeftDir);
        AddPossibleTurnsInLine(turns, squareWithThis.Coordinates, downLeftDir);
        AddPossibleTurnsInLine(turns, squareWithThis.Coordinates, downRightDir);

        return turns;
    }

    private void AddPossibleTurnsInLine(List<Square> turns, Vector2Int pieceCoordinats, Vector2Int rowDirection)
    {
        for (int i = 1; i < Chessboard.Instance.Length; i++)
        {
            Square square = _squareHandler.GetSquareWithCoordinates(pieceCoordinats.x + i * rowDirection.x, pieceCoordinats.y + i * rowDirection.y);

            if (square == _squareHandler.GhostSquare)
                break;

            if (IsPieceStandsOnSquare(square))
            {
                if (IsPieceOnSquareHasOppositeColor(square))
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
}