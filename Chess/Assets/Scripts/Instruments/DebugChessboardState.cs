using AnalysisOfChessState.CodeHandler;
using UnityEngine;

public class DebugChessboardState : MonoBehaviour
{
    public static Chessboard MainChessboard;
    
    public void DebugChessState()
    {
        var tokens = StateCodeHandler.GetTokens(MainChessboard);
        var code = StateCodeHandler.GetStateCode(tokens);
        Debug.Log(code.PiecesState);
    }
}