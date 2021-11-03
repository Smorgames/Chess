using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceMovesHighlighter : MonoBehaviour
{
    public List<Square> PieceMoves { get; private set; } = new List<Square>();

    private void HighlightPieceMoves(object s, ActivePieceClickedArgs a)
    {
        var square = a.MySquare;
        var piece = square.PieceOnIt;
        var pieceMoves = CheckMateHandler.MovesWithoutCheckForKing(square, piece.SupposedMoves);
        
        DeactivateAllSquaresHighlights(square.Board);

        foreach (var move in pieceMoves.Where(move => move != null))
            move.SetEnabledHighlight(true, move.PieceOnIt != null ? ActionType.Attack : ActionType.Movement);

        PieceMoves = pieceMoves;
    }

    public void DeactivateAllSquaresHighlights(Board board)
    {
        for (var x = 0; x < board.Size.x; x++)
        for (var y = 0; y < board.Size.y; y++)
            board.Squares[x, y].DeactivateAllHighlights();
    }

    #region Events

    private void Start()
    {
        Square.OnPieceWhoseTurnNowClicked += HighlightPieceMoves;
    }

    private void OnDestroy()
    {
        Square.OnPieceWhoseTurnNowClicked -= HighlightPieceMoves;
    }

    #endregion
}