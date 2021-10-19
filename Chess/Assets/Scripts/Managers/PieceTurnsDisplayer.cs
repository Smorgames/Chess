using UnityEngine;

public class PieceTurnsDisplayer : MonoBehaviour
{
    public PiecePossibleTurns PieceTurns { get => _pieceTurns; }
    private PiecePossibleTurns _pieceTurns = new PiecePossibleTurns();

    private void ShowTurnsOfPieceStandsOnSquare(Square square)
    {
        _pieceTurns.Piece = square.PieceOnSquare;

        _pieceTurns.MoveTurns = _pieceTurns.Piece.GetPossibleMoveTurns(square);
        _pieceTurns.AttackTurns = _pieceTurns.Piece.GetPossibleAttackTurns(square);

        SingletonRegistry.Instance.Board.DeactivateAllSquares();

        SingletonRegistry.Instance.Board.ActivateListOfSquares(_pieceTurns.MoveTurns);
        SingletonRegistry.Instance.Board.ActivateListOfSquares(_pieceTurns.AttackTurns);
    }

    public void HideTurnsOfPiece()
    {
        SingletonRegistry.Instance.Board.DeactivateAllSquares();
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