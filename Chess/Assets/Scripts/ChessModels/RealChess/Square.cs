using UnityEngine;

public class Square : MonoBehaviour, IRealSquare, IHighlightable
{
    public delegate void SquareClickHandler(Square square);
    public static event SquareClickHandler OnSquareWithPieceClicked;
    public static event SquareClickHandler OnEmptySquareClicked;

    [SerializeField] private GameObject _highlight;

    #region Interface implementation

    public bool IsGhost => _isGhost;
    [SerializeField] private bool _isGhost;
    
    public Vector2Int Coordinates { get; private set; }

    public IChessBoard Board => RealBoard;

    public Transform MyTransform => transform;
    
    public IRealChessBoard RealBoard { get; private set; }
    
    public IRealPiece RealPieceOnIt { get; set; }

    public IPiece PieceOnIt => RealPieceOnIt;
    
    public IHighlightable DisplayComponent => this;

    #endregion
    
    private void OnMouseDown()
    {
        if (RealPieceOnIt != null && NowTurnOfThisPiece())
            OnSquareWithPieceClicked?.Invoke(this);
        else
            OnEmptySquareClicked?.Invoke(this);
    }

    private bool NowTurnOfThisPiece()
    {
        return RealPieceOnIt.ColorCode == GameManager.Instance.WhoseTurn;
    }

    public void Initialize(Vector2Int coordinates, Chessboard board)
    {
        Coordinates = coordinates;
        RealBoard = board;
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