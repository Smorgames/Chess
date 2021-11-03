using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMateHandler
{
    public static List<Square> MovesWithoutCheckForKing(Square square, List<Square> supposedMoves)
    {
        var kingColor = square.PieceOnIt.ColorCode;
        var board = square.Board;

        var correctMoves = new List<Square>();
        
        foreach (var move in supposedMoves)
        {
            board.PlacePieceFromSquareToOther(square, move);
            var check = CheckForKing(board, kingColor);
            
            if (!check) correctMoves.Add(move);
            board.UNDO_PlacePieceFromSquareToOther();
        }

        return correctMoves;
    }
    private static bool CheckForKing(Board board, string kingColor)
    {
        var enemyColor = kingColor == "w" ? "b" : "w";
        UpdateSupposedMovesForIterativeMovingPiece(board, enemyColor);

        var allEnemiesMoves = new List<Square>();

        for (var x = 0; x < board.Size.x; x++)
        for (var y = 0; y < board.Size.y; y++)
        {
            var enemy = board.Squares[x, y].PieceOnIt;

            if (enemy == null || enemy.ColorCode == kingColor) continue;

            var enemyMoves = enemy.SupposedMoves;

            foreach (var move in enemyMoves)
                allEnemiesMoves.Add(move);
        }

        foreach (var move in allEnemiesMoves)
        {
            var attackedPiece = move.PieceOnIt;
            if (attackedPiece == null) continue;

            if (attackedPiece is King) return true;
        }

        return false;
    }
    private static void UpdateSupposedMovesForIterativeMovingPiece(Board board, string color)
    {
        for (var x = 0; x < board.Size.x; x++)
        for (var y = 0; y < board.Size.y; y++)
        {
            var piece = board.Squares[x, y].PieceOnIt;
            if (piece == null || piece.ColorCode != color) continue;
            piece.UpdateSupposedMoves(board.Squares[x, y]);
        }
    }
    
    public static bool MateForKing(Board board, string kingColor)
    {
        for (var x = 0; x < board.Size.x; x++)
        for (var y = 0; y < board.Size.y; y++)
        {
            var piece = board.Squares[x, y].PieceOnIt;
            if (piece == null || piece.ColorCode != kingColor) continue;
            
            var piecesMoves = MovesWithoutCheckForKing(board.Squares[x, y], piece.SupposedMoves);
            if (piecesMoves.Count > 0) return false;
        }

        return true;
    }
}