using UnityEngine;

public class PieceTurnsDisplayer : MonoBehaviour
{
    [SerializeField] private SquareHandler _squareHandler;

    public PiecePossibleTurns PieceTurns { get => _pieceTurns; }
    private PiecePossibleTurns _pieceTurns = new PiecePossibleTurns();

    public void ShowTurnsOfPieceStandsOnSquare(Square square)
    {
        _pieceTurns.Piece = square.PieceOnSquare;

        _pieceTurns.MoveTurns = _pieceTurns.Piece.GetPossibleMoveTurns(square);
        _pieceTurns.AttackTurns = _pieceTurns.Piece.GetPossibleAttackTurns(square);

        _squareHandler.DeactivateAllSquares();

        _squareHandler.ActivateListOfSquares(_pieceTurns.MoveTurns);
        _squareHandler.ActivateListOfSquares(_pieceTurns.AttackTurns);
    }

    public void HideTurnsOfPiece()
    {
        _squareHandler.DeactivateAllSquares();
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