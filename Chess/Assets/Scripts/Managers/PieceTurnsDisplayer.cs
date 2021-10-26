using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceTurnsDisplayer : MonoBehaviour
{
    public PiecesPossibleMoves PieceTurns { get => _pieceMoves; }
    private PiecesPossibleMoves _pieceMoves = new PiecesPossibleMoves();

    private void ShowTurnsOfPieceStandsOnSquare(RealSquare realSquare)
    {
        var moves = realSquare.RealPieceOnIt.GetMoves(realSquare);
        var realMoves = moves.Select(move => realSquare.RealBoard.RealSquareWithCoordinates(move.Coordinates)).ToList();

        var attacks = realSquare.RealPieceOnIt.GetAttacks(realSquare);
        var realAttacks = attacks.Select(attack => realSquare.RealBoard.RealSquareWithCoordinates(attack.Coordinates)).ToList();

        ActivateListOfSquares(realMoves);
        ActivateListOfSquares(realAttacks);
    }

    private void ActivateListOfSquares(List<RealSquare> moves)
    {
        foreach (var move in moves)
            move.ActivateHighlight();
    }

    public void DeactivateAllSquaresHighlights(RealSquare realSquare)
    {
        for (int x = 0; x < realSquare.RealBoard.Size.x; x++)
        for (int y = 0; y < realSquare.RealBoard.Size.y; y++)
            realSquare.RealBoard.RealSquares[x, y].DeactivateHighlight();
    }

    private void DeactivateListOfSquares(List<RealSquare> realSquares)
    {
        foreach (var move in realSquares)
            move.DeactivateHighlight();
    }

    #region Events

    private void Start()
    {
        RealSquare.OnSquareWithPieceClicked += ShowTurnsOfPieceStandsOnSquare;
    }

    private void OnDestroy()
    {
        RealSquare.OnSquareWithPieceClicked -= ShowTurnsOfPieceStandsOnSquare;
    }

    #endregion
}

public class PiecesPossibleMoves
{
    public RealPiece RealPiece { get => _realPiece; set => _realPiece = value; }
    private RealPiece _realPiece;
    public List<RealSquare> MoveTurns { get => _moveTurns; set => _moveTurns = value; }
    private List<RealSquare> _moveTurns = new List<RealSquare>();
    public List<RealSquare> AttackTurns { get => _attackTurns; set => _attackTurns = value; }
    private List<RealSquare> _attackTurns = new List<RealSquare>();

    private List<RealSquare> _possibleTurns;

    public List<RealSquare> GetAllPossibleTurns()
    {
        var turns = new List<RealSquare>();

        turns.AddRange(_moveTurns);
        turns.AddRange(_attackTurns);

        return turns;
    }
}