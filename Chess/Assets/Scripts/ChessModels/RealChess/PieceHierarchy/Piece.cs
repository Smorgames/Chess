using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour, IPiece, IMovable
{
    public bool IsFirstMove => _isFirstMove;
    protected bool _isFirstMove = true;
    
    public delegate void PieceMoveHandler();
    public static event PieceMoveHandler OnPieceMoved;

    public delegate void PieceDeadHandler(Piece chessPiece);
    public static event PieceDeadHandler OnPieceDied;
    
    public abstract PieceType MyType { get; }
    public abstract string TypeCode { get; }

    public static readonly Vector3 Offset = new Vector3(0f, 0f, 1f);

    public string ColorCode => _colorCode;
    [SerializeField] private string _colorCode;

    [SerializeField] private PieceColor _myColor;

    protected GameManager _gameManager;

    [SerializeField] private Collider2D _collider;

    private Vector3 _normalSize;
    private Vector3 _selectedSize;

    public abstract List<IRealSquare> GetMoves(IRealSquare square);
    public abstract List<IRealSquare> GetAttacks(IRealSquare square);

    public void Move(Square square)
    {
        transform.position = square.transform.position + Offset;

        float overlapRadius = 0.2f;
        Collider2D[] collidersInOverlapArea = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

        for (int i = 0; i < collidersInOverlapArea.Length; i++)
        {
            Piece enemyPiece = collidersInOverlapArea[i].GetComponent<Piece>();

            if (enemyPiece != null && _gameManager.WhoseTurn == ColorCode)
            {
                var squareWithPiece = square.Board.GetSquareWithPiece(enemyPiece);
                squareWithPiece.PieceOnIt = null;

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

    protected bool PieceStandsOnSquare(IRealSquare square)
    {
        if (square.PieceOnIt != null)
            return true;

        return false;
    }

    protected bool PieceOnSquareHasOppositeColor(IRealSquare square)
    {
        return square.PieceOnIt.ColorCode != _colorCode;
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