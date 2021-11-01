using System;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public static EventHandler<TurnOrderEventArgs> OnTurnOrderChanged;
    public enum GameState { Initialization, Playing, Ended }

    private GameState _currentState;
    
    public NewPieceColor WhoseTurn { get; private set; }

    public static NewGameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _currentState = GameState.Initialization;
        WhoseTurn = NewPieceColor.Black;
        ChangeTurnOrder();
        NewPiece.OnPieceMoved += OnPieceMoved;

        _currentState = GameState.Playing;
    }

    private void OnDestroy()
    {
        NewPiece.OnPieceMoved -= OnPieceMoved;
    }
    
    private void OnPieceMoved(object sender, PieceMovedEventArgs e) => ChangeTurnOrder();

    private void ChangeTurnOrder()
    {
        WhoseTurn = WhoseTurn == NewPieceColor.White ? NewPieceColor.Black : NewPieceColor.White;
        OnTurnOrderChanged?.Invoke(this, new TurnOrderEventArgs(WhoseTurn));
    }
}

public class TurnOrderEventArgs : EventArgs
{
    public readonly NewPieceColor WhoseTurn;

    public TurnOrderEventArgs(NewPieceColor whoseTurn)
    {
        WhoseTurn = whoseTurn;
    }
}