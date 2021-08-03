using UnityEngine;
using System.Collections.Generic;

public class PieceMover : MonoBehaviour
{
    [SerializeField] private PieceTurnsDisplayer _pieceTurnDisplayer;

    private Square _squareWithPiece;
    private Square _squareWherePieceWillGo;

    private void MoveChessPiece()
    {
        //Piece selectedPiece = GameManager.Instance.GetSelectedChessPiece();
        //Square oldCell = Chessboard.Instance.GetCellOnWhichPieceStands(selectedPiece);

        //oldCell.PieceOnThis = null;
        //cell.PieceOnThis = selectedPiece;
        //selectedPiece.Move(cell);

        Piece piece = _squareWithPiece.PieceOnThis;
        piece.Move(_squareWherePieceWillGo);
    }

    private void SelectSquareWithPiece(Square square)
    {
        _squareWithPiece = square;
    }

    private void SelectSquareWhereWillGo(Square square)
    {
        if (SquareIsPieceTurn(square))
            _squareWherePieceWillGo = square;
        else
        {
            _pieceTurnDisplayer.HideTurnsOfPiece();
            ResetSquareFields();
        }

        if (_squareWithPiece != null)
            MoveChessPiece();
    }

    private bool SquareIsPieceTurn(Square square)
    {
        List<Square> possibleTurns = _pieceTurnDisplayer.PieceTurns.GetPossibleTurns();

        for (int i = 0; i < possibleTurns.Count; i++)
            if (square == possibleTurns[i])
                return true;

        return false;
    }

    private void ResetSquareFields()
    {
        _squareWithPiece = _squareWherePieceWillGo = null;
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
        Square.OnEmptySquareClicked += SelectSquareWhereWillGo;
    }

    private void UnsubscrubeOnEvents()
    {
        Square.OnSquareWithPieceClicked -= SelectSquareWithPiece;
        Square.OnEmptySquareClicked -= SelectSquareWhereWillGo;
    }
    #endregion
}