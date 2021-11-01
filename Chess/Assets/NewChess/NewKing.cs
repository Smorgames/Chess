using System.Collections.Generic;
using UnityEngine;

public class NewKing : UniversallyMovingPieces
{
    public override void UpdateSupposedMoves(NewSquare squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _up, _down, _left, _right, _upRight, _upLeft, _downRight, _downLeft };
        SupposedMoves = OneDirectionOneMove(squareWithPiece, directions);
    }
}