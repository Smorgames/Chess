using AbstractChess;
using UnityEngine;
using Instruments;

using AbstractBoard = AbstractChess.Chessboard;
using ChessPiece = AbstractChess.Piece;

public class TestAbstractChess : MonoBehaviour
{
    private AbstractBoard _chessboard;
    
    private void Start()
    {
        ChessSystemTesting.Test1();
    }
}

public static class ChessSystemTesting
{
    public static void Test1()
    {
        var boardSize = new Vector2Int(5, 5);
        var chessboard = new AbstractBoard(boardSize);
        
        var chessStateToken = "10wb32bK"; // White bishop on square (1,0) and check black king (3, 2)
        var chessInterpreter = new ChessInterpreter();

        chessInterpreter.ArrangePiecesOnBoardBasedOnToken(chessStateToken, chessboard);
        
        var checkAnalyzer = new CheckAnalyzer();
        var isCheck = checkAnalyzer.IsCheckForKing(chessboard, ChessPiece.Color.Black);
        
        var checkText = isCheck ? "checks".Bold() : "didn't check".Bold();
        var testName = $"[{nameof(Test1)}]:".Bold().Color("Blue");
        
        Debug.Log($"{testName} White bishop {checkText} black king");
    }
}