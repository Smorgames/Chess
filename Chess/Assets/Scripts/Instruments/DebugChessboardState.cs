using AnalysisOfChessState.CodeHandler;
using UnityEngine;

public class DebugChessboardState : MonoBehaviour
{
    public void DebugChessState()
    {
        var codeHandler = new StateCodeHandler();
        var tokens = codeHandler.GetTokens(SingletonRegistry.Instance.Board);
        var code = codeHandler.GetStateCode(tokens);
        Debug.Log(code.Value);
    }
}