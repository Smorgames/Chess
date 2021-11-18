using System.Collections.Generic;
using UnityEngine;

public class Rook : UniversallyMovingPiece
{
    public override string TypeCode => "r";

    private static Vector3 _effectOffset = new Vector3(0f, 0.035f, 0f);

    public override void UpdateSupposedMoves(Square squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _up, _down, _left, _right };
        SupposedMoves = IterativelyAddedSquares(squareWithPiece, directions, true);
    }
    
    protected override void SetGraphics()
    {
        var isWhite = ColorCode == "w";
        var rookGrapgicData = ReferenceRegistry.Instance.MySpritesStorage.RookGraphicData;
        _renderer.sprite = isWhite ? rookGrapgicData.WhiteSprite : rookGrapgicData.BlackSprite;
        var effect = isWhite ? rookGrapgicData.WhiteEffects : rookGrapgicData.BlackEffects;
        InitializeVisualEffect(effect);
    }
}