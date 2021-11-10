using System.Collections.Generic;
using UnityEngine;

public class PawnPromotion : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    [SerializeField] private List<PieceReplacerButton> _replacerButtons;

    public void Activate(Pawn pawn)
    {
        Game.Instance.CurrentState = Game.GameState.Paused;
        _panel.SetActive(true);
        SetButtonSprites(pawn.ColorCode);
        var square = Game.Instance.GameBoard.SquareWithPiece(pawn);
        SetSquareForButtons(square);
    }

    private void SetButtonSprites(string colorCode)
    {
        foreach (var button in _replacerButtons) button.SetSprite(colorCode);
    }

    private void SetSquareForButtons(Square square)
    {
        foreach (var button in _replacerButtons) button.SetSquareWithPieceNeedReplace(square);
    }
    
    public void Deactivate() => _panel.SetActive(false);
}