using AnalysisOfChessState.CodeHandler;
using AnalysisOfChessState.Recreator;
using Instruments;
using UnityEngine;

public class TestAnalyzer : MonoBehaviour
{
    private void Start()
    {
        Test1();
    }

    private void Test1()
    {
        // Created chess board [1; 3] where white pieces turn first
        var center = new Vector2(12, 0);
        var size = new Vector2Int(1, 3);
        var board = ChessboardBuilder.BuildArbitraryChessboard(center, size);
        board.WhoseTurn = "w";
        
        // Put black King on square [0; 1] 
        var arranger = new StateRecreator();
        var tokens = StateCodeHandler.GetTokens(new ChessCode("01bK"));
        arranger.ArrangePiecesOnRealBoard(tokens, board);

        // Check encode methode
        var newCode = Analyzer.EncodeRealBoard(board);
        var assert1 = string.Equals("0;1;b;K;1_", newCode.PiecesState);
        var assert2 = string.Equals("1;3", newCode.BoardSize);
        var assert3 = string.Equals("w", newCode.WhoseTurn);
        
        var text = assert1 && assert2 && assert3 ? "[Passed]".Color("Green") : "[Failed]".Color("Red");
        var test = $"{nameof(TestAnalyzer)} [Test1]:".Bold().Color("LightBlue");
        var debug = $"{test} Checking the correctness of the {nameof(Analyzer)}.{nameof(Analyzer.EncodeRealBoard)}() {text}";
        
        Debug.Log(debug);
    }
}