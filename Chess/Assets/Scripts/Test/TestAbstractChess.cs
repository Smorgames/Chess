using AbstractChess;
using AnalysisOfChessState;
using AnalysisOfChessState.Parser;
using AnalysisOfChessState.Recreator;
using UnityEngine;
using Instruments;

public class TestAbstractChess : MonoBehaviour
{
    private AbsChessboard _absChessboard;
    
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
        var chessboard = new AbsChessboard(boardSize);
        
        var chessStateCode = "10wb32bK"; // White bishop on square (1,0) and check black king (3, 2)
        var recreator = new ChessStateRecreator();
        var parser = new ChessParser();
        
        var analyzer = new ChessStateAnalyzer();
        var tokens = parser.Parse(chessStateCode);

        recreator.RecreateChessState(chessboard, tokens);

        var isCheck = analyzer.IsCheckForAbstractKing(chessboard, PieceColor.Black);
        
        var isPassed = isCheck ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
        var testName = $"[{nameof(Test1)}]:".Bold().Color("LightBlue");
        
        Debug.Log($"{testName} Check for black king {isPassed}");
    }
    
    public static void Test2()
    {
        var chessStateCode = "00br02wp03wK13wp23bQ";
        var boardSize = new Vector2Int(3, 4);
        
        var analyzer = new ChessStateAnalyzer();
        
        var isCheck = analyzer.IsCheckForAbstractKing(chessStateCode, boardSize, PieceColor.White);
        
        var checkText = isCheck ? "[Failed]".Color("Red") : "[Passed]".Color("Green");
        var testName = $"[{nameof(Test2)}]:".Bold().Color("LightBlue");
        
        Debug.Log($"{testName} White king is saved from danger {checkText}");
    }
}