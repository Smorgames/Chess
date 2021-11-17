﻿using System.Collections.Generic;

public class CheckMateAnalyser
{
    public static List<Square> MovesWithoutCheckForKing(Square squareWithPiece, List<Square> supposedPieceMoves)
    {
        var kingColor = squareWithPiece.PieceOnIt.ColorCode;
        var board = squareWithPiece.MyBoard;

        var enemyColor = kingColor == "w" ? "b" : "w";
        var enemyPlayer = Game.Instance.GetPlayerBasedOnColorCode(enemyColor);

        var correctMoves = new List<Square>();
        
        foreach (var supposedMove in supposedPieceMoves)
        {
            board.MyPiecePlacer.PlacePieceFromSquareToOther(squareWithPiece, supposedMove);
            enemyPlayer.UpdatePiecesSupposedMoves();
            
            var check = CheckForKing(kingColor);
            
            if (!check) correctMoves.Add(supposedMove);
            board.MyPiecePlacer.UNDO_PlacePieceFromSquareToOther();
            
            enemyPlayer.UpdatePiecesSupposedMoves();
        }

        return correctMoves;
    }
    public static bool CheckForKing(string kingColor)
    {
        var enemyColor = kingColor == "w" ? "b" : "w";
        var enemyPlayer = Game.Instance.GetPlayerBasedOnColorCode(enemyColor);

        var allEnemiesMoves = new List<Square>();

        foreach (var piece in enemyPlayer.MyPieces)
            if (piece.SupposedMoves.Count > 0)
                allEnemiesMoves.AddRange(piece.SupposedMoves);

        foreach (var move in allEnemiesMoves)
        {
            var attackedPiece = move.PieceOnIt;
            if (attackedPiece == null) continue;
            if (attackedPiece is King && attackedPiece.ColorCode != enemyColor) return true;
        }
        
        return false;
    }

    public static bool MateForKing(Board board, string kingColor)
    {
        var player = Game.Instance.GetPlayerBasedOnColorCode(kingColor);

        foreach (var piece in player.MyPieces)
        {
            var piecesMoves = MovesWithoutCheckForKing(board.SquareWithPiece(piece), piece.SupposedMoves);
            if (piecesMoves.Count > 0) return false;
        }

        return CheckForKing(kingColor);
    }
    
    public static void MateOrDrawForKing(string kingColor, out bool mate, out bool draw)
    {
        var player = Game.Instance.GetPlayerBasedOnColorCode(kingColor);
        var board = Game.Instance.GameBoard;

        foreach (var piece in player.MyPieces)
        {
            var piecesMoves = MovesWithoutCheckForKing(board.SquareWithPiece(piece), piece.SupposedMoves);
            if (piecesMoves.Count > 0)
            {
                mate = draw = false;
                return;
            }
        }

        mate = CheckForKing(kingColor);
        draw = !mate;
    }
}