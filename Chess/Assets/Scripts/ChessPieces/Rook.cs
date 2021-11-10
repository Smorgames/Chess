using System.Collections.Generic;
using UnityEngine;

public class Rook : UniversallyMovingPiece
{
    public override string TypeCode => "r";

    public override void UpdateSupposedMoves(Square squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _up, _down, _left, _right };
        SupposedMoves = IterativelyAddedSquares(squareWithPiece, directions, true);
    }
    
    protected override void SetSprite()
    {
        var rookSprites = ReferenceRegistry.Instance.MySpritesStorage.RookSprites;
        _renderer.sprite = ColorCode == "w" ? rookSprites.White : rookSprites.Black;
    }
}