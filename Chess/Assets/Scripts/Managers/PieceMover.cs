using UnityEngine;
using System.Collections.Generic;

public class PieceMover : MonoBehaviour
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
            _pieceTurnDisplayer.HideTurnsOfPiece();
            ResetSquareFields();
        }

        if (_squareWithPiece != null)
        {
            MoveChessPiece();
            UpdateStateOnChessboard();
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
        Piece piece = _squareWithPiece.PieceOnThis;

        _squareWithPiece.PieceOnThis = null;
        _squareWherePieceWillGo.PieceOnThis = piece;

        piece.Move(_squareWherePieceWillGo);
    }

    private void UpdateStateOnChessboard()
    {
        _pieceTurnDisplayer.HideTurnsOfPiece();
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