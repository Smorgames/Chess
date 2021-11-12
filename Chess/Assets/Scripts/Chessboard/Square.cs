using System;
using UnityEngine;

public class Square : MonoBehaviour
{
    public static EventHandler<SquareClickedArgs> OnPieceWhoseTurnNowClicked;
    public static EventHandler<SquareClickedArgs> OnPieceWhoNotTurnNowClicked;
    public static EventHandler<SquareClickedArgs> OnEmptySquareClicked;

    public Board MyBoard { get; private set; }
    public Vector2Int Coordinates => _coordinates;
    private Vector2Int _coordinates;
    public Piece PieceOnIt { get; set; }

    public EnPassant MyEnPassant { get; } = new EnPassant() { MyPawn = null, Possible = false };

    [SerializeField] private GameObject _moveHighlight;
    [SerializeField] private GameObject _attackHighlight;
    
    public void Initialize(Vector2Int coordinates, Board board)
    {
        MyBoard = board;
        SetCoordinates(coordinates);
    }
    
    private void SetCoordinates(Vector2Int coordinates) => _coordinates = coordinates;

    private void OnMouseDown()
    {
        if (Game.Instance.CurrentState == Game.GameState.Paused) return;
        
        if (PieceOnIt != null && PieceOnIt.ColorCode == Game.Instance.WhoseTurn)
            OnPieceWhoseTurnNowClicked?.Invoke(this, new SquareClickedArgs() { MySquare = this });
        else if (PieceOnIt != null && PieceOnIt.ColorCode != Game.Instance.WhoseTurn)
            OnPieceWhoNotTurnNowClicked?.Invoke(this, new SquareClickedArgs() { MySquare = this });
        else if (PieceOnIt == null)
            OnEmptySquareClicked?.Invoke(this, new SquareClickedArgs() { MySquare = this });
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

    #region Events

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
        if (MyEnPassant.Possible && Game.Instance.WhoseTurn == MyEnPassant.MyPawn.ColorCode)
            MyEnPassant.Reset();
        if (PieceOnIt == null) return;
        PieceOnIt.UpdateSupposedMoves(this);
    }

    #endregion
}

[Serializable]
public class EnPassant
{
    public Pawn MyPawn { get; set; }
    public bool Possible { get; set; }

    public void Set(Pawn pawn)
    {
        MyPawn = pawn;
        Possible = true;
    }
    
    public void Reset()
    {
        MyPawn = null;
        Possible = false;
    }
}

public class SquareClickedArgs : EventArgs
{
    public Square MySquare;
}