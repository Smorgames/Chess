using System.Linq;
using UnityEngine;

public class PieceShifter : MonoBehaviour
{
    [SerializeField] private PieceTurnsDisplayer _pieceTurnDisplayer;

    private NewSquare _fromSquare;
    private NewSquare _toSquare;

    private void SelectSquareFromGo(object s, SquareWithPieceClickedArgs a)
    {
        _fromSquare = a.Square;
    }

    private void SelectSquareToGo(object s, EmptySquareClickedArgs a)
    {
        if (PieceCanGoToSquare(a.Square))
            _toSquare = a.Square;
        else
        {
            _pieceTurnDisplayer.DeactivateAllSquaresHighlights(a.Square.Board);
            ResetSquareFields();
        }

        if (_fromSquare != null)
        {
            MoveChessPiece();
            UpdateStateOnChessboard(a.Square);
        }    
    }

    private bool PieceCanGoToSquare(NewSquare realSquare)
    {
        var possibleTurns = _pieceTurnDisplayer.PieceTurns;

        return possibleTurns.Any(t => realSquare == t);
    }

    private void ResetSquareFields()
    {
        _fromSquare = _toSquare = null;
    }

    private void MoveChessPiece()
    {
        var piece = _fromSquare.PieceOnIt;
        _fromSquare.PieceOnIt = null;
        
        var enemyPiece = _toSquare.PieceOnIt;
        if (enemyPiece != null) enemyPiece.Death();
        _toSquare.PieceOnIt = piece;

        piece.MoveTo(_toSquare);
    }

    private void UpdateStateOnChessboard(NewSquare square)
    {
        _pieceTurnDisplayer.DeactivateAllSquaresHighlights(square.Board);
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
        NewSquare.OnSquareWithPieceClicked += SelectSquareFromGo;
        NewSquare.OnEmptySquareClicked += SelectSquareToGo;
    }

    private void UnsubscrubeOnEvents()
    {
        NewSquare.OnSquareWithPieceClicked -= SelectSquareFromGo;
        NewSquare.OnEmptySquareClicked -= SelectSquareToGo;
    }
    #endregion
}