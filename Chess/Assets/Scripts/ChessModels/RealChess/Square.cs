using UnityEngine;

public class Square : MonoBehaviour, IRealSquare, IHighlightable
{
    public bool IsGhost => _isGhost;
    [SerializeField] private bool _isGhost; 
    public delegate void SquareClickHandler(Square square);
    public static event SquareClickHandler OnSquareWithPieceClicked;
    public static event SquareClickHandler OnEmptySquareClicked;

    [SerializeField] private GameObject _highlight;

    public IRealChessBoard Board => _board;
    private IRealChessBoard _board;
    
    public Vector2Int Coordinates { get => _coordinates; }
    private Vector2Int _coordinates;

    public IPiece PieceOnIt { get => _pieceOnIt; set => _pieceOnIt = value; }

    public IHighlightable DisplayComponent => this;

    private IPiece _pieceOnIt;

    private void OnMouseDown()
    {
        if (_pieceOnIt != null && NowTurnOfThisPiece())
            OnSquareWithPieceClicked?.Invoke(this);
        else
            OnEmptySquareClicked?.Invoke(this);
    }

    private bool NowTurnOfThisPiece()
    {
        return _pieceOnIt.ColorCode == GameManager.Instance.WhoseTurn;
    }

    public void Initialize(Vector2Int coordinates, Chessboard board)
    {
        _coordinates = coordinates;
        _board = board;
    }

    public void RemovePieceFromSquare()
    {
        _pieceOnIt = null;
    }

    public void ActivateHighlight()
    {
        _highlight.SetActive(true);
    }

    public void DeactivateHighlight()
    {
        _highlight.SetActive(false);
    }
}