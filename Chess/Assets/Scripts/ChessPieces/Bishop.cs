using System.Collections.Generic;
using UnityEngine;

public class Bishop : UniversallyMovingPiece
{
    public override string TypeCode => "b";

    public override void UpdateSupposedMoves(Square squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _upRight, _upLeft, _downRight, _downLeft };
        SupposedMoves = IterativelyAddedSquares(squareWithPiece, directions);
    }
}