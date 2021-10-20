using System.Collections.Generic;
using AbstractChess;
using AnalysisOfChessState;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void ChangeTurnHandler();
    public event ChangeTurnHandler OnTurnChanged;

    public PieceColor WhoseTurn { get => _whoseTurn; }
    private PieceColor _whoseTurn = PieceColor.White;

    public ChessStateAnalyzer Analyzer => _analyzer;
    private ChessStateAnalyzer _analyzer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _analyzer = new ChessStateAnalyzer();

        var board = SingletonRegistry.Instance.Builder.BuildMainChessboard();
        //var board = SingletonRegistry.Instance.Builder.BuildArbitraryChessboard(Vector2.zero, new Vector2Int(9, 9));
        DebugChessboardState.MainChessboard = board;
        
        _analyzer.ArrangePiecesOnChessboard(board, GameStates.Start);

        SubscribeMethodsOnEvent();
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