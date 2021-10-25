using System.Collections.Generic;
using UnityEngine;

public class PieceTurnsDisplayer : MonoBehaviour
{
    public PiecesPossibleMoves PieceTurns { get => _pieceMoves; }
    private PiecesPossibleMoves _pieceMoves = new PiecesPossibleMoves();

    private void ShowTurnsOfPieceStandsOnSquare(IRealSquare square)
    {
        var moves = square.PieceOnIt.GetMoves(square);
        var attacks = square.PieceOnIt.GetAttacks(square);

        ActivateListOfSquares(moves);
        ActivateListOfSquares(attacks);
    }

    private void ActivateListOfSquares(List<IRealSquare> moves)
    {
        foreach (var move in moves)
            move.DisplayComponent.ActivateHighlight();
    }

    public void HideTurnsOfPiece(IRealSquare square)
    {
        square.Board.DeactivateAllSquares();
    }

    private void DeactivateListOfSquares(List<IRealSquare> moves)
    {
        foreach (var move in moves)
            move.DisplayComponent.DeactivateHighlight();
    }

    #region Events

    private void Start()
    {
        Square.OnSquareWithPieceClicked += ShowTurnsOfPieceStandsOnSquare;
    }

    private void OnDestroy()
    {
        Square.OnSquareWithPieceClicked -= ShowTurnsOfPieceStandsOnSquare;
    }

    #endregion
}

public class PiecesPossibleMoves
{
    public IRealSquare Piece { get => _piece; set => _piece = value; }
    private IRealSquare _piece;
    public List<IRealSquare> MoveTurns { get => _moveTurns; set => _moveTurns = value; }
    private List<IRealSquare> _moveTurns = new List<IRealSquare>();
    public List<IRealSquare> AttackTurns { get => _attackTurns; set => _attackTurns = value; }
    private List<IRealSquare> _attackTurns = new List<IRealSquare>();

    private List<IRealSquare> _possibleTurns;

    public List<IRealSquare> GetAllPossibleTurns()
    {
        var turns = new List<IRealSquare>();

        turns.AddRange(_moveTurns);
        turns.AddRange(_attackTurns);

        return turns;
    }
}