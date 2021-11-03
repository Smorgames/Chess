using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static EventHandler<TurnOrderEventArgs> OnTurnOrderChanged;

    private enum GameState { Initialization, Playing, Ended }
    private GameState _currentState;

    public string WhoseTurn { get; private set; }
    public ChessPlayer WhitePlayer { get; private set; } = new ChessPlayer("w");
    public ChessPlayer BlackPlayer { get; private set; } = new ChessPlayer("b");
    private ChessPlayer[] _players = new ChessPlayer[2];

    public Board GameBoard => _gameBoard;
    private Board _gameBoard;

    #region Singleton

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    #endregion

    #region Initialization and events

    private void Start()
    {
        Initialization();
    }

    private void Initialization()
    {
        _currentState = GameState.Initialization;
        _players[0] = WhitePlayer;
        _players[1] = BlackPlayer;
        EventSubscription();
        _gameBoard = ChessboardBuilder.BuildStandardChessboard();
        var pieceSignatures = ChessCodeHandler.GetPieceSignaturesFromChessCode(UsefulChessCodes.StartChessState);
        GameSetuper.ArrangePiecesOnBoard(pieceSignatures, _gameBoard);
        // After pieces had been arranged on board, GameSetuper triggers event OnPiecesArranged and GameManager raises PieceArranged function 
    }
    
    private void OnDestroy()
    {
        EventUnsubscription();
    }

    private void EventSubscription()
    {
        Piece.OnPieceMoved += PieceMoved;
        GameSetuper.OnPiecesArranged += PiecesArranged;
    }

    private void EventUnsubscription()
    {
        Piece.OnPieceMoved -= PieceMoved;
        GameSetuper.OnPiecesArranged -= PiecesArranged;
    }

    #endregion
    
    private void PieceMoved(object sender, PieceMovedEventArgs e) => ChangeTurnOrder();
    private void ChangeTurnOrder()
    {
        WhoseTurn = WhoseTurn == "w" ? "b" : "w";
        foreach (var player in _players) player.UpdatePiecesSupposedMoves();
        if (WhoseTurn == "w")
        {
            WhitePlayer.SetPiecesTransparency(1f);
            BlackPlayer.SetPiecesTransparency(0.5f);
        }
        else
        {
            WhitePlayer.SetPiecesTransparency(0.5f);
            BlackPlayer.SetPiecesTransparency(1f);
        }
        var gameEnd = CheckMateHandler.MateForKing(_gameBoard, WhoseTurn);
        if (gameEnd)
        {
            var whoWin = WhoseTurn == "w" ? "Black" : "White";
            Debug.Log($"{whoWin} player won this chess game!");
        }

        if (WhoseTurn == "w")
        {
            foreach (var piece in WhitePlayer.PlayerPieces)
            {
                var pieceSquare = GameManager.Instance.GameBoard.SquareWithPiece(piece);
                var possibleMoves = CheckMateHandler.MovesWithoutCheckForKing(pieceSquare, piece.SupposedMoves);

                if (possibleMoves.Count <= 0)
                {
                    var color = piece.Renderer.color;
                    color.a = 0.5f;
                    piece.Renderer.color = color;
                }
            }
        }
        else
        {
            foreach (var piece in BlackPlayer.PlayerPieces)
            {
                var pieceSquare = GameManager.Instance.GameBoard.SquareWithPiece(piece);
                var possibleMoves = CheckMateHandler.MovesWithoutCheckForKing(pieceSquare, piece.SupposedMoves);

                if (possibleMoves.Count <= 0)
                {
                    var color = piece.Renderer.color;
                    color.a = 0.5f;
                    piece.Renderer.color = color;
                }
            }
        }
    }
    
    private void PiecesArranged(object sender, PiecesArrangedArgs e)
    {
        FillPlayersPieces();
        foreach (var player in _players) player.UpdatePiecesSupposedMoves();
        WhoseTurn = "b";
        ChangeTurnOrder();
        _currentState = GameState.Playing;
    }
    private void FillPlayersPieces()
    {
        for (var x = 0; x < _gameBoard.Size.x; x++)
        for (var y = 0; y < _gameBoard.Size.y; y++)
        {
            var piece = _gameBoard.Squares[x, y].PieceOnIt;
            if (piece == null) continue;
        
            if (piece.ColorCode == "w") WhitePlayer.AddPiece(piece);
            else BlackPlayer.AddPiece(piece);
        }
    }

    public void TriggerUpdateSupposedMoves() => OnTurnOrderChanged?.Invoke(this, new TurnOrderEventArgs());
}

public class TurnOrderEventArgs : EventArgs
{

}