using UnityEngine;
using System.Collections.Generic;

public class SquareHandler : MonoBehaviour
{
    public static SquareHandler Instance;

    public Square GhostSquare { get => _ghostSquare; }
    [SerializeField] private Square _ghostSquare;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public Chessboard Board { get => _chessboard; }
    [SerializeField] private Chessboard _chessboard;

    public Square GetSquareWithPiece(Piece piece)
    {
        for (int x = 0; x < _chessboard.Length; x++)
        {
            for (int y = 0; y < _chessboard.Height; y++)
            {
                Square square = _chessboard.Squares[x, y];

                if (square.PieceOnThis == piece)
                    return square;
            }
        }

        return _ghostSquare;
    }

    public Square GetSquareWithCoordinates(int x, int y)
    {
        if (x >= 0 && x < _chessboard.Length && y >= 0 && y < _chessboard.Height)
            return _chessboard.Squares[x, y];
        else
            return _ghostSquare;
    }

    public void ActivateListOfSquares(List<Square> squares)
    {
        if (squares.Count > 0)
            foreach (Square square in squares)
                square.Activate();
    }

    public void DeactivateAllSquares()
    {
        for (int x = 0; x < _chessboard.Length; x++)
            for (int y = 0; y < _chessboard.Height; y++)
                if (_chessboard.Squares[x, y].IsActive)
                    _chessboard.Squares[x, y].Deactivate();
    }
}