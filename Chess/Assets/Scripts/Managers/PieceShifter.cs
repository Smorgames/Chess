using UnityEngine;

public class PieceShifter : MonoBehaviour
{
    [SerializeField] private PieceTurnsDisplayer _pieceTurnDisplayer;

    private RealSquare _fromSquare;
    private RealSquare _toSquare;

    private void SelectSquareFromGo(RealSquare realSquare)
    {
        _fromSquare = realSquare;
    }

    private void SelectSquareToGo(RealSquare square)
    {
        if (PieceCanGoToSquare(square))
            _toSquare = square;
        else
        {
            _pieceTurnDisplayer.DeactivateAllSquaresHighlights(square);
            ResetSquareFields();
        }

        if (_fromSquare != null)
        {
            MoveChessPiece();
            UpdateStateOnChessboard(square);
        }    
    }

    private bool PieceCanGoToSquare(RealSquare realSquare)
    {
        var possibleTurns = _pieceTurnDisplayer.PieceTurns.GetAllPossibleTurns();

        for (int i = 0; i < possibleTurns.Count; i++)
            if (realSquare == possibleTurns[i])
                return true;

        return false;
    }

    private void ResetSquareFields()
    {
        _fromSquare = _toSquare = null;
    }

    private void MoveChessPiece()
    {
        var piece = _fromSquare.RealPieceOnIt;

        _fromSquare.RealPieceOnIt = null;
        _toSquare.RealPieceOnIt = piece;

        piece.Move(_toSquare);
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
        RealSquare.OnSquareWithPieceClicked += SelectSquareFromGo;
        RealSquare.OnEmptySquareClicked += SelectSquareToGo;
    }

    private void UnsubscrubeOnEvents()
    {
        RealSquare.OnSquareWithPieceClicked -= SelectSquareFromGo;
        RealSquare.OnEmptySquareClicked -= SelectSquareToGo;
    }
    #endregion
}