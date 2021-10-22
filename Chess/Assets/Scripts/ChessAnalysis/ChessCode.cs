public class ChessCode
{
    public readonly string PiecesState;
    public readonly string BoardSize;
    public readonly string WhoseTurn;

    public ChessCode(string piecesState)
    {
        PiecesState = piecesState;
    }

    public ChessCode(string piecesState, string boardSize, string whoseTurn)
    {
        PiecesState = piecesState;
        BoardSize = boardSize;
        WhoseTurn = whoseTurn;
    }
}