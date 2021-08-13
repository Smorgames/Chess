using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }

    public PieceColor WhoseTurn { get => _whoseTurn; }
    private PieceColor _whoseTurn;

    public void ChangeTurn()
    {
        if (_whoseTurn == PieceColor.Black)
            _whoseTurn = PieceColor.White;
        else if (_whoseTurn == PieceColor.White)
            _whoseTurn = PieceColor.Black;

        OnTurnChanged?.Invoke(_whoseTurn);
    }

    public delegate void ChangeTurnHandler(PieceColor color);
    public event ChangeTurnHandler OnTurnChanged;
}