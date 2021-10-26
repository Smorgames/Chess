using System.Collections.Generic;
using UnityEngine;

public abstract class RealPiece : MonoBehaviour, IPiece
{
    public delegate void PieceMoveHandler();
    public static event PieceMoveHandler OnPieceMoved;

    public delegate void PieceDeadHandler(RealPiece chessRealPiece);
    public static event PieceDeadHandler OnPieceDied;

    public bool IsFirstMove => _isFirstMove;
    protected bool _isFirstMove = true;
    public abstract string TypeCode { get; }
    public string ColorCode => _colorCode;
    private string _colorCode;

    public static readonly Vector3 Offset = new Vector3(0f, 0f, 1f);

    [SerializeField] protected Sprite _blackSprite;
    [SerializeField] protected Sprite _whiteSprite;

    protected GameManager _gameManager;

    private Vector3 _normalSize;
    private Vector3 _selectedSize;

    public RealPiece SetColorCode(string colorCode)
    {
        _colorCode = colorCode;
        var spriteRenderer = GetComponent<SpriteRenderer>();

        if (_colorCode == "w") spriteRenderer.sprite = _whiteSprite;
        if (_colorCode == "b") spriteRenderer.sprite = _blackSprite;
        return this;
    }

    public void Move(RealSquare realSquare)
    {
        transform.position = realSquare.transform.position + Offset;

        var overlapRadius = 0.2f;
        var collidersInOverlapArea = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

        for (int i = 0; i < collidersInOverlapArea.Length; i++)
        {
            var enemyPiece = collidersInOverlapArea[i].GetComponent<RealPiece>();

            if (enemyPiece != null && _gameManager.WhoseTurn == ColorCode)
            {
                var squareWithPiece = realSquare.RealBoard.GetRealSquareWithPiece(enemyPiece);
                squareWithPiece.RealPieceOnIt = null;

                enemyPiece.Death();
            }
        }

        ResetAttackTurns();
        
        if (_isFirstMove) 
            _isFirstMove = false;
        
        OnPieceMoved?.Invoke();
    }
    
    public abstract List<ISquare> GetMoves(ISquare squareWithPiece);
    public abstract List<ISquare> GetAttacks(ISquare square);

    protected virtual void ResetAttackTurns() { }

    public void Death()
    {
        OnPieceDied?.Invoke(this);
        Destroy(gameObject);
    }

    protected bool PieceStandsOnSquare(ISquare square)
    {
        return square.PieceOnIt != null;
    }

    protected bool PieceOnSquareHasOppositeColor(ISquare square)
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