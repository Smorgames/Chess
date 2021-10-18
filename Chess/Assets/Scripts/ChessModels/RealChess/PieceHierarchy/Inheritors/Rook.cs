using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public override PieceType MyType => PieceType.Rook;
    private List<Square> _attackTurns = new List<Square>();

    public override List<Square> GetPossibleAttackTurns(Square squareWithThis)
    {
        return _attackTurns;
    }

    public override List<Square> GetPossibleMoveTurns(Square squareWithThis)
    {
        _attackTurns.Clear();

        int x = squareWithThis.Coordinates.x;
        int y = squareWithThis.Coordinates.y;

        List<Square> turns = new List<Square>();

        Vector2Int upDir = new Vector2Int(0, 1);
        Vector2Int downDir = new Vector2Int(0, -1);
        Vector2Int rightDir = new Vector2Int(1, 0);
        Vector2Int leftDir = new Vector2Int(-1, 0);

        AddPossibleTurnsInRow(turns, squareWithThis.Coordinates, upDir);
        AddPossibleTurnsInRow(turns, squareWithThis.Coordinates, downDir);
        AddPossibleTurnsInRow(turns, squareWithThis.Coordinates, rightDir);
        AddPossibleTurnsInRow(turns, squareWithThis.Coordinates, leftDir);

        return turns;
    }

    private void AddPossibleTurnsInRow(List<Square> turns, Vector2Int pieceCoordinats, Vector2Int rowDirection)
    {
        for (int i = 1; i < SingletonRegistry.Instance.Board.Size.x; i++)
        {
            Square square = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(pieceCoordinats.x + i * rowDirection.x, pieceCoordinats.y + i * rowDirection.y);

            if (square == _squareHandler.GhostSquare)
                break;

            if (IsPieceStandsOnSquare(square))
            {
                if (IsPieceOnSquareHasOppositeColor(square))
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