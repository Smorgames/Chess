using System.Collections.Generic;
using UnityEngine;

public class NewBishop : UniversallyMovingPieces
{
    public override string TypeCode => "b";

    public override void UpdateSupposedMoves(NewSquare squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _upRight, _upLeft, _downRight, _downLeft };
        SupposedMoves = IterativelyAddedSquares(squareWithPiece, directions);
    }
}