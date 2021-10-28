using UnityEngine;

public class ChessCode
{
    public readonly string PiecesState;
    public readonly string BoardSize;
    public readonly string WhoseTurn;

    public ChessCode(string piecesState, string boardSize, string whoseTurn)
    {
        PiecesState = piecesState;
        BoardSize = boardSize;
        WhoseTurn = whoseTurn;
    }
    
    public ChessCode(string piecesState, Vector2Int boardSize, string whoseTurn)
    {
        PiecesState = piecesState;

        var x = boardSize.x.ToString();
        var y = boardSize.y.ToString();
        BoardSize = $"{x};{y}";
        
        WhoseTurn = whoseTurn;
    }
}