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

    public bool IsUnderAttack { get => _underAttack; }
    protected bool _underAttack = false;

    protected bool _thisColorTurn = false;

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
        OnPieceMoved?.Invoke();
    }

    public void Death()
    {
        OnPieceDied?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Piece enemyPiece = collision.GetComponent<Piece>();

        if (enemyPiece != null)
        {
            PieceColor pieceColor = enemyPiece.ColorData.Color;

            if (pieceColor != _colorData.Color && _thisColorTurn)
            {
                Debug.Log(enemyPiece.name);
                enemyPiece.Death();
            }
        }
    }

    private void Start()
    {
        Init();
        _gameManager.OnTurnChanged += ChangeTurn;
    }

    private void Init()
    {
        _squareHandler = SquareHandler.Instance;
        _gameManager = GameManager.Instance;
    }

    private void OnDestroy()
    {
        _gameManager.OnTurnChanged -= ChangeTurn;
    }

    private void ChangeTurn(PieceColor color)
    {
        _thisColorTurn = color == _colorData.Color;
    }
}