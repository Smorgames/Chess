using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceTurnsDisplayer : MonoBehaviour
{
    public PiecesPossibleMoves PieceTurns { get; } = new PiecesPossibleMoves();

    private void ShowTurnsOfPieceStandsOnSquare(RealSquare realSquare)
    {
        var moves = realSquare.RealPieceOnIt.GetMoves(realSquare);
        var realMoves = moves.Select(move => realSquare.RealBoard.RealSquareWithCoordinates(move.Coordinates)).ToList();

        var attacks = realSquare.RealPieceOnIt.GetAttacks(realSquare);
        var realAttacks = attacks.Select(attack => realSquare.RealBoard.RealSquareWithCoordinates(attack.Coordinates)).ToList();

        PieceTurns.AttackTurns.AddRange(realAttacks);
        PieceTurns.AttackTurns.AddRange(realMoves);
        
        DeactivateAllSquaresHighlights(realSquare);
        ActivateListOfSquares(realMoves, ActionType.Movement);
        ActivateListOfSquares(realAttacks, ActionType.Attack);
    }

    private void ActivateListOfSquares(List<RealSquare> squares, ActionType actionType)
    {
        foreach (var square in squares)
            square.SetEnabledHighlight(true, actionType);
    }

    public void DeactivateAllSquaresHighlights(RealSquare realSquare)
    {
        var board = realSquare.RealBoard;
        
        for (int x = 0; x < board.Size.x; x++)
        for (int y = 0; y < board.Size.y; y++)
            board.RealSquares[x, y].DeactivateAllHighlights();
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