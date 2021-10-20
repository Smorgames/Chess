using AnalysisOfChessState.CodeHandler;
using UnityEngine;

public class DebugChessboardState : MonoBehaviour
{
    public static Chessboard MainChessboard;
    
    public void DebugChessState()
    {
        var codeHandler = new StateCodeHandler();
        var tokens = codeHandler.GetTokens(MainChessboard);
        var code = codeHandler.GetStateCode(tokens);
        Debug.Log(code.Value);
    }
}