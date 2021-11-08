using System;
using UnityEngine;

public class Square : MonoBehaviour
{
    public static EventHandler<ActivePieceClickedArgs> OnPieceWhoseTurnNowClicked;
    public static EventHandler<InactivePieceClickedArgs> OnPieceWhoNotTurnNowClicked;
    public static EventHandler<EmptySquareClickedArgs> OnEmptySquareClicked;
    
    [SerializeField] private GameObject _moveHighlight;
    [SerializeField] private GameObject _attackHighlight;
    
    public Board Board { get; private set; }
    public Vector2Int Coordinates => _coordinates;
    private Vector2Int _coordinates;
    public Piece PieceOnIt { get; set; }

    public void Initialize(Vector2Int coordinates, Board board)
    {
        Board = board;
        SetCoordinates(coordinates);
    }
    private void SetCoordinates(Vector2Int coordinates) => _coordinates = coordinates;

    private void OnMouseDown()
    {
        if (Game.Instance.CurrentState == Game.GameState.Paused) return;
        
        if (PieceOnIt != null && PieceOnIt.ColorCode == Game.Instance.WhoseTurn)
            OnPieceWhoseTurnNowClicked?.Invoke(this, new ActivePieceClickedArgs() { MySquare = this });
        else if (PieceOnIt != null && PieceOnIt.ColorCode != Game.Instance.WhoseTurn)
            OnPieceWhoNotTurnNowClicked?.Invoke(this, new InactivePieceClickedArgs() { MySquare = this });
        else if (PieceOnIt == null)
            OnEmptySquareClicked?.Invoke(this, new EmptySquareClickedArgs() { MySquare = this });
    }
    
    public void SetEnabledHighlight(bool isEnabled, ActionType actionType)
    {
        if (actionType == ActionType.Attack)
            _attackHighlight.SetActive(isEnabled);
        else
            _moveHighlight.SetActive(isEnabled);
    }

    public void DeactivateAllHighlights()
    {
        SetEnabledHighlight(false, ActionType.Attack);
        SetEnabledHighlight(false, ActionType.Movement);
    }

    private void Start()
    {
        Game.OnTurnOrderChanged += TurnOrderChanged;
    }

    private void OnDestroy()
    {
        Game.OnTurnOrderChanged -= TurnOrderChanged;
    }
    
    private void TurnOrderChanged(object sender, TurnOrderEventArgs e)
    {
        if (PieceOnIt == null) return;
        PieceOnIt.UpdateSupposedMoves(this);
    }
}

public class ActivePieceClickedArgs : EventArgs
{
    public Square MySquare;
}

public class InactivePieceClickedArgs : EventArgs
{
    public Square MySquare;
}

public class EmptySquareClickedArgs : EventArgs
{
    public Square MySquare;
}