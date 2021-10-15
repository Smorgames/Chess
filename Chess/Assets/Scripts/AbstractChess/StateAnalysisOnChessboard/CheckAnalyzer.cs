using System.Collections.Generic;

using AbstractChessboard = AbstractChess.Chessboard;

namespace AbstractChess
{
    public class CheckAnalyzer
    {
        public bool IsCheckForAbsKing(AbstractChessboard abstractChessboard, PieceColor kingColor)
        {
            var allAttackTurns = new List<Square>();
            
            foreach (var square in abstractChessboard.Squares)
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
    }
}