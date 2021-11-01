using System;
using UnityEngine;

public class NewSquare : MonoBehaviour
{
    public static EventHandler<SquareWithPieceClickedArgs> OnSquareWithPieceClicked;
    public static EventHandler<EmptySquareClickedArgs> OnEmptySquareClicked;
    
    [SerializeField] private GameObject _moveHighlight;
    [SerializeField] private GameObject _attackHighlight;
    
    public NewBoard Board { get; private set; }
    public Vector2Int Coordinates => _coordinates;
    private Vector2Int _coordinates;
    public NewPiece PieceOnIt { get; set; }

    public void Initialize(Vector2Int coordinates, NewBoard board)
    {
        Board = board;
        SetCoordinates(coordinates);
    }
    private void SetCoordinates(Vector2Int coordinates) => _coordinates = coordinates;

    private void OnMouseDown()
    {
        if (PieceOnIt != null && PieceOnIt.MySignature.MyColor == NewGameManager.Instance.WhoseTurn)
            OnSquareWithPieceClicked?.Invoke(this, new SquareWithPieceClickedArgs() { Square = this });
        else if (PieceOnIt == null)
            OnEmptySquareClicked?.Invoke(this, new EmptySquareClickedArgs() { Square = this });
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
        NewGameManager.OnTurnOrderChanged += TurnOrderChanged;
    }

    private void OnDestroy()
    {
        NewGameManager.OnTurnOrderChanged -= TurnOrderChanged;
    }
    
    private void TurnOrderChanged(object sender, TurnOrderEventArgs e)
    {
        if (PieceOnIt == null) return;
        if (PieceOnIt.MySignature.MyColor == e.WhoseTurn)
            PieceOnIt.UpdateSupposedMoves(this);
    }
}

public class SquareWithPieceClickedArgs : EventArgs
{
    public NewSquare Square;
}

public class EmptySquareClickedArgs : EventArgs
{
    public NewSquare Square;
}