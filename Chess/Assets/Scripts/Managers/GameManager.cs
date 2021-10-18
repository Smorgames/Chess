using System.Collections.Generic;
using AbstractChess;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void ChangeTurnHandler();
    public event ChangeTurnHandler OnTurnChanged;

    public PieceColor WhoseTurn { get => _whoseTurn; }
    private PieceColor _whoseTurn;
    
    public Chessboard RealChessboard => _chessboard;
    [SerializeField] private Chessboard _chessboard;

    [Header("Handlers and managers")]
    [SerializeField] private ChessboardFiller _filler;

    [SerializeField] private PieceColor _whoseTurnFirst;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _filler.InitializeChessboard(_chessboard);

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
    }

    private void ChangeColorOrderOfTurn()
    {
        if (_whoseTurn == PieceColor.Black)
            _whoseTurn = PieceColor.White;
        else if (_whoseTurn == PieceColor.White)
            _whoseTurn = PieceColor.Black;
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