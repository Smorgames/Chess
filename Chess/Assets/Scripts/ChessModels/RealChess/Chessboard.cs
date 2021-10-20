using System.Collections.Generic;
using UnityEngine;

public class Chessboard : MonoBehaviour
{
    public Vector2Int Size => _size;
    private Vector2Int _size;
    
    public Square[,] Squares => _squares;
    private Square[,] _squares;

    public Square GhostSquare { get => _ghostSquare; set => _ghostSquare = value; }
    private Square _ghostSquare;

    public void InitializeChessboard(Vector2Int size)
    {
        _size.x = size.x;
        _size.y = size.y;
        
        _squares = new Square[_size.x, _size.y];
    }
    
    public Square GetSquareWithPiece(Piece piece)
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                var square = _squares[x, y];

                if (square.PieceOnIt == piece)
                    return square;
            }
        }

        return _ghostSquare;
    }

    public Square GetSquareWithCoordinates(int x, int y)
    {
        if (x >= 0 && x < _size.x && y >= 0 && y < _size.y)
            return _squares[x, y];
        
        return _ghostSquare;
    }

    public Square GetSquareWithCoordinates(Vector2Int coordinates)
    {
        return GetSquareWithCoordinates(coordinates.x, coordinates.y);
    }

    public void ActivateListOfSquares(List<Square> squares)
    {
        if (squares.Count > 0)
            foreach (Square square in squares)
                square.Activate();
    }

    public void DeactivateAllSquares()
    {
        for (int x = 0; x < _size.x; x++)
        for (int y = 0; y < _size.y; y++)
            _squares[x, y].Deactivate();
    }
}