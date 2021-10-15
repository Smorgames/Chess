using AbstractChess;
using UnityEngine;
using Instruments;

using AbstractBoard = AbstractChess.Chessboard;

public class TestAbstractChess : MonoBehaviour
{
    private AbstractBoard _chessboard;
    
    private void Start()
    {
        ChessSystemTesting.Test1();
        ChessSystemTesting.Test2();
    }
}

public static class ChessSystemTesting
{
    public static void Test1()
    {
        var boardSize = new Vector2Int(5, 5);
        var chessboard = new AbstractBoard(boardSize);
        
        var chessStateToken = "10wb32bK"; // White bishop on square (1,0) and check black king (3, 2)
        
        var parser = new ChessParser();
        var recreator = new ChessStateRecreator();
        var analyzer = new CheckAnalyzer();

        var tokens = parser.Parse(chessStateToken);
        recreator.RecreateChessState(chessboard, tokens);
        
        var isCheck = analyzer.IsCheckForAbsKing(chessboard, PieceColor.Black);
        
        var isPassed = isCheck ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
        var testName = $"[{nameof(Test1)}]:".Bold().Color("Blue");
        
        Debug.Log($"{testName} Check for black king {isPassed}");
    }
    
    public static void Test2()
    {
        var chessStateToken = "00br02wp03wK13wp23bQ";
        
        var parser = new ChessParser();
        var recreator = new ChessStateRecreator();
        var analyzer = new CheckAnalyzer();

        var tokens = parser.Parse(chessStateToken);
        var size = new Vector2Int(3, 4);
        var chessboard = recreator.CreateAbsBoardAndRecreateChessState(size, tokens);

        var isCheck = analyzer.IsCheckForAbsKing(chessboard, PieceColor.White);
        
        var checkText = isCheck ? "[Failed]".Color("Red") : "[Passed]".Color("Green");
        var testName = $"[{nameof(Test2)}]:".Bold().Color("Blue");
        
        Debug.Log($"{testName} White king is saved from danger {checkText}");
    }
}