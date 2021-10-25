using System.Collections.Generic;
using UnityEngine;

public class Chessboard : MonoBehaviour, IRealChessBoard
{
    public Vector2Int Size => _size;
    public ISquare[,] Squares => RealSquares;

    private Vector2Int _size;

    public IRealSquare[,] RealSquares => _squares;
    private Square[,] _squares;

    public IRealSquare RealGhostSquare { get => _ghostSquare; set => _ghostSquare = value; }
    public ISquare GhostSquare => _ghostSquare;
    private IRealSquare _ghostSquare;

    public string WhoseTurn { get; set; }

    public void InitializeChessboard(Vector2Int size)
    {
        _size.x = size.x;
        _size.y = size.y;
        
        _squares = new Square[_size.x, _size.y];
    }

    public IRealSquare GetRealSquareWithPiece(IRealPiece realPiece)
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                var square = _squares[x, y];

                if (square.RealPieceOnIt == realPiece)
                    return square;
            }
        }

        return _ghostSquare;
    }

    public IRealSquare GetRealSquareWithCoordinates(int x, int y)
    {
        if (x >= 0 && x < _size.x && y >= 0 && y < _size.y)
            return _squares[x, y];
        
        return _ghostSquare;
    }

    public IRealSquare GetRealSquareWithCoordinates(Vector2Int coordinates)
    {
        return GetRealSquareWithCoordinates(coordinates.x, coordinates.y);
    }

    public ISquare GetSquareWithCoordinates(int x, int y)
    {
        return GetRealSquareWithCoordinates(x, y);
    }

    public ISquare GetSquareWithCoordinates(Vector2Int coordinates)
    {
        return GetSquareWithCoordinates(coordinates.x, coordinates.y);
    }

    public List<IHighlightable> HighlightableSquares()
    {
        var highlightableSquares = new List<IHighlightable>();

        for (int x = 0; x < _size.x; x++)
        for (int y = 0; y < _size.y; y++)
            highlightableSquares.Add(_squares[x, y]);

        return highlightableSquares;
    }
}