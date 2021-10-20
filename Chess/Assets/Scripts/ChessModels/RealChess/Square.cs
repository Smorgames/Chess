using UnityEngine;

public class Square : MonoBehaviour
{
    public bool IsGhost => _isGhost;
    [SerializeField] private bool _isGhost; 
    public delegate void SquareClickHandler(Square square);
    public static event SquareClickHandler OnSquareWithPieceClicked;
    public static event SquareClickHandler OnEmptySquareClicked;

    [SerializeField] private GameObject _highlight;

    public Chessboard Board => _board;
    private Chessboard _board;
    
    public Vector2Int Coordinates { get => _coordinates; }
    private Vector2Int _coordinates;

    public Piece PieceOnIt { get => _pieceOnIt; set => _pieceOnIt = value; }
    private Piece _pieceOnIt;

    private void OnMouseDown()
    {
        if (_pieceOnIt != null)
            Test.ShowPossibleTurns(this);

        if (_pieceOnIt != null && NowTurnOfThisPiece())
            OnSquareWithPieceClicked?.Invoke(this);
        else
            OnEmptySquareClicked?.Invoke(this);
    }

    private bool NowTurnOfThisPiece()
    {
        return _pieceOnIt.MyColor == GameManager.Instance.WhoseTurn;
    }

    public void Initialize(Vector2Int coordinates, Chessboard board)
    {
        _coordinates = coordinates;
        _board = board;
    }

    public void Activate()
    {
        _highlight.SetActive(true);
    }

    public void Deactivate()
    {
        _highlight.SetActive(false);
    }
}