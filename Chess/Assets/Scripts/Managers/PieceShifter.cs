using System.Linq;
using UnityEngine;

public class PieceShifter : MonoBehaviour
{
    [SerializeField] private PieceTurnsDisplayer _pieceTurnDisplayer;

    private NewSquare _fromSquare;
    private NewSquare _toSquare;

    private void SelectSquareFromGo(object s, ActivePieceClickedArgs a)
    {
        _fromSquare = a.Square;
    }

    private void ClickedOnEmptySquare(object s, EmptySquareClickedArgs a)
    {
        if (_fromSquare == null) return;

        if (PieceCanGoToSquare(a.Square))
            _toSquare = a.Square;

        if (_fromSquare != null && _toSquare != null)
            MoveChessPiece();
        
        ResetChessBoardState(a.Square);
    }
    private void ResetChessBoardState(NewSquare square)
    {
        _pieceTurnDisplayer.DeactivateAllSquaresHighlights(square.Board);
        ResetSquareFields();
    }
    private void ResetSquareFields() => _fromSquare = _toSquare = null;

    private void SelectSquareToGo(object s, InactivePieceClickedArgs a)
    {
        if (PieceCanGoToSquare(a.Square))
            _toSquare = a.Square;

        if (_fromSquare != null && _toSquare != null)
            MoveChessPiece();
          
        ResetChessBoardState(a.Square);
    }
    private bool PieceCanGoToSquare(NewSquare square) => _pieceTurnDisplayer.PieceTurns.Any(move => square == move);
    private void MoveChessPiece()
    {
        var piece = _fromSquare.PieceOnIt;
        _fromSquare.PieceOnIt = null;
        
        var enemyPiece = _toSquare.PieceOnIt;
        if (enemyPiece != null) enemyPiece.Death();
        _toSquare.PieceOnIt = piece;

        piece.MoveTo(_toSquare);
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
        NewSquare.OnActivePieceClicked += SelectSquareFromGo;
        NewSquare.OnInactivePieceClicked += SelectSquareToGo;
        NewSquare.OnEmptySquareClicked += ClickedOnEmptySquare;
    }

    private void UnsubscrubeOnEvents()
    {
        NewSquare.OnActivePieceClicked -= SelectSquareFromGo;
        NewSquare.OnInactivePieceClicked -= SelectSquareToGo;
        NewSquare.OnEmptySquareClicked -= ClickedOnEmptySquare;
    }
    #endregion
}