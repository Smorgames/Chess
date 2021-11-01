using System.Collections.Generic;

public class NewAnalyzer
{
    private static bool CheckForKing(NewBoard board, NewPieceColor kingColor)
    {
        var allEnemiesMoves = new List<NewSquare>();
        
        for (var x = 0; x < board.Size.x; x++)
        for (var y = 0; y < board.Size.y; y++)
        {
            var enemy = board.Squares[x, y].PieceOnIt;

            if (enemy == null || enemy.MySignature.MyColor == kingColor) continue;

            var enemyMoves = enemy.SupposedMoves;

            foreach (var move in enemyMoves)
                allEnemiesMoves.Add(move);
        }

        foreach (var move in allEnemiesMoves)
        {
            var attackedPiece = move.PieceOnIt;
            if (attackedPiece == null) continue;

            if (attackedPiece is NewKing) return true;
        }

        return false;
    }

    public static List<NewSquare> MovesWithoutCheckForKing(NewSquare square, List<NewSquare> supposedMoves)
    {
        var kingColor = square.PieceOnIt.MySignature.MyColor;
        var board = square.Board;

        var correctMoves = new List<NewSquare>();
        
        foreach (var move in supposedMoves)
        {
            board.PlacePieceFromSquareToOther(square, move);
            var check = CheckForKing(board, kingColor);
            
            if (!check) correctMoves.Add(move);
            board.UNDO_PlacePieceFromSquareToOther();
        }

        return correctMoves;
    }

    public static bool MateForKing(NewBoard board, NewPieceColor kingColor)
    {
        var possibleMoves = new List<NewSquare>();
        
        for (var x = 0; x < board.Size.x; x++)
        for (var y = 0; y < board.Size.y; y++)
        {
            var piece = board.Squares[x, y].PieceOnIt;

            if (piece == null || piece.MySignature.MyColor != kingColor) continue;
            
            var piecesMoves = piece.SupposedMoves;
            if (piecesMoves.Count > 0) return false;
        }

        return true;
    }
}