using Instruments;
using UnityEngine;

public class TestAnalyzer : MonoBehaviour
{
    private void Start()
    {
        //Test1();
        //Test2();
        Test3();
        Test4();
        Test5();
        //Test6();
        Test7();
    }

    // private void Test1()
    // {
    //     // Created chess board [1; 3] where white pieces turn first
    //     var center = new Vector2(12, 0);
    //     var size = new Vector2Int(1, 3);
    //     var board = ChessboardBuilder.BuildArbitraryChessboard(center, size);
    //     board.WhoseTurn = "w";
    //     
    //     // Put black King on square [0; 1] 
    //     var arranger = new StateRecreator();
    //     var tokens = StateCodeHandler.GetTokens(new ChessCode("01bK"));
    //     arranger.ArrangePiecesOnRealBoard(tokens, board);
    //
    //     // Check encode methode
    //     var newCode = Analyzer.EncodeRealBoard(board);
    //     var assert1 = string.Equals("0;1;b;K;1_", newCode.PiecesState);
    //     var assert2 = string.Equals("1;3", newCode.BoardSize);
    //     var assert3 = string.Equals("w", newCode.WhoseTurn);
    //     
    //     var text = assert1 && assert2 && assert3 ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
    //     var test = $"{nameof(TestAnalyzer)} [Test1]:".Bold().Color("LightBlue");
    //     var debug = $"{test} Checking the correctness of the {nameof(Analyzer)}.{nameof(Analyzer.EncodeRealBoard)}() {text}";
    //     
    //     Debug.Log(debug);
    // }

    //private void Test2()
    //{
    //    // Created chess board [2; 3] where white pieces turn first
    //    var center = new Vector2(12, -5);
    //    var size = new Vector2Int(2, 3);
    //    var board = ChessboardBuilder.BuildArbitraryChessboard(center, size);
    //    board.WhoseTurn = "w";

    //    // Put black King on square [0; 1] 
    //    var arranger = new StateRecreator();
    //    var tokens = StateCodeHandler.GetTokens(new ChessCode("01bK"));
    //    arranger.ArrangePiecesOnRealBoard(tokens, board);

    //    var absBoard = Analyzer.AbsBoardFromRealBoard(board);

    //    var sb = new StringBuilder();
    //    for (int x = 0; x < absBoard.Size.x; x++)
    //        for (int y = 0; y < absBoard.Size.y; y++)
    //            sb.Append(absBoard.Squares[x, y]);

    //    var assert = string.Equals(sb.ToString(), "[0;0 ][0;1 Black king][0;2 ][1;0 ][1;1 ][1;2 ]");

    //    var text = assert ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
    //    var test = $"{nameof(TestAnalyzer)} [Test2]:".Bold().Color("LightBlue");
    //    var debug = $"{test} Checking the correctness of the {nameof(Analyzer)}.{nameof(Analyzer.AbsBoardFromRealBoard)}() {text}";

    //    Debug.Log(debug);
    //}

    private void Test3()
    {
        // Create chess code
        // [  ] [  ] [bK]
        // [  ] [  ] [  ]
        // [wb] [  ] [  ]
        var piecesState = "0;0;w;b;1_2;2;b;K;0_";
        var boardSize = "3;3";
        var whoseTurn = "b";
        var chessCode = new ChessCode(piecesState, boardSize, whoseTurn);

        // In this situation there is check for black king
        var assert = Analyzer.IsCheckForKingBasedOnChessCode(chessCode, "b");

        var text = assert ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
        var test = $"{nameof(TestAnalyzer)} [Test3]:".Bold().Color("LightBlue");
        var debug = $"{test} Checking the correctness of the {nameof(Analyzer)}.{nameof(Analyzer.IsCheckForKingBasedOnChessCode)}() {text}";

        Debug.Log(debug);
    }

    private void Test4()
    {
        // Create chess code
        // [bK] [  ] [  ]
        // [  ] [  ] [  ]
        // [wb] [  ] [  ]
        var piecesState = "0;0;w;b;1_0;2;b;K;0_";
        var boardSize = "3;3";
        var whoseTurn = "b";
        var chessCode = new ChessCode(piecesState, boardSize, whoseTurn);

        // In this situation there is no check for black king
        var assert = !Analyzer.IsCheckForKingBasedOnChessCode(chessCode, "b");

        var text = assert ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
        var test = $"{nameof(TestAnalyzer)} [Test4]:".Bold().Color("LightBlue");
        var debug = $"{test} Checking the correctness of the {nameof(Analyzer)}.{nameof(Analyzer.IsCheckForKingBasedOnChessCode)}() {text}";

        Debug.Log(debug);
    }

    private void Test5()
    {
        // Create chess code
        // [  ] [  ] [wr] [  ]
        // [  ] [  ] [  ] [wQ]
        // [  ] [bK] [  ] [  ]
        // [wr] [  ] [  ] [  ]
        var piecesState = "0;0;w;r;0_1;1;b;K;0_2;3;w;r;0_3;2;w;Q;0_";
        var boardSize = "4;4";
        var whoseTurn = "b";
        var chessCode = new ChessCode(piecesState, boardSize, whoseTurn);

        // In this situation there is no check for black king
        var assert = !Analyzer.IsCheckForKingBasedOnChessCode(chessCode, "b");

        var text = assert ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
        var test = $"{nameof(TestAnalyzer)} [Test5]:".Bold().Color("LightBlue");
        var debug = $"{test} Checking the correctness of the {nameof(Analyzer)}.{nameof(Analyzer.IsCheckForKingBasedOnChessCode)}() {text}";

        Debug.Log(debug);
    }

    //private void Test6()
    //{
    //    // Created chess board [3; 3] where white pieces turn first
    //    var center = new Vector2(12, -10);
    //    var size = new Vector2Int(3, 3);
    //    var board = ChessboardBuilder.BuildArbitraryChessboard(center, size);
    //    board.WhoseTurn = "w";

    //    // Put black Queen on [0; 2], white Pawn [1; 0], white King on [2; 0]
    //    var arranger = new StateRecreator();
    //    var tokens = StateCodeHandler.GetTokens(new ChessCode("02bQ10wp20wK"));
    //    arranger.ArrangePiecesOnRealBoard(tokens, board);

    //    var squareWithWhitePawn = board.Squares[1, 0];
    //    var movesWithoutCheck = Analyzer.GetMovesWithoutCheckForKing(squareWithWhitePawn, ActionType.Movement);

    //    var assert1 = movesWithoutCheck.Count == 1;
    //    var assert2 = movesWithoutCheck[0].Coordinates == new Vector2Int(1, 1);

    //    var text = assert1 && assert2 ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
    //    var test = $"{nameof(TestAnalyzer)} [Test6]:".Bold().Color("LightBlue");
    //    var debug = $"{test} Checking the correctness of the {nameof(Analyzer)}.{nameof(Analyzer.GetMovesWithoutCheckForKing)}() {text}";

    //    Debug.Log(debug);
    //}

    private void Test7()
    {
        var piecesState = "0;2;b;b;0_2;2;b;b;1_1;1;w;p;0_2;0;w;K;1_";
        var boardSize = "3;3";
        var whoseTurn = "w";
        var chessCode = new ChessCode(piecesState, boardSize, whoseTurn);
        var absBoard = Analyzer.AbsBoardFromChessCode(chessCode);

        var assert1 = absBoard.Squares[0, 2].PieceOnIt.ColorCode == "b" &&
                      absBoard.Squares[0, 2].PieceOnIt.TypeCode == "b" &&
                      absBoard.Squares[0, 2].PieceOnIt.IsFirstMove == false;
        var assert2 = absBoard.Squares[2, 2].PieceOnIt.ColorCode == "b" &&
                      absBoard.Squares[2, 2].PieceOnIt.TypeCode == "b" &&
                      absBoard.Squares[2, 2].PieceOnIt.IsFirstMove;
        var assert3 = absBoard.Squares[1, 1].PieceOnIt.ColorCode == "w" &&
                      absBoard.Squares[1, 1].PieceOnIt.TypeCode == "p" &&
                      absBoard.Squares[1, 1].PieceOnIt.IsFirstMove == false;
        var assert4 = absBoard.Squares[2, 0].PieceOnIt.ColorCode == "w" &&
                      absBoard.Squares[2, 0].PieceOnIt.TypeCode == "K" &&
                      absBoard.Squares[2, 0].PieceOnIt.IsFirstMove;
        
        var text = assert1 && assert2 && assert3 && assert4 ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
        var test = $"{nameof(TestAnalyzer)} [Test7]:".Bold().Color("LightBlue");
        var debug = $"{test} Checking the correctness of the {nameof(Analyzer)}.{nameof(Analyzer.AbsBoardFromChessCode)}() {text}";

        Debug.Log(debug);
    }
}