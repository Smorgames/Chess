using UnityEngine;

public class PieceTurnsDisplayer : MonoBehaviour
{
    public PiecePossibleTurns PieceTurns { get => _pieceTurns; }
    private PiecePossibleTurns _pieceTurns = new PiecePossibleTurns();

    private void ShowTurnsOfPieceStandsOnSquare(Square square)
    {
        _pieceTurns.Piece = square.PieceOnIt;

        _pieceTurns.MoveTurns = _pieceTurns.Piece.GetPossibleMoveTurns(square);
        _pieceTurns.AttackTurns = _pieceTurns.Piece.GetPossibleAttackTurns(square);

        square.Board.DeactivateAllSquares();

        square.Board.ActivateListOfSquares(_pieceTurns.MoveTurns);
        square.Board.ActivateListOfSquares(_pieceTurns.AttackTurns);
    }

    public void HideTurnsOfPiece(Square square)
    {
        square.Board.DeactivateAllSquares();
    }
    private void Start()
    {
        Square.OnSquareWithPieceClicked += ShowTurnsOfPieceStandsOnSquare;
    }

    private void OnDestroy()
    {
        Square.OnSquareWithPieceClicked -= ShowTurnsOfPieceStandsOnSquare;
    }
}