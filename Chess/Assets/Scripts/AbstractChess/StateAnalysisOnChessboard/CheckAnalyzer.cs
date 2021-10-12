using System.Collections.Generic;

namespace AbstractChess
{
    public class CheckAnalyzer
    {
        private readonly ChessStateParser _parser;
        private readonly ChessboardArranger _arranger;

        public CheckAnalyzer(ChessStateParser _chessStateParser, ChessboardArranger _chessboardArranger)
        {
            _parser = _chessStateParser;
            _arranger = _chessboardArranger;
        }
        
        public bool IsCheckForKing(string chessState, Chessboard chessboard, Piece.Color kingColor)
        {
            var chessTokens = _parser.Parse(chessState);
            _arranger.ArrangeChessPiecesOnBoard(chessTokens, chessboard);
            return AnalyzeIsCheckForKing(kingColor, chessboard);
        }

        private bool AnalyzeIsCheckForKing(Piece.Color kingColor, Chessboard chessboard)
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