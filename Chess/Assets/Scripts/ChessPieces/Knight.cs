using System.Collections.Generic;
using UnityEngine;

public class Knight : UniversallyMovingPiece
{
    public override string TypeCode => "n";

    public override void UpdateSupposedMoves(Square squareWithPiece)
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
    
    protected override void SetGraphics()
    {
        var knightGraphicData = ReferenceRegistry.Instance.MySpritesStorage.KnightGraphicData;
        _renderer.sprite = ColorCode == "w" ? knightGraphicData.WhiteSprite : knightGraphicData.BlackSprite;
        var effect = ColorCode == "w" ? knightGraphicData.WhiteEffects : knightGraphicData.BlackEffects;
        InitializeVisualEffect(effect);
    }
}