using UnityEngine;

public class PieceShifter : MonoBehaviour
{
    [SerializeField] private PieceTurnsDisplayer _pieceTurnDisplayer;

    private RealSquare _realSquareWithPiece;
    private RealSquare _realSquareWherePieceWillGo;

    private void SelectSquareWithPiece(RealSquare realSquare)
    {
        _realSquareWithPiece = realSquare;
    }

    private void SelectSquareWherePieceWillGo(RealSquare realSquare)
    {
        if (ThisSquareIsPossiblePieceTurn(realSquare))
            _realSquareWherePieceWillGo = realSquare;
        else
        {
            _pieceTurnDisplayer.DeactivateAllSquaresHighlights(realSquare);
            ResetSquareFields();
        }

        if (_realSquareWithPiece != null)
        {
            MoveChessPiece();
            UpdateStateOnChessboard(realSquare);
        }    
    }

    private bool ThisSquareIsPossiblePieceTurn(RealSquare realSquare)
    {
        var possibleTurns = _pieceTurnDisplayer.PieceTurns.GetAllPossibleTurns();

        for (int i = 0; i < possibleTurns.Count; i++)
            if (realSquare == possibleTurns[i])
                return true;

        return false;
    }

    private void ResetSquareFields()
    {
        _realSquareWithPiece = _realSquareWherePieceWillGo = null;
    }

    private void MoveChessPiece()
    {
        var piece = _realSquareWithPiece.RealRealPieceOnIt;

        _realSquareWithPiece.RealRealPieceOnIt = null;
        _realSquareWherePieceWillGo.RealRealPieceOnIt = piece;

        piece.Move(_realSquareWherePieceWillGo);
    }

    private void UpdateStateOnChessboard(RealSquare realSquare)
    {
        _pieceTurnDisplayer.DeactivateAllSquaresHighlights(realSquare);
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
        RealSquare.OnSquareWithPieceClicked += SelectSquareWithPiece;
        RealSquare.OnEmptySquareClicked += SelectSquareWherePieceWillGo;
    }

    private void UnsubscrubeOnEvents()
    {
        RealSquare.OnSquareWithPieceClicked -= SelectSquareWithPiece;
        RealSquare.OnEmptySquareClicked -= SelectSquareWherePieceWillGo;
    }
    #endregion
}