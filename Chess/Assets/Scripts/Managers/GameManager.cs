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
    
    [Header("Chessboards")]
    [SerializeField] private Chessboard _chessboard;

    public AbsChessboard AbstractBoard => _abstractBoard;
    private AbsChessboard _abstractBoard;

    [Header("Handlers and managers")]
    [SerializeField] private ChessboardFiller _filler;
    [SerializeField] private PiecesStorage _pieceStorage;
    [SerializeField] private SquareHandler _squareHandler;

    [SerializeField] private PieceColor _whoseTurnFirst;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _filler.InitializeChessboard(_chessboard);
        _abstractBoard = new AbsChessboard(_chessboard.Length, _chessboard.Length);
        
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
        VerifyIfIsCheck();
    }

    public King VerifyIfIsCheck()
    {
        List<Piece> allPieces = _pieceStorage.AllPieces;
        Dictionary<Piece, List<Square>> piecesAndTheirAttackTurns = new Dictionary<Piece, List<Square>>();

        foreach (var piece in allPieces)
        {
            Square squareWithPiece = _squareHandler.GetSquareWithPiece(piece);
            List<Square> attackTurns = new List<Square>();

            if (piece.GetType() == typeof(Rook) || piece.GetType() == typeof(Bishop) || piece.GetType() == typeof(Queen))
                piece.GetPossibleMoveTurns(squareWithPiece);

            attackTurns = piece.GetPossibleAttackTurns(squareWithPiece);

            piecesAndTheirAttackTurns.Add(piece, attackTurns);
        }

        foreach (var piece in piecesAndTheirAttackTurns.Keys)
        {
            foreach (var square in piecesAndTheirAttackTurns[piece])
            {
                Piece pieceOnSquare = square.PieceOnSquare;

                if (pieceOnSquare.GetType() == typeof(King))
                {
                    Test.DebugTestInfo($"{piece.name} check {pieceOnSquare.name}");
                    Test.Instance.ConnectTwoPieceWithLine(piece, pieceOnSquare);
                    return (King)pieceOnSquare;
                }
            }
        }

        //Test.Instance.ResetLine();
        return null;
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