using System.Collections.Generic;
using UnityEngine;

public class Bishop : RealPiece
{
    public override PieceType MyType => PieceType.Bishop;
    public override string TypeCode => "b";
    
    private List<ISquare> _attacks = new List<ISquare>();
    private List<ISquare> _moves = new List<ISquare>();

    public override List<ISquare> GetMoves(ISquare square) => SquaresListBasedOnActionType(square, ActionType.Movement);
    public override List<ISquare> GetAttacks(ISquare square) => SquaresListBasedOnActionType(square, ActionType.Attack);

    private List<ISquare> SquaresListBasedOnActionType(ISquare realSquare, ActionType actionType)
    {
        ClearMovesAndAttackLists();
        var directions = new List<Vector2Int>() { new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-1, 1) };

        foreach (var direction in directions)
            FillMovesAndAttackLists(realSquare, direction);
        
        if (actionType == ActionType.Attack) return _attacks;
        if (actionType == ActionType.Movement) return _moves;
        return null;
    }
    
    private void ClearMovesAndAttackLists()
    {
        _attacks.Clear();
        _moves.Clear();
    }

    private void FillMovesAndAttackLists(ISquare currentSquare, Vector2Int rowDirection)
    {
        var longestBoardSide = currentSquare.Board.Size.x >= currentSquare.Board.Size.y ? currentSquare.Board.Size.x : currentSquare.Board.Size.y;
        
        for (int i = 1; i < longestBoardSide; i++)
        {
            var x = currentSquare.Coordinates.x + i * rowDirection.x;
            var y = currentSquare.Coordinates.y + i * rowDirection.y;
            var nextSquare = currentSquare.Board.SquareWithCoordinates(x, y);

            if (nextSquare == currentSquare.Board.GhostSquare) break;

            if (PieceStandsOnSquare(nextSquare))
            {
                if (PieceOnSquareHasOppositeColor(nextSquare))
                    _attacks.Add(nextSquare);
                
                break;
            }

            _moves.Add(nextSquare);
        }
    }

    protected override void ResetAttackTurns()
    {
        ClearMovesAndAttackLists();
    }
}