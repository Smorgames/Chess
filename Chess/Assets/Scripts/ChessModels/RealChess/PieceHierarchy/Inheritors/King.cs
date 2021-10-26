using System.Collections.Generic;
using UnityEngine;

public class King : RealPiece
{
    public override string TypeCode => "K";

    private List<ISquare> _attacks = new List<ISquare>();
    private List<ISquare> _moves = new List<ISquare>();

    public override List<ISquare> GetMoves(ISquare square) => SquaresBasedOnActionType(square, ActionType.Movement);
    public override List<ISquare> GetAttacks(ISquare square) => SquaresBasedOnActionType(square, ActionType.Attack);
    private List<ISquare> SquaresBasedOnActionType(ISquare square, ActionType actionType)
    {
        ClearMovesAndAttacks();

        var x = square.Coordinates.x;
        var y = square.Coordinates.y;

        var coordinatesList = new List<Vector2Int>()
        {
            new Vector2Int(x, y + 1), new Vector2Int(x + 1, y + 1), new Vector2Int(x + 1, y), new Vector2Int(x + 1, y - 1), 
            new Vector2Int(x, y - 1), new Vector2Int(x - 1, y - 1), new Vector2Int(x - 1, y), new Vector2Int(x - 1, y + 1)
        };

        foreach (var coordinates in coordinatesList)
        {
            var supposedMove = square.Board.SquareWithCoordinates(coordinates);

            if (!supposedMove.IsGhost)
            {
                if (PieceStandsOnSquare(supposedMove))
                {
                    if (PieceOnSquareHasOppositeColor(supposedMove))
                        _attacks.Add(supposedMove);
                }
                else
                    _moves.Add(supposedMove);
            }
        }

        if (actionType == ActionType.Attack) return _attacks;
        if (actionType == ActionType.Movement) return _moves;
        return null;
    }
    private void ClearMovesAndAttacks()
    {
        _attacks.Clear();
        _moves.Clear();
    }

    protected override void ResetAttackTurns()
    {
        ClearMovesAndAttacks();
    }
}