using System.Collections.Generic;
using AbstractChess;

namespace AnalysisOfChessState.Analyzer
{
    public class CheckAnalyzer
    {
        public bool CheckForAbstractKing(AbsChessboard abstractAbsChessboard, PieceColor kingColor)
        {
            var allAttackTurns = new List<AbsSquare>();
            
            foreach (var square in abstractAbsChessboard.Squares)
            {
                var pieceOnSquare = square.AbsPieceOnIt;

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
                if (turn.AbsPieceOnIt is AbsKing)
                    return true;
            
            return false;
        }
    }
}