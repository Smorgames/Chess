using System.Collections.Generic;

using AbstractBoard = AbstractChess.Chessboard;

namespace AbstractChess
{
    public class CheckAnalyzer
    {
        private readonly ChessStateParser _parser;
        private readonly ChessboardArranger _arranger;

        public CheckAnalyzer()
        {
            _parser = new ChessStateParser();
            _arranger = new ChessboardArranger();
        }
        
        public bool IsCheckForKing(AbstractBoard chessboard, Piece.Color kingColor)
        {
            var allAttackTurns = new List<Square>();
            
            foreach (var square in chessboard.Squares)
            {
                var pieceOnSquare = square.PieceOnThisSquare;

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
                if (turn.PieceOnThisSquare is King)
                    return true;
            
            return false;
        }
    }
}