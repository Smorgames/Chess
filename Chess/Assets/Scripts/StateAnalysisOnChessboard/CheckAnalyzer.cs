using System.Collections.Generic;
using AbstractChess;

namespace AnalysisOfChessState.Analyzer
{
    public class CheckAnalyzer
    {
        public bool IsCheckForAbsKing(AbsChessboard abstractAbsChessboard, PieceColor kingColor)
        {
            var allAttackTurns = new List<AbsSquare>();
            
            foreach (var square in abstractAbsChessboard.Squares)
            {
                var pieceOnSquare = square.AbsPieceOnThisSquare;

                if (pieceOnSquare != null && pieceOnSquare.MyColor != kingColor)
                {
                    var attackTurns = pieceOnSquare.PossibleAttackMoves(square);

                    if (attackTurns != null)
                        foreach (var turn in attackTurns)
                            if (!allAttackTurns.Contains(turn))
                                allAttackTurns.Add(turn);
                }
            }

            foreach (var turn in allAttackTurns)
                if (turn.AbsPieceOnThisSquare is AbsKing)
                    return true;
            
            return false;
        }
        
        public bool IsCheckForAbsKing(Chessboard board, PieceColor kingColor)
        {
            var allAttackTurns = new List<Square>();
            
            foreach (var square in board.Squares)
            {
                var pieceOnSquare = square.PieceOnSquare;

                if (pieceOnSquare != null && pieceOnSquare.ColorData.Color != kingColor)
                {
                    var attackTurns = pieceOnSquare.GetPossibleAttackTurns(square);

                    if (attackTurns != null)
                        foreach (var turn in attackTurns)
                            if (!allAttackTurns.Contains(turn))
                                allAttackTurns.Add(turn);
                }
            }

            foreach (var turn in allAttackTurns)
                if (turn.PieceOnSquare is King)
                    return true;
            
            return false;
        }
    }
}