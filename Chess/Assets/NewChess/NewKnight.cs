﻿using System.Collections.Generic;
using UnityEngine;

public class NewKnight : UniversallyMovingPieces
{
    public override void UpdateSupposedMoves(NewSquare squareWithPiece)
    {
        SupposedMoves.Clear();

        var directions = new List<Vector2Int>()
        {
            _upRight + _up, _upRight + _right,
            _upLeft + _up, _upLeft + _left,
            _downRight + _down, _downRight + _right,
            _downLeft + _down, _downLeft + _left
        };

        SupposedMoves = OneDirectionOneMove(squareWithPiece, directions);
    }
}