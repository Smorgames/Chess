using System.Collections.Generic;
using UnityEngine;

public class Queen : LinearlyMovingRealPiece
{
    public override string TypeCode => "Q";

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
        var directions = new List<Vector2Int>() 
            { UpDirection, DownDirection, LeftDirection, RightDirection, UpRightDirection, UpLeftDirection, DownRightDirection, DownLeftDirection };
        FillAttacksAndMovesSquares(square, directions);
    }

    protected override void ResetAttackTurns()
    {
        ClearAttacksAndMoves();
    }
}