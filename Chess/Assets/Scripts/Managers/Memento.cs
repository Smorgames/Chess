using System;
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
    private Tuple<Move, Move> _move;

    private void AddCurrentState(object s, TurnOrderEventArgs a)
    {
        var gameBoard = Game.Instance.GameBoard;
        var chessCode = ChessCodeHandler.EncodeBoard(gameBoard);
        _gameStates.Push(chessCode);
    }

    private class Move
    {
        public readonly Square FromSquare;
        public readonly Piece FromPiece;

        public readonly Square ToSquare;
        public readonly Piece ToPiece;

        public Move(Square fromSquare, Piece fromPiece, Square toSquare, Piece toPiece)
        {
            FromSquare = fromSquare;
            FromPiece = fromPiece;
            ToSquare = toSquare;
            ToPiece = toPiece;
        }
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