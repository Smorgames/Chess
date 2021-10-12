using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void ChangeTurnHandler();
    public event ChangeTurnHandler OnTurnChanged;

    public Piece.Color WhoseTurn { get => _whoseTurn; }
    private Piece.Color _whoseTurn;
    
    [Header("Chessboards")]
    [SerializeField] private Chessboard _realChessboard;
    [SerializeField] private Chessboard _abstractChessboard;
    
    [Header("Handlers and managers")]
    [SerializeField] private ChessboardFiller _filler;
    [SerializeField] private PiecesStorage _pieceStorage;
    [SerializeField] private SquareHandler _squareHandler;

    [SerializeField] private Piece.Color _whoseTurnFirst;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        _filler.InitializeChessboard(_realChessboard);
        _filler.InitializeChessboard(_abstractChessboard);
        
        SubscribeMethodsOnEvent();
        SetWhichColorTurnFirst(_whoseTurnFirst);
    }

    private void SetWhichColorTurnFirst(Piece.Color color)
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
                Piece pieceOnSquare = square.PieceOnThis;

                if (pieceOnSquare.GetType() == typeof(King))
                {
                    Test.DebugTestInfo($"{piece.name} check {pieceOnSquare.name}");
                    Test.Instance.ConnectTwoPieceWithLine(piece, pieceOnSquare);
                    return (King)pieceOnSquare;
                }
            }
        }

        Test.Instance.ResetLine();
        return null;
    }

    private void ChangeColorOrderOfTurn()
    {
        if (_whoseTurn == Piece.Color.Black)
            _whoseTurn = Piece.Color.White;
        else if (_whoseTurn == Piece.Color.White)
            _whoseTurn = Piece.Color.Black;
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

/*
public void VerifyIfIsCheck()
    {
        List<Piece> allPieces = _pieceStorage.AllPieces;

        List<Square> allWhitePossibleAttackTurns = new List<Square>();
        List<Square> allBlackPossibleAttackTurns = new List<Square>();

        foreach (var piece in allPieces)
        {
            Square squareWithPiece = _squareHandler.GetSquareWithPiece(piece);
            List<Square> pieceAttackTurns = new List<Square>();

            if (piece.GetType() == typeof(Rook) || piece.GetType() == typeof(Bishop) || piece.GetType() == typeof(Queen))
                piece.GetPossibleMoveTurns(squareWithPiece);

            pieceAttackTurns = piece.GetPossibleAttackTurns(squareWithPiece);

            if (piece.ColorData.Color == PieceColor.White)
                allWhitePossibleAttackTurns.AddRange(pieceAttackTurns);
            else
                allBlackPossibleAttackTurns.AddRange(pieceAttackTurns);
        }

        foreach (var square in allWhitePossibleAttackTurns)
        {
            Piece pieceOnSquare = square.PieceOnThis;

            if (pieceOnSquare.GetType() == typeof(King))
                Debug.Log("Check for black");
        }

        foreach (var square in allBlackPossibleAttackTurns)
        {
            Piece pieceOnSquare = square.PieceOnThis;

            if (pieceOnSquare.GetType() == typeof(King))
                Debug.Log("Check for white");
        }
    } 
*/