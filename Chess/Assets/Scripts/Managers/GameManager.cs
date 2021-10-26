using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        var board = ChessboardBuilder.BuildMainChessboard();
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