using System;
using System.Text;
using AbstractChess;
using AnalysisOfChessState;
using UnityEngine;
using Instruments;

public class TestAbstractChess : MonoBehaviour
{
    private AbsChessboard _absChessboard;
    
    private void Start()
    {
        ChessSystemTesting.Test1();
        ChessSystemTesting.Test2();

        var code = "11wp54wQ12bB";
        var index = code.IndexOf("12", StringComparison.Ordinal);
        Debug.Log(index);
        var sb = new StringBuilder();
        
        for (int i = 0; i < code.Length; i++)
        {
            var l = code[i].ToString();
            if (i == index)
            {
                l = "55";
                ++i;
            }

            sb.Append(l);
        }
        Debug.Log(sb.ToString());
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
}