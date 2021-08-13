using UnityEngine;

public class Square : MonoBehaviour
{
    public delegate void SquareClickHandler(Square square);
    public static event SquareClickHandler OnSquareWithPieceClicked;
    public static event SquareClickHandler OnEmptySquareClicked;

    [SerializeField] private GameObject _highlight;
    [SerializeField] private Collider2D _collider;

    public Vector2Int Coordinates { get => _coordinates; }
    private Vector2Int _coordinates;

    public Piece PieceOnThis { get => _pieceOnThis; set => _pieceOnThis = value; }
    private Piece _pieceOnThis;

    public bool IsActive
    {
        get
        {
            if (_highlight.activeSelf)
                return true;
            else 
                return false;
        }
    }

    private void OnMouseDown()
    {
        if (_pieceOnThis != null && NowTurnOfThisPiece())
        {
            OnSquareWithPieceClicked?.Invoke(this);
        }
        else
        {
            OnEmptySquareClicked?.Invoke(this);
        }
    }

    private bool NowTurnOfThisPiece()
    {
        return _pieceOnThis.ColorData.Color == GameManager.Instance.WhoseTurn;
    }

    public void SetCoordinates(int x, int y)
    {
        _coordinates.x = x;
        _coordinates.y = y;
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