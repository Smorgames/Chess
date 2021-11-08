using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public static EventHandler<PieceMovedEventArgs> OnPieceMoved;
    
    public abstract string TypeCode { get; }
    public string ColorCode { get; private set; }
    public bool IsFirstMove { get; protected set; }
    public List<Square> SupposedMoves { get; protected set; } = new List<Square>();

    public static readonly Vector3 Offset = new Vector3(0f, 0f, 1f);

    protected King _myKing;

    private static readonly float _deactivatedAlpha = 0.5f;
    private static readonly float _activatedAlpha = 1f;

    public static readonly Vector3 SelectedSize = new Vector3(1.15f, 1.15f, 1f);
    public static readonly Vector3 NormalSize = new Vector3(1f, 1f, 1f);

    public SpriteRenderer Renderer => _renderer;
    [SerializeField] private SpriteRenderer _renderer;
    
    [SerializeField] private Sprite _whiteSprite;
    [SerializeField] private Sprite _blackSprite;

    public void Init(string colorCode, bool isFirstMove)
    {
        if (colorCode == "w" || colorCode == "b")
            ColorCode = colorCode;
        IsFirstMove = isFirstMove;
        _renderer.sprite = ColorCode == "w" ? _whiteSprite : _blackSprite;
    }

    public abstract void UpdateSupposedMoves(Square squareWithPiece);

    public virtual void MoveTo(Square square)
    {
        ChangePosition(square);
        OnPieceMoved?.Invoke(this, new PieceMovedEventArgs());
    }

    public void ChangePosition(Square square)
    {
        transform.position = square.transform.position + Offset;
        if (IsFirstMove) IsFirstMove = false;
    }

    public void Select()
    {
        if (Math.Abs(_renderer.color.a - _deactivatedAlpha) > 0.001f)
            transform.localScale = SelectedSize; 
    } 
    public void Deselect() => transform.localScale = NormalSize;
    
    public void Activate()
    {
        var color = _renderer.color;
        color.a = _activatedAlpha;
        _renderer.color = color;
    }
    
    public void Deactivate()
    {
        var color = _renderer.color;
        color.a = _deactivatedAlpha;
        _renderer.color = color;
    }
    
    public void Death()
    {
        Destroy(gameObject);
    }
}

public class PieceMovedEventArgs : EventArgs
{
    
}