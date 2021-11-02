using System;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    public static EventHandler<TurnOrderEventArgs> OnTurnOrderChanged;
    public enum GameState { Initialization, Playing, Ended }
    public string WhoseTurn { get; private set; }
    public ChessPlayer WhitePlayer { get; private set; }
    public ChessPlayer BlackPlayer { get; private set; }

    private GameState _currentState;

    public static NewGameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _currentState = GameState.Initialization;
        WhoseTurn = "b";
        NewPiece.OnPieceMoved += OnPieceMoved;
        var board = ChessboardBuilder.BuildStandardChessboard();
        SetuperOfPieces.StandartSetup(board);
        var code = "0;0;wrt_1;0;wnt_2;0;wbt_3;0;wqt_4;0;wkt_5;0;wbt_6;0;wnt_7;0;wrt_" +
                   "0;1;wpt_1;1;wpt_2;1;wpt_3;1;wpt_4;1;wpt_5;1;wpt_6;1;wpt_7;1;wpt_" +
                   "0;7;brt_1;7;bnt_2;7;bbt_3;7;bqt_4;7;bkt_5;7;bbt_6;7;bnt_7;7;brt_" +
                   "0;6;bpt_1;6;bpt_2;6;bpt_3;6;bpt_4;6;bpt_5;6;bpt_6;6;bpt_7;6;bpt_";
        var data = NewAnalyzer.GetPieceStateDataFromStateCode(new StateCode(code));
        NewAnalyzer.ArrangePiecesOnBoard(data, board);
        WhitePlayer.TeamColor = NewPieceColor.White;
        BlackPlayer.TeamColor = NewPieceColor.Black;
        for (int x = 0; x < board.Size.x; x++)
        for (int y = 0; y < board.Size.y; y++)
        {
            var piece = board.Squares[x, y].PieceOnIt;
            if (piece == null) continue;

            if (piece.ColorCode == "w") WhitePlayer.AddPiece(piece);
            else BlackPlayer.AddPiece(piece);
        }

        ChangeTurnOrder();
        _currentState = GameState.Playing;
    }

    private void OnDestroy()
    {
        NewPiece.OnPieceMoved -= OnPieceMoved;
    }
    
    private void OnPieceMoved(object sender, PieceMovedEventArgs e) => ChangeTurnOrder();

    private void ChangeTurnOrder()
    {
        WhoseTurn = WhoseTurn == "w" ? "b" : "w";
        OnTurnOrderChanged?.Invoke(this, new TurnOrderEventArgs(WhoseTurn));
    }

    public void TriggerUpdateSupposedMoves() => OnTurnOrderChanged?.Invoke(this, new TurnOrderEventArgs(WhoseTurn));
}

public class TurnOrderEventArgs : EventArgs
{
    public readonly string WhoseTurn;

    public TurnOrderEventArgs(string whoseTurn)
    {
        WhoseTurn = whoseTurn;
    }
}