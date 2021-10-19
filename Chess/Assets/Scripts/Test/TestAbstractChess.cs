using System;
using System.Text;
using AbstractChess;
using AnalysisOfChessState;
using AnalysisOfChessState.CodeHandler;
using AnalysisOfChessState.Recreator;
using UnityEngine;
using Instruments;

public class TestAbstractChess : MonoBehaviour
{
    private AbsChessboard _absChessboard;
    
    private void Start()
    {
        var s = "11gH43bf";
        Debug.Log(s);
        var c = "43";
        var nc = "";

        s = s.Replace(c, nc);
        Debug.Log(s);
        //ChessSystemTesting.Test1();
        //ChessSystemTesting.Test2();
        //ChessSystemTesting.Test3();
    }
}

public static class ChessSystemTesting
{
    public static void Test1()
    {
        var boardSize = new Vector2Int(5, 5);

        var stateCode = new StateCode("10wb32bK"); // White bishop on square (1,0) and check black king (3, 2)
        var analyzer = new ChessStateAnalyzer();

        var isCheck = analyzer.IsCheckForAbstractKing(stateCode, boardSize, PieceColor.Black);
        
        var isPassed = isCheck ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
        var testName = $"[{nameof(Test1)}]:".Bold().Color("LightBlue");
        
        Debug.Log($"{testName} Check for black king {isPassed}");
        
    }
    
    public static void Test2()
    {
        var chessStateCode = new StateCode("00br02wp03wK13wp23bQ");
        var boardSize = new Vector2Int(3, 4);
        
        var analyzer = new ChessStateAnalyzer();
        
        var isCheck = analyzer.IsCheckForAbstractKing(chessStateCode, boardSize, PieceColor.White);
        
        var checkText = isCheck ? "[Failed]".Color("Red") : "[Passed]".Color("Green");
        var testName = $"[{nameof(Test2)}]:".Bold().Color("LightBlue");
        
        Debug.Log($"{testName} White king is saved from danger {checkText}");
    }
    
    public static void Test3()
    {
        var center = new Vector2(7, 5);
        var size = new Vector2Int(3, 3);

        var board = SingletonRegistry.Instance.Builder.BuildArbitraryChessboard(center, size);
        var code = new StateCode("00bK02br12wp22wK");

        var codeHandler = new StateCodeHandler();
        var tokens = codeHandler.GetTokens(code);
        
        var stateRecreator = new StateRecreator();
        stateRecreator.ArrangePiecesOnRealBoard(tokens, board);
    }
}