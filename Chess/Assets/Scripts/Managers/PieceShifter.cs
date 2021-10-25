﻿using UnityEngine;
using System.Collections.Generic;

public class PieceShifter : MonoBehaviour
{
    [SerializeField] private PieceTurnsDisplayer _pieceTurnDisplayer;

    private Square _squareWithPiece;
    private Square _squareWherePieceWillGo;

    private void SelectSquareWithPiece(Square square)
    {
        _squareWithPiece = square;
    }

    private void SelectSquareWherePieceWillGo(Square square)
    {
        if (ThisSquareIsPossiblePieceTurn(square))
            _squareWherePieceWillGo = square;
        else
        {
            _pieceTurnDisplayer.HideTurnsOfPiece(square);
            ResetSquareFields();
        }

        if (_squareWithPiece != null)
        {
            MoveChessPiece();
            UpdateStateOnChessboard(square);
        }    
    }

    private bool ThisSquareIsPossiblePieceTurn(Square square)
    {
        List<Square> possibleTurns = _pieceTurnDisplayer.PieceTurns.GetAllPossibleTurns();

        for (int i = 0; i < possibleTurns.Count; i++)
            if (square == possibleTurns[i])
                return true;

        return false;
    }

    private void ResetSquareFields()
    {
        _squareWithPiece = _squareWherePieceWillGo = null;
    }

    private void MoveChessPiece()
    {
        var piece = _squareWithPiece.PieceOnIt;

        _squareWithPiece.PieceOnIt = null;
        _squareWherePieceWillGo.PieceOnIt = piece;

        piece.Move(_squareWherePieceWillGo);
    }

    private void UpdateStateOnChessboard(Square square)
    {
        _pieceTurnDisplayer.HideTurnsOfPiece(square);
        ResetSquareFields();
    }

    #region Events
    private void Start()
    {
        SubscrubeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscrubeOnEvents();
    }

    private void SubscrubeOnEvents()
    {
        Square.OnSquareWithPieceClicked += SelectSquareWithPiece;
        Square.OnEmptySquareClicked += SelectSquareWherePieceWillGo;
    }

    private void UnsubscrubeOnEvents()
    {
        Square.OnSquareWithPieceClicked -= SelectSquareWithPiece;
        Square.OnEmptySquareClicked -= SelectSquareWherePieceWillGo;
    }
    #endregion
}