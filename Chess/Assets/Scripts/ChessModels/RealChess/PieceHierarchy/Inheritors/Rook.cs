using System.Collections.Generic;
using UnityEngine;

public class Rook : RealPiece
{
    public override string TypeCode => "r";
    private List<ISquare> _attacks = new List<ISquare>();

    public override List<ISquare> GetAttacks(ISquare square) => _attacks;
    
    public override List<ISquare> GetMoves(ISquare square)
    {
        _attacks.Clear();

        var supposedMoves = new List<ISquare>();
        var directions = new List<Vector2Int>() { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0) };

        foreach (var direction in directions)
            AddPossibleTurnsInRow(supposedMoves, square, direction);

        return supposedMoves;
    }
    
    private void AddPossibleTurnsInRow(List<ISquare> supposedMoves, ISquare currentRealSquare, Vector2Int rowDirection)
    {
        for (int i = 1; i < currentRealSquare.Board.Size.x; i++)
        {
            var x = currentRealSquare.Coordinates.x + i * rowDirection.x;
            var y = currentRealSquare.Coordinates.y + i * rowDirection.y;
            var square = currentRealSquare.Board.SquareWithCoordinates(x, y);

            if (square == currentRealSquare.Board.GhostSquare) break;

            if (PieceStandsOnSquare(square))
            {
                if (PieceOnSquareHasOppositeColor(square))
                    _attacks.Add(square);
                
                break;
            }

            if (!supposedMoves.Contains(square))
                supposedMoves.Add(square);
        }
    }
    
    protected override void ResetAttackTurns()
    {
        _attacks.Clear();
    }
}