using System.Collections.Generic;
using UnityEngine;

public class Queen : UniversallyMovingPiece
{
    public override string TypeCode => "q";

    private static Vector3 _effectOffset = new Vector3(0f, 0.055f, 0f);

    public override void UpdateSupposedMoves(Square squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _up, _down, _left, _right, _upRight, _upLeft, _downRight, _downLeft };
        SupposedMoves = IterativelyAddedSquares(squareWithPiece, directions);
    }

    protected override void SetGraphics()
    {
        var isWhite = ColorCode == "w";
        var queenGraphicData = ReferenceRegistry.Instance.MySpritesStorage.QueenGraphicData;
        _renderer.sprite = isWhite ? queenGraphicData.WhiteSprite : queenGraphicData.BlackSprite;
        var effect = isWhite ? queenGraphicData.WhiteEffects : queenGraphicData.BlackEffects;
        InitializeVisualEffect(effect);
    }
}