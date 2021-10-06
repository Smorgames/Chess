using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public delegate void PieceMoveHandler();
    public static event PieceMoveHandler OnPieceMoved;

    public delegate void PieceDeadHandler(Piece chessPiece);
    public static event PieceDeadHandler OnPieceDied;

    public static readonly Vector3 Offset = new Vector3(0f, 0f, 1f);

    public ColorData ColorData
    {
        get => _colorData;
        set
        {
            if (!_isColorDataInitialized)
            {
                _colorData.Color = value.Color;
                _isColorDataInitialized = true;
            }
        }
    }
    [SerializeField] private ColorData _colorData;
    private bool _isColorDataInitialized;

    protected SquareHandler _squareHandler;
    protected GameManager _gameManager;

    [SerializeField] private Collider2D _collider;

    private Vector3 _normalSize;
    private Vector3 _selectedSize;

    public abstract List<Square> GetPossibleMoveTurns(Square squareWithThis);
    public abstract List<Square> GetPossibleAttackTurns(Square squareWithThis);

    public virtual void Move(Square square)
    {
        transform.position = square.transform.position + Offset;

        float overlapRadius = 0.2f;
        Collider2D[] collidersInOverlapArea = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

        for (int i = 0; i < collidersInOverlapArea.Length; i++)
        {
            Piece enemyPiece = collidersInOverlapArea[i].GetComponent<Piece>();

            if (enemyPiece != null)
            {
                Color pieceColor = enemyPiece.ColorData.Color;

                if (_gameManager.WhoseTurn == _colorData.Color)
                {
                    Square squareWithPiece = _squareHandler.GetSquareWithPiece(enemyPiece);
                    squareWithPiece.PieceOnThis = null;

                    enemyPiece.Death();
                }
            }
        }

        ResetAttackTurns();
        OnPieceMoved?.Invoke();
    }

    protected virtual void ResetAttackTurns() { }

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
    
    [System.Serializable]
    public enum Color
    {
        None,
        Black,
        White
    }
}

[System.Serializable]
public class ColorData
{
    public Piece.Color Color
    {
        get => _color;
        set
        {
            if (!_isColorInitialized)
            {
                _color = value;
                _isColorInitialized = true;
            }
        }
    }
    [SerializeField] private Piece.Color _color;
    private bool _isColorInitialized;

    public int Multiplier
    {
        get
        {
            if (_color == Piece.Color.Black)
                return -1;
            else if (_color == Piece.Color.White)
                return 1;
            return 0;
        }
    }
    private int _colorMultiplier;
}