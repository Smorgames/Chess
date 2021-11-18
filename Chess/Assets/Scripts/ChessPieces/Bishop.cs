using System.Collections.Generic;
using UnityEngine;

public class Bishop : UniversallyMovingPiece
{
    public override string TypeCode => "b";

    private static Vector3 _effectOffset = new Vector3(0f, 0.065f, 0f);

    public override void UpdateSupposedMoves(Square squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _upRight, _upLeft, _downRight, _downLeft };
        SupposedMoves = IterativelyAddedSquares(squareWithPiece, directions);
    }
    
    protected override void SetGraphics()
    {
        var isWhite = ColorCode == "w";
        var bishopGraphicData = ReferenceRegistry.Instance.MySpritesStorage.BishopGraphicData;
        _renderer.sprite = isWhite ? bishopGraphicData.WhiteSprite : bishopGraphicData.BlackSprite;
        var effect = isWhite ? bishopGraphicData.WhiteEffects : bishopGraphicData.BlackEffects;
        InitializeVisualEffect(effect);
    }
}