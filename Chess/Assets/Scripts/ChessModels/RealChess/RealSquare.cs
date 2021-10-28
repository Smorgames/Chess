using UnityEngine;

public class RealSquare : MonoBehaviour, ISquare
{
    #region Events

    public delegate void SquareClickHandler(RealSquare realSquare);
    public static event SquareClickHandler OnSquareWithPieceClicked;
    public static event SquareClickHandler OnEmptySquareClicked;

    #endregion

    [SerializeField] private GameObject _moveHighlight;
    [SerializeField] private GameObject _attackHighlight;

    #region Interface implementation

    public bool IsGhost { get => _isGhost; set => _isGhost = value;}
    [SerializeField] private bool _isGhost;
    public Vector2Int Coordinates { get; private set; }
    public IPiece PieceOnIt => RealPieceOnIt;
    public RealPiece RealPieceOnIt { get; set; }
    public IChessBoard Board => RealBoard;
    public RealChessBoard RealBoard { get; private set; }

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

    public void Initialize(Vector2Int coordinates, RealChessBoard board)
    {
        Coordinates = coordinates;
        RealBoard = board;
    }

    public void ActivateHighlight()
    {
        _moveHighlight.SetActive(true);
    }

    public void DeactivateHighlight()
    {
        _moveHighlight.SetActive(false);
    }
}