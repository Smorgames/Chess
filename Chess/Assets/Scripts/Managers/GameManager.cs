using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;    
    }

    public bool IsBlackTurn { get => _isBlackTurn; set => _isBlackTurn = value; }
    private bool _isBlackTurn = true;

    public delegate void ChangeTurnHandler(bool isBlackTurn);
    public static event ChangeTurnHandler OnTurnChangedToBlack;

    public void IsNowBlackTurn(bool isBlackTurn)
    {
        _isBlackTurn = isBlackTurn;
        OnTurnChangedToBlack?.Invoke(_isBlackTurn);
    }
}