using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly ChessCode _startChessCode = new ChessCode(
        "0;0;w;r;1_1;0;w;k;1_2;0;w;b;1_3;0;w;Q;1_4;0;w;K;1_5;0;w;b;1_6;0;w;k;1_7;0;w;r;1_" +
        "0;1;w;p;1_1;1;w;p;1_2;1;w;p;1_3;1;w;p;1_4;1;w;p;1_5;1;w;p;1_6;1;w;p;1_7;1;w;p;1_" +
        "0;7;b;r;1_1;7;b;k;1_2;7;b;b;1_3;7;b;Q;1_4;7;b;K;1_5;7;b;b;1_6;7;b;k;1_7;7;b;r;1_" +
        "0;6;b;p;1_1;6;b;p;1_2;6;b;p;1_3;6;b;p;1_4;6;b;p;1_5;6;b;p;1_6;6;b;p;1_7;6;b;p;1_",
        "8;8", 
        "w");
    public static GameManager Instance;

    public delegate void ChangeTurnHandler();
    public event ChangeTurnHandler OnTurnChanged;

    public string WhoseTurn { get => _whoseTurn; }
    private string _whoseTurn = "w";

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        var board = ChessboardBuilder.BuildStandardChessboard();
        //Analyzer.RecreatePiecesFromChessCodeOnRealBoard(_startChessCode, board);
        SubscribeMethodsOnEvent();
    }

    public void TriggerChangeTurn()
    {
        ChangeColorOrderOfTurn();
    }

    private void ChangeColorOrderOfTurn()
    {
        var whoseTurn = _whoseTurn == "b" ? "w" : "b";
        _whoseTurn = whoseTurn;
    }

    private void OnDestroy()
    {
        UnsubscribeMethodsOnEvent();
    }

    private void SubscribeMethodsOnEvent()
    {
        RealPiece.OnPieceMoved += TriggerChangeTurn;
    }

    private void UnsubscribeMethodsOnEvent()
    {
        RealPiece.OnPieceMoved -= TriggerChangeTurn;
    }
}