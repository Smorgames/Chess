﻿using System.Collections.Generic;
using UnityEngine;

public class NewRook : UniversallyMovingPieces
{
    public override void UpdateSupposedMoves(NewSquare squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _up, _down, _left, _right };
        SupposedMoves = IterativelyAddedSquares(squareWithPiece, directions);
    }
}