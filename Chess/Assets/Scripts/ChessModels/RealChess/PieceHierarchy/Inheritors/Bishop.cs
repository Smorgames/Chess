using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override PieceType MyType => PieceType.Bishop;
    public override string TypeCode => "b";

    private List<IRealSquare> _attackTurns = new List<IRealSquare>();

    public override List<IRealSquare> GetRealAttacks(IRealSquare realSquare)
    {
        _attackTurns.Clear();

        var directions = new List<Vector2Int>() { new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-1, 1) };

        foreach (var direction in directions)
            FindPossibleAttackTurns(realSquare, direction);
        
        return _attackTurns;
    }

    private void FindPossibleAttackTurns(IRealSquare currentSquare, Vector2Int rowDirection)
    {
        for (int i = 1; i < currentSquare.Board.Size.x; i++)
        {
            var x = currentSquare.Coordinates.x + i * rowDirection.x;
            var y = currentSquare.Coordinates.y + i * rowDirection.y;
            var nextSquare = currentSquare.RealBoard.GetRealSquareWithCoordinates(x, y);

            if (nextSquare == currentSquare.RealBoard.RealGhostSquare) break;

            if (!PieceStandsOnSquare(nextSquare)) continue;
            if (PieceOnSquareHasOppositeColor(nextSquare)) _attackTurns.Add(nextSquare);
            else break;
        }
    }

    public override List<IRealSquare> GetRealMoves(IRealSquare realSquare)
    {
        _attackTurns.Clear();
        var supposedMoves = new List<IRealSquare>();
        
        var directions = new List<Vector2Int>() { new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, -1), new Vector2Int(-1, 1) };

        foreach (var direction in directions)
            AddPossibleTurnsInDiagonal(supposedMoves, realSquare, direction);
        
        return supposedMoves;
    }

    private void AddPossibleTurnsInDiagonal(List<IRealSquare> supposedMoves, IRealSquare currentSquare, Vector2Int moveDirection)
    {
        for (int i = 1; i < currentSquare.Board.Size.x; i++)
        {
            var square = currentSquare.RealBoard.GetRealSquareWithCoordinates(currentSquare.Coordinates.x + i * moveDirection.x, currentSquare.Coordinates.y + i * moveDirection.y);

            if (square == currentSquare.RealBoard.RealGhostSquare)
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

            supposedMoves.Add(square);
        }
    }
    
    public override List<ISquare> GetMoves(ISquare squareWithPiece)
    {

    }

    public override List<ISquare> GetAttacks(ISquare square)
    {
        
    }

    protected override void ResetAttackTurns()
    {
        _attackTurns.Clear();
    }
}