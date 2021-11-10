using System.Collections.Generic;
using UnityEngine;

public class Queen : UniversallyMovingPiece
{
    public override string TypeCode => "q";

    public override void UpdateSupposedMoves(Square squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _up, _down, _left, _right, _upRight, _upLeft, _downRight, _downLeft };
        SupposedMoves = IterativelyAddedSquares(squareWithPiece, directions);
    }

    protected override void SetSprite()
    {
        var queenSprites = ReferenceRegistry.Instance.MySpritesStorage.QueenSprites;
        _renderer.sprite = ColorCode == "w" ? queenSprites.White : queenSprites.Black;
    }
}