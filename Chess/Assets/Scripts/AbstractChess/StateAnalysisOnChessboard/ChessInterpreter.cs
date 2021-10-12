namespace AbstractChess
{
    public class ChessInterpreter
    {
        private ChessStateParser _parser;
        private ChessboardArranger _arranger;

        public ChessInterpreter()
        {
            _parser = new ChessStateParser();
            _arranger = new ChessboardArranger();
        }

        public void ArrangePiecesOnBoardBasedOnToken(string chessStateToken, Chessboard board)
        {
            var pieceTokens = _parser.Parse(chessStateToken);
            _arranger.ArrangeChessPiecesOnBoard(pieceTokens, board);
        }
    }
}