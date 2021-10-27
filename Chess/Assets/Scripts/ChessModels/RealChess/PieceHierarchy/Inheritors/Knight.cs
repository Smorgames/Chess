using System.Collections.Generic;
using UnityEngine;

public class Knight : RealPiece
{
    public override string TypeCode => "k";

    public override List<ISquare> GetAttacks(ISquare square) => SquareListBasedOnActionType(square, ActionType.Attack);
    
    public override List<ISquare> GetMoves(ISquare square) => SquareListBasedOnActionType(square, ActionType.Movement);

    private List<ISquare> SquareListBasedOnActionType(ISquare square, ActionType actionType)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;

        var supposedMoves = new List<ISquare>();
        var supposedSquaresCoordinates = new List<Vector2Int>()
        {
            new Vector2Int(x + 2, y + 1), new Vector2Int(x + 2, y - 1), new Vector2Int(x - 2, y + 1), new Vector2Int(x - 2, y - 1), 
            new Vector2Int(x + 1, y + 2), new Vector2Int(x + 1, y - 2), new Vector2Int(x - 1, y + 2), new Vector2Int(x - 1, y - 2)
        };

        foreach (var coordinates in supposedSquaresCoordinates)
        {
            var supposedMove = square.Board.SquareWithCoordinates(coordinates);

            if (actionType == ActionType.Movement)
                if (!PieceStandsOnSquare(supposedMove) && !supposedMove.IsGhost)
                    supposedMoves.Add(supposedMove);
            

            if (actionType == ActionType.Attack)
                if (!supposedMove.IsGhost && PieceStandsOnSquare(supposedMove) && PieceOnSquareHasOppositeColor(supposedMove))
                    supposedMoves.Add(supposedMove);
        }
        
        return Analyzer.MovesWithoutCheckForKing(square, supposedMoves, actionType);
    }
}