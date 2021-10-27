using System.Collections.Generic;
using UnityEngine;

public class Rook : LinearlyMovingRealPiece
{
    public override string TypeCode => "r";

    public override List<ISquare> GetAttacks(ISquare square)
    {
        FillAttacksAndMoves(square);
        return Attacks;
    }
    
    public override List<ISquare> GetMoves(ISquare square)
    {
        FillAttacksAndMoves(square);
        return Moves;
    }

    private void FillAttacksAndMoves(ISquare square)
    {
        var directions = new List<Vector2Int>() { UpDirection, DownDirection, LeftDirection, RightDirection };
        FillAttacksAndMovesSquares(square, directions);
    }

    protected override void ResetAttackTurns()
    {
        ClearAttacksAndMoves();
    }
}