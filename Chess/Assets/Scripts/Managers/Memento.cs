using System.Collections.Generic;
using UnityEngine;

public class Memento : MonoBehaviour
{
    private List<ChessCode> _gameStates = new List<ChessCode>();

    private void AddCurrentState(object s, TurnOrderEventArgs a)
    {
        var gameBoard = Game.Instance.GameBoard;
        var chessCode = ChessCodeHandler.EncodeBoard(gameBoard);
        _gameStates.Add(chessCode);
    }

    public void RevertTurn()
    {
        
    }

    #region Events

    private void Start() => EventsManipulation(EventsManipulationType.Subscribe);
    private void OnDestroy() => EventsManipulation(EventsManipulationType.Unsubscribe);
    private void EventsManipulation(EventsManipulationType type)
    {
        if (type == EventsManipulationType.Subscribe) 
            Game.OnTurnOrderChanged += AddCurrentState;
        else
            Game.OnTurnOrderChanged -= AddCurrentState;
    }

    #endregion
}