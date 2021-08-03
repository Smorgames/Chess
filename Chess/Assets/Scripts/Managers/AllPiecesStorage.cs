using System.Collections.Generic;
using UnityEngine;

public class AllPiecesStorage : MonoBehaviour
{
    public List<Piece> AllChessPieces { get => _allChessPieces; }
    private List<Piece> _allChessPieces = new List<Piece>();

    public void AddPieceInArrayOfAllPieces(Piece chessPiece)
    {
        if (ArrayDoesNotContain(chessPiece))
            _allChessPieces.Add(chessPiece);
    }

    private bool ArrayDoesNotContain(Piece chessPiece)
    {
        for (int i = 0; i < _allChessPieces.Count; i++)
            if (chessPiece == _allChessPieces[i])
                return false;

        return true;
    }
}