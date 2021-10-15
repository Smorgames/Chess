﻿using UnityEngine;

public class Square : MonoBehaviour
{
    public delegate void SquareClickHandler(Square square);
    public static event SquareClickHandler OnSquareWithPieceClicked;
    public static event SquareClickHandler OnEmptySquareClicked;

    [SerializeField] private GameObject _highlight;
    
    [SerializeField] private Collider2D _collider;

    public Vector2Int Coordinates { get => _coordinates; }
    private Vector2Int _coordinates;

    public Piece PieceOnSquare { get => _pieceOnSquare; set => _pieceOnSquare = value; }
    private Piece _pieceOnSquare;

    public bool IsActive
    {
        get
        {
            if (_highlight.activeSelf) 
                return true;
            
            return false;
        }
    }

    private void OnMouseDown()
    {
        if (_pieceOnSquare != null)
            Test.ShowPossibleTurns(this);

        if (_pieceOnSquare != null && NowTurnOfThisPiece())
            OnSquareWithPieceClicked?.Invoke(this);
        else
            OnEmptySquareClicked?.Invoke(this);
    }

    private bool NowTurnOfThisPiece()
    {
        return _pieceOnSquare.ColorData.Color == GameManager.Instance.WhoseTurn;
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