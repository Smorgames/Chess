using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceHighlighter : MonoBehaviour
{
    public List<Square> PieceMoves { get; private set; } = new List<Square>();

    private void HighlightPieceMoves(object s, ActivePieceClickedArgs a)
    {
        DeselectAllPieces();
        var square = a.MySquare;
        var piece = square.PieceOnIt;
        piece.Select();

        var pieceMoves = CheckMateAnalyser.MovesWithoutCheckForKing(square, piece.SupposedMoves);

        DeactivateAllSquaresHighlights(square.Board);

        foreach (var move in pieceMoves.Where(move => move != null))
            move.SetEnabledHighlight(true, move.PieceOnIt != null ? ActionType.Attack : ActionType.Movement);

        PieceMoves = pieceMoves;
    }

    private void DeselectAllPieces()
    {
        foreach (var player in Game.Instance.Players)
        foreach (var piece in player.MyPieces)
            if (piece.transform.localScale == Piece.SelectedSize) piece.Deselect();
    }
    public void DeactivateAllSquaresHighlights(Board board)
    {
        for (var x = 0; x < board.Size.x; x++)
        for (var y = 0; y < board.Size.y; y++)
            board.Squares[x, y].DeactivateAllHighlights();
    }
    
    private void EmptySquareClicked(object sender, EmptySquareClickedArgs e) => DeselectAllPieces();
    private void OnPieceWhoNotTurnNowClicked(object sender, InactivePieceClickedArgs e) => DeselectAllPieces();

    #region Events

    private void Start()
    {
        Square.OnPieceWhoseTurnNowClicked += HighlightPieceMoves;
        Square.OnEmptySquareClicked += EmptySquareClicked;
        Square.OnPieceWhoNotTurnNowClicked += OnPieceWhoNotTurnNowClicked;
    }
    
    private void OnDestroy()
    {
        Square.OnPieceWhoseTurnNowClicked -= HighlightPieceMoves;
        Square.OnEmptySquareClicked -= EmptySquareClicked;
        Square.OnPieceWhoNotTurnNowClicked -= OnPieceWhoNotTurnNowClicked;
    }

    #endregion
}