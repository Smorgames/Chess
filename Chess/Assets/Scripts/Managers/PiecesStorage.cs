using System.Collections.Generic;
using UnityEngine;

public class PiecesStorage : MonoBehaviour
{
    public List<Piece> AllPieces { get => _allPieces; }
    private List<Piece> _allPieces = new List<Piece>();

    public King GetKing(PieceColor pieceColor)
    {
        foreach (var piece in _allPieces)
            if (piece.GetType() == typeof(King) && piece.ColorData.Color == pieceColor)
                return (King)piece;

        return null;
    }

    public void AddPieceInArrayOfAllPieces(Piece chessPiece)
    {
        if (ArrayDoesNotContain(chessPiece))
            _allPieces.Add(chessPiece);
    }

    private bool ArrayDoesNotContain(Piece chessPiece)
    {
        for (int i = 0; i < _allPieces.Count; i++)
            if (chessPiece == _allPieces[i])
                return false;

        return true;
    }
}