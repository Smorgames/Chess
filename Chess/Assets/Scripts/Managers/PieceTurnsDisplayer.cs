using System.Collections.Generic;
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

public class PiecePossibleTurns
{
    public Piece Piece { get => _piece; set => _piece = value; }
    private Piece _piece;
    public List<Square> MoveTurns { get => _moveTurns; set => _moveTurns = value; }
    private List<Square> _moveTurns = new List<Square>();
    public List<Square> AttackTurns { get => _attackTurns; set => _attackTurns = value; }
    private List<Square> _attackTurns = new List<Square>();

    private List<Square> _possibleTurns;

    public List<Square> GetAllPossibleTurns()
    {
        List<Square> turns = new List<Square>();

        turns.AddRange(_moveTurns);
        turns.AddRange(_attackTurns);

        return turns;
    }
}