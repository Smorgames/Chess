using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceTurnsDisplayer : MonoBehaviour
{
    public List<NewSquare> PieceTurns { get; private set; } = new List<NewSquare>();

    private void ShowTurnsOfPieceStandsOnSquare(object s, SquareWithPieceClickedArgs a)
    {
        PieceTurns.Clear();
        var square = a.Square;
        var piece = square.PieceOnIt;
        var moves = NewAnalyzer.MovesWithoutCheckForKing(square, piece.SupposedMoves);
        
        DeactivateAllSquaresHighlights(square.Board);

        foreach (var move in moves.Where(move => move != null))
            move.SetEnabledHighlight(true, move.PieceOnIt != null ? ActionType.Attack : ActionType.Movement);

        PieceTurns = moves;
    }

    public void DeactivateAllSquaresHighlights(NewBoard board)
    {
        for (int x = 0; x < board.Size.x; x++)
        for (int y = 0; y < board.Size.y; y++)
            board.Squares[x, y].DeactivateAllHighlights();
    }

    #region Events

    private void Start()
    {
        NewSquare.OnSquareWithPieceClicked += ShowTurnsOfPieceStandsOnSquare;
    }

    private void OnDestroy()
    {
        NewSquare.OnSquareWithPieceClicked -= ShowTurnsOfPieceStandsOnSquare;
    }

    #endregion
}