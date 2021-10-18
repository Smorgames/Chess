using UnityEngine;
using System.Collections.Generic;

public class SquareHandler : MonoBehaviour
{
    public static SquareHandler Instance;

    public Square GhostSquare { get => _ghostSquare; }
    [SerializeField] private Square _ghostSquare;

    public Chessboard Chessboard => _chessboard;
    [SerializeField] private Chessboard _chessboard;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}