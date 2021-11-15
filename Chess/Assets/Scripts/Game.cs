using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static EventHandler<TurnOrderEventArgs> OnTurnOrderChanged;
    
    public enum GameState { Initialization, Playing, Paused, Ended }
    public GameState CurrentState { get; set; }
    
    public string WhoseTurn { get; private set; }

    #region Players getters

    public ChessPlayer WhitePlayer { get; } = new ChessPlayer("w");
    public ChessPlayer BlackPlayer { get; } = new ChessPlayer("b");
    public ChessPlayer[] Players => _players;
    private ChessPlayer[] _players = new ChessPlayer[2];
    public ChessPlayer PlayerWhoseTurn => WhoseTurn == "w" ? WhitePlayer : BlackPlayer;
    public ChessPlayer PlayerWhoNotTurn => WhoseTurn == "w" ? BlackPlayer : WhitePlayer;

    #endregion

    public Board GameBoard { get; private set; }

    #region Singleton

    public static Game Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    #endregion

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        CurrentState = GameState.Initialization;
        _players[0] = WhitePlayer;
        _players[1] = BlackPlayer;
        EventsManipulation(EventsManipulationType.Subscribe);
        GameBoard = BoardBuilder.BuildStandardChessboard();
        var pieceSignatures = ChessCodeHandler.GetPieceSignaturesFromChessCode(UsefulChessCodes.StartChessState);
        GameSetuper.ArrangePiecesOnBoard(pieceSignatures, GameBoard);
        // After pieces had been arranged on board, GameSetuper triggers event OnPiecesArranged and GameManager raises PieceArranged function 
    }
    
    private void OnDestroy() => EventsManipulation(EventsManipulationType.Unsubscribe);

    private void EventsManipulation(EventsManipulationType type)
    {
        if (type == EventsManipulationType.Subscribe)
        {
            Piece.OnPieceMoved += PieceMoved;
            GameSetuper.OnPiecesArranged += PiecesArranged;
        }
        else
        {
            Piece.OnPieceMoved -= PieceMoved;
            GameSetuper.OnPiecesArranged -= PiecesArranged;
        }
    }

    private void PieceMoved(object sender, PieceMovedEventArgs e) => ChangeTurnOrder();
    public void ChangeTurnOrder()
    {
        WhoseTurn = WhoseTurn == "w" ? "b" : "w";
        foreach (var player in _players) player.UpdatePiecesSupposedMoves();
        
        PlayerWhoseTurn.ActivatePieces();
        PlayerWhoNotTurn.DeactivatePieces();
        
        CheckMateAnalyser.MateOrDrawForKing(WhoseTurn, out var mate, out var draw);
        if (mate)
        {
            var whoWin = WhoseTurn == "w" ? "Black" : "White";
            CurrentState = GameState.Ended;
            Debug.Log($"{whoWin} player won this chess game!");
        }

        if (draw)
        {
            CurrentState = GameState.Ended;
            Debug.Log($"Draw!");
        }
        
        OnTurnOrderChanged?.Invoke(this, new TurnOrderEventArgs());
        PlayerWhoseTurn.DeactivatePiecesWhoCanNotMove();
    }
    
    private void PiecesArranged(object sender, PiecesArrangedArgs e)
    {
        FillPlayersPieces();
        WhoseTurn = "b";
        ChangeTurnOrder();
        CurrentState = GameState.Playing;
    }
    private void FillPlayersPieces()
    {
        for (var x = 0; x < GameBoard.Size.x; x++)
        for (var y = 0; y < GameBoard.Size.y; y++)
        {
            var piece = GameBoard.Squares[x, y].PieceOnIt;
            if (piece == null) continue;

            var player = GetPlayerBasedOnColorCode(piece.ColorCode);
            player.AddPiece(piece);
            if (piece is King king) player.MyKing = king;
        }
    }
    public ChessPlayer GetPlayerBasedOnColorCode(string colorCode) => colorCode == "w" ? WhitePlayer : BlackPlayer;
}

public enum EventsManipulationType
{
    Subscribe, Unsubscribe
}

public class TurnOrderEventArgs : EventArgs
{

}