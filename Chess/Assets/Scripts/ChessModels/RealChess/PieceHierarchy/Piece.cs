using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public bool IsFirstMove => _isFirstMove;
    protected bool _isFirstMove = true;
    
    public delegate void PieceMoveHandler();
    public static event PieceMoveHandler OnPieceMoved;

    public delegate void PieceDeadHandler(Piece chessPiece);
    public static event PieceDeadHandler OnPieceDied;
    
    public abstract PieceType MyType { get; }

    public static readonly Vector3 Offset = new Vector3(0f, 0f, 1f);

    public PieceColor MyColor => _myColor;
    [SerializeField] private PieceColor _myColor;

    protected GameManager _gameManager;

    [SerializeField] private Collider2D _collider;

    private Vector3 _normalSize;
    private Vector3 _selectedSize;

    public abstract List<Square> GetPossibleMoveTurns(Square square);
    public abstract List<Square> GetPossibleAttackTurns(Square square);

    public void Move(Square square)
    {
        transform.position = square.transform.position + Offset;

        float overlapRadius = 0.2f;
        Collider2D[] collidersInOverlapArea = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

        for (int i = 0; i < collidersInOverlapArea.Length; i++)
        {
            Piece enemyPiece = collidersInOverlapArea[i].GetComponent<Piece>();

            if (enemyPiece != null && _gameManager.WhoseTurn == _myColor)
            {
                Square squareWithPiece = SingletonRegistry.Instance.Board.GetSquareWithPiece(enemyPiece);
                squareWithPiece.PieceOnSquare = null;

                enemyPiece.Death();
            }
        }

        ResetAttackTurns();
        
        if (_isFirstMove) 
            _isFirstMove = false;
        
        OnPieceMoved?.Invoke();
    }

    protected virtual void ResetAttackTurns() { }

    public void Death()
    {
        OnPieceDied?.Invoke(this);
        Destroy(gameObject);
    }

    protected bool PieceStandsOnSquare(Square square)
    {
        if (square.PieceOnSquare)
            return true;

        return false;
    }

    protected bool PieceOnSquareHasOppositeColor(Square square)
    {
        return square.PieceOnSquare.MyColor != MyColor;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _gameManager = GameManager.Instance;
    }
}