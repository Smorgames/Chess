using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewPiece : MonoBehaviour
{
    public static EventHandler<PieceMovedEventArgs> OnPieceMoved;
    
    public string PieceCode { get; private set; }
    public Signature MySignature;
    public List<NewSquare> SupposedMoves = new List<NewSquare>();

    private static readonly Vector3 Offset = new Vector3(0f, 0f, 1f);

    public void Init(NewPieceColor myColor, NewPieceType myType, bool isFirstMove)
    {
        MySignature.MyColor = myColor;
        MySignature.MyType = myType;
        MySignature.IsFirstMove = isFirstMove;
        //SetPieceCode();
    }

    private void SetPieceCode()
    {
        if (MySignature.MyType == NewPieceType.Pawn) PieceCode = MySignature.MyColor == NewPieceColor.White ? "P" : "p";
        if (MySignature.MyType == NewPieceType.Rook) PieceCode = MySignature.MyColor == NewPieceColor.White ? "R" : "r";
        if (MySignature.MyType == NewPieceType.Knight) PieceCode = MySignature.MyColor == NewPieceColor.White ? "N" : "n";
        if (MySignature.MyType == NewPieceType.Bishop) PieceCode = MySignature.MyColor == NewPieceColor.White ? "B" : "b";
        if (MySignature.MyType == NewPieceType.Queen) PieceCode = MySignature.MyColor == NewPieceColor.White ? "Q" : "q";
        if (MySignature.MyType == NewPieceType.King) PieceCode = MySignature.MyColor == NewPieceColor.White ? "K" : "k";
    }

    public abstract void UpdateSupposedMoves(NewSquare squareWithPiece);

    public void MoveTo(NewSquare square)
    {
        transform.position = square.transform.position + Offset;
        if (MySignature.IsFirstMove) MySignature.IsFirstMove = false;
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