using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public delegate void ChangeTurnHandler(PieceColor color);
    public event ChangeTurnHandler OnTurnChanged;

    public PieceColor WhoseTurn { get => _whoseTurn; }
    private PieceColor _whoseTurn;

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
        OnTurnChanged?.Invoke(_whoseTurn);
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