using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void ChangeTurnHandler();
    public event ChangeTurnHandler OnTurnChanged;

    public PieceColor WhoseTurn { get => _whoseTurn; }
    private PieceColor _whoseTurn;

    [SerializeField] private PiecesStorage _pieceStorage;
    [SerializeField] private SquareHandler _squareHandler;

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
        VerifyIfIsCheck();
        OnTurnChanged?.Invoke();
    }

    private void ChangeColorOrderOfTurn()
    {
        if (_whoseTurn == PieceColor.Black)
            _whoseTurn = PieceColor.White;
        else if (_whoseTurn == PieceColor.White)
            _whoseTurn = PieceColor.Black;
    }

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
            {
                piece.GetPossibleMoveTurns(squareWithPiece);
                pieceAttackTurns = piece.GetPossibleAttackTurns(squareWithPiece);
            }

            pieceAttackTurns = piece.GetPossibleAttackTurns(squareWithPiece);

            if (piece.ColorData.Color == PieceColor.White)
                allWhitePossibleAttackTurns.AddRange(pieceAttackTurns);
            else
                allBlackPossibleAttackTurns.AddRange(pieceAttackTurns);
        }

        foreach (var square in allWhitePossibleAttackTurns)
        {
            Piece piece = square.PieceOnThis;

            if (piece.GetType() == typeof(King))
                Debug.Log("Check for black");
        }

        foreach (var square in allBlackPossibleAttackTurns)
        {
            Piece piece = square.PieceOnThis;

            if (piece.GetType() == typeof(King))
                Debug.Log("Check for white");
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