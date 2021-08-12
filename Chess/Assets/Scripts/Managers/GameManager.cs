using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;    
    }

    public PieceColor WhoseTurn { get => _whoseTurn; }
    private PieceColor _whoseTurn;

    public void ChangeTurn()
    {
        if (_whoseTurn == PieceColor.Black)
            _whoseTurn = PieceColor.White;
        else if (_whoseTurn == PieceColor.White)
            _whoseTurn = PieceColor.Black;
    }

    public delegate void ChangeTurnHandler(bool isBlackTurn);
    public static event ChangeTurnHandler OnTurnChangedToBlack;
}