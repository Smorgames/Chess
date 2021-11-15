using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memento : MonoBehaviour
{
    #region Singleton

    private static Memento _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
    }

    #endregion

    private Stack<ChessCode> _gameStates = new Stack<ChessCode>();
    private Stack<List<MoveCache>> _cachedMoves = new Stack<List<MoveCache>>();

    private bool _needRememberGameState = false;
    
    public void AddMove(List<MoveCache> movesOnThisTurn) => _cachedMoves.Push(movesOnThisTurn);

    public void UndoMove()
    {
        var moves = _cachedMoves.Pop();

        foreach (var move in moves)
        {
            
        }

        _needRememberGameState = false;
        Game.Instance.ChangeTurnOrder();
    }

    private void AddCurrentState(object s, TurnOrderEventArgs a)
    {
        if (_needRememberGameState)
        {
            var chessCode = ChessCodeHandler.EncodeBoard(Game.Instance.GameBoard);
            _gameStates.Push(chessCode);
        }

        _needRememberGameState = true;
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