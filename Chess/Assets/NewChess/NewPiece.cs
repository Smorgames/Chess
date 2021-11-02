using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewPiece : MonoBehaviour
{
    public static EventHandler<PieceMovedEventArgs> OnPieceMoved;
    
    public abstract string TypeCode { get; }
    public string ColorCode { get; private set; }
    public bool IsFirstMove { get; private set; }
    public List<NewSquare> SupposedMoves { get; protected set; } = new List<NewSquare>();

    public static readonly Vector3 Offset = new Vector3(0f, 0f, 1f);

    [SerializeField] private Sprite _whiteSprite;
    [SerializeField] private Sprite _blackSprite;

    public void Init(string colorCode, bool isFirstMove)
    {
        if (colorCode == "w" || colorCode == "b")
            ColorCode = colorCode;
        IsFirstMove = isFirstMove;
        GetComponent<SpriteRenderer>().sprite = ColorCode == "w" ? _whiteSprite : _blackSprite;
    }

    public abstract void UpdateSupposedMoves(NewSquare squareWithPiece);

    public void MoveTo(NewSquare square)
    {
        transform.position = square.transform.position + Offset;
        if (IsFirstMove) IsFirstMove = false;
        OnPieceMoved?.Invoke(this, new PieceMovedEventArgs());
    }
    
    public void Death()
    {
        Destroy(gameObject);
    }
}

public class PieceMovedEventArgs : EventArgs
{
    
}

[Serializable]
public class Signature
{
    public NewPieceColor MyColor;
    public NewPieceType MyType;
    public bool IsFirstMove;
}

[Serializable]
public enum NewPieceColor { White, Black }
[Serializable]
public enum NewPieceType { Pawn, Rook, Knight, Bishop, Queen, King }