using System.Collections.Generic;
using UnityEngine;

public class Bishop : LinearlyMovingRealPiece
{
    public override string TypeCode => "b";
    
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
        var directions = new List<Vector2Int>() { UpRightDirection, UpLeftDirection, DownRightDirection, DownLeftDirection };
        FillAttacksAndMovesSquares(square, directions);
    }

    protected override void ResetAttackTurns()
    {
        ClearAttacksAndMoves();
    }
}