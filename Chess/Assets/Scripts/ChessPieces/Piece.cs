using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public delegate void PieceMoveHandler();
    public static event PieceMoveHandler OnPieceMoved;

    public delegate void PieceDeadHandler(Piece chessPiece);
    public static event PieceDeadHandler OnPieceDied;

    public static readonly Vector3 Offset = new Vector3(0f, 0f, 1f);

    public ColorData ColorData { get => _colorData; }
    [SerializeField] private ColorData _colorData;

    //public bool IsUnderAttack { get => _underAttack; }
    //protected bool _underAttack = false;

    protected SquareHandler _squareHandler;
    protected GameManager _gameManager;

    [SerializeField] private Collider2D _collider;

    private Vector3 _normalSize;
    private Vector3 _selectedSize;

    public abstract List<Square> GetPossibleMoveTurns(Square squareWithThis);
    public abstract List<Square> GetPossibleAttackTurns(Square squareWithThis);

    public virtual void Move(Square cell)
    {
        transform.position = cell.transform.position + Offset;

        float overlapRadius = 0.2f;
        Collider2D[] collidersInOverlapArea = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

        for (int i = 0; i < collidersInOverlapArea.Length; i++)
        {
            Piece enemyPiece = collidersInOverlapArea[i].GetComponent<Piece>();

            if (enemyPiece != null)
            {
                PieceColor pieceColor = enemyPiece.ColorData.Color;

                if (_gameManager.WhoseTurn == _colorData.Color)
                    enemyPiece.Death();
            }
        }

        OnPieceMoved?.Invoke();
    }

    public void Death()
    {
        OnPieceDied?.Invoke(this);
        Destroy(gameObject);
    }

    protected bool IsPieceStandsOnSquare(Square square)
    {
        if (square.PieceOnThis)
            return true;

        return false;
    }

    protected bool IsPieceOnSquareHasOppositeColor(Square square)
    {
        return square.PieceOnThis.ColorData.Color != ColorData.Color;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _squareHandler = SquareHandler.Instance;
        _gameManager = GameManager.Instance;
    }
}

[System.Serializable]
public class ColorData
{
    public PieceColor Color { get { return _color; } }
    [SerializeField] private PieceColor _color;

    public int Multiplier
    {
        get
        {
            if (_color == PieceColor.Black)
                return -1;
            else if (_color == PieceColor.White)
                return 1;
            return 0;
        }
    }
    private int _colorMultiplier;
}

[System.Serializable]
public enum PieceColor
{
    Black,
    White
}