using System.Text;
using AnalysisOfChessState;
using AnalysisOfChessState.CodeHandler;
using AnalysisOfChessState.Recreator;
using UnityEngine;
using Instruments;

public class TestAbstractChess : MonoBehaviour
{
    private void Start()
    {
        ChessSystemTesting.Test1();
        ChessSystemTesting.Test2();
        ChessSystemTesting.Test3();
        ChessSystemTesting.Test4();
        ChessSystemTesting.Test5();
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
        var code = new StateCode("02br20bQ22wK42br24bQ13wr23wr33wr");

        var board = ChessboardBuilder.BuildArbitraryChessboard(new Vector2(15, 5), new Vector2Int(5, 5));
        
        var recreator = new StateRecreator();
        var ch = new StateCodeHandler();
        
        recreator.ArrangePiecesOnRealBoard(ch.GetTokens(code), board);
        
        var size = new Vector2Int(5, 5);
        var analyzer = new ChessStateAnalyzer();

        var isMate = analyzer.MateForAbstractKing(code, size, PieceColor.White);
        
        var mateText = isMate ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
        var testName = $"[{nameof(Test3)}]:".Bold().Color("LightBlue");
        
        Debug.Log($"{testName} Mate for white king {mateText}");
    }
    
    public static void Test4()
    {
        var square = DebugChessboardState.MainChessboard.Squares[1, 0];
        SquareHandler.TryGetStateCodeOfSquare(square, out var code);

        var isCorrectCode = string.Equals(code.Value, "[1;0;w;k;1]");
        var mateText = isCorrectCode ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
        var testName = $"[{nameof(Test4)}]:".Bold().Color("LightBlue");
        
        Debug.Log($"{testName} Code for [1;0] square with white knight is [1;0;w;k;1] {mateText}");
    }
    
    public static void Test5()
    {
        var a = new ChessStateAnalyzer();
        var size = new Vector2Int(3, 3);
        var code = new StateCode("[0;0;w;p;1][2;1;w;Q;0][0;2;b;K;0]");
        var ac = a.AbsBoardBasedOnCode(code, size);

        for (int y = ac.Size.y - 1; y >= 0; y--)
        {
            var sb = new StringBuilder();
            
            for (int x = 0; x < ac.Size.x; x++)
                sb.Append($"{ac.Squares[x,y] }");

            Debug.Log(sb.ToString());
        }
    }
}