﻿using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour, IRealPiece
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

    public Transform PiecesTransform => transform;
    public void Move(IRealSquare square)
    {
        transform.position = square.MyTransform.position + Offset;

        var overlapRadius = 0.2f;
        var collidersInOverlapArea = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

        for (int i = 0; i < collidersInOverlapArea.Length; i++)
        {
            var enemyPiece = collidersInOverlapArea[i].GetComponent<Piece>();

            if (enemyPiece != null && _gameManager.WhoseTurn == ColorCode)
            {
                var squareWithPiece = square.RealBoard.GetRealSquareWithPiece(enemyPiece);
                squareWithPiece.RealPieceOnIt = null;

                enemyPiece.Death();
            }
        }

        ResetAttackTurns();
        
        if (_isFirstMove) 
            _isFirstMove = false;
        
        OnPieceMoved?.Invoke();
    }

    public abstract List<IRealSquare> GetRealMoves(IRealSquare realSquare);
    public abstract List<IRealSquare> GetRealAttacks(IRealSquare realSquare);
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