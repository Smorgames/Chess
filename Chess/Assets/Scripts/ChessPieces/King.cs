using System.Collections.Generic;
using UnityEngine;

public class King : UniversallyMovingPiece
{
    public override string TypeCode => "k";

    public Square CastleMove { get; set; }
    
    public override void UpdateSupposedMoves(Square squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _up, _down, _left, _right, _upRight, _upLeft, _downRight, _downLeft };
        SupposedMoves = OneDirectionOneMove(squareWithPiece, directions);
        if (CastleMove != null) SupposedMoves.Add(CastleMove);
    }
}