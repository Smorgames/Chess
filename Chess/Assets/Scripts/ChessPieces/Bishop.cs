﻿using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
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

        Vector2Int upRightDir = new Vector2Int(1, 1);
        Vector2Int upLeftDir = new Vector2Int(1, -1);
        Vector2Int downRightDir = new Vector2Int(-1, -1);
        Vector2Int downLeftDir = new Vector2Int(-1, 1);

        AddPossibleTurnsInDiagonal(turns, squareWithThis.Coordinates, upRightDir);
        AddPossibleTurnsInDiagonal(turns, squareWithThis.Coordinates, upLeftDir);
        AddPossibleTurnsInDiagonal(turns, squareWithThis.Coordinates, downLeftDir);
        AddPossibleTurnsInDiagonal(turns, squareWithThis.Coordinates, downRightDir);

        return turns;
    }

    private void AddPossibleTurnsInDiagonal(List<Square> turns, Vector2Int pieceCoordinats, Vector2Int rowDirection)
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