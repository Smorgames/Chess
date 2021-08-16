using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void ChangeTurnHandler(PieceColor color);
    public event ChangeTurnHandler OnTurnChanged;

    public PieceColor WhoseTurn { get => _whoseTurn; }
    private PieceColor _whoseTurn;

    [SerializeField] private PiecesStorage _pieceStorage;

    [SerializeField] private PieceColor _whoseTurnFirst;

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        SubscribeMethodsOnEvent();
        SetWhichColorTurnFirst(_whoseTurnFirst);
    }

    private void SetWhichColorTurnFirst(PieceColor color)
    {
        _whoseTurn = color;
    }

    public void TriggerChangeTurn()
    {
        ChangeColorOrderOfTurn();
        OnTurnChanged?.Invoke(_whoseTurn);
    }

    private void ChangeColorOrderOfTurn()
    {
        if (_whoseTurn == PieceColor.Black)
            _whoseTurn = PieceColor.White;
        else if (_whoseTurn == PieceColor.White)
            _whoseTurn = PieceColor.Black;
    }

    private bool IsCheckState()
    {
        List<Piece> allPieces = _pieceStorage.AllPieces;
        List<Piece> piecesWhoseTurn = new List<Piece>();

        foreach (var piece in allPieces)
            if (piece.ColorData.Color == _whoseTurn)
                piecesWhoseTurn.Add(piece);

        
        List<Square> allPossibleTurns = new List<Square>();

        foreach (var piece in piecesWhoseTurn)
        {

        }
    }

    private void OnDestroy()
    {
        UnsubscribeMethodsOnEvent();
    }

    private void SubscribeMethodsOnEvent()
    {
        Piece.OnPieceMoved += TriggerChangeTurn;
    }

    private void UnsubscribeMethodsOnEvent()
    {
        Piece.OnPieceMoved -= TriggerChangeTurn;
    }
}