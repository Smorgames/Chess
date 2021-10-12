using System.Text;
using UnityEngine;

namespace AbstractChess
{
    public class Chessboard
    {
        public int Length => _length;
        private readonly int _length;
    
        public int Height => _height;
        private readonly int _height;
    
        public Square[,] Squares => _squares;
        private readonly Square[,] _squares;
    
        public Square GhostSquare => _ghostSquare;
        private static Square _ghostSquare;
    
        public Chessboard(int length, int height, Square[,] squares)
        {
            _length = length;
            _height = height;
    
            if (_ghostSquare == null)
                _ghostSquare = new Square(-1, -1, this) { Board = this };
            
            _squares = new Square[length, height];
            _squares = squares;
        }
    
        public Square GetSquareBasedOnCoordinates(Vector2Int coordinates)
        {
            var xCorrect = coordinates.x >= 0 && coordinates.x < _length;
            var yCorrect = coordinates.y >= 0 && coordinates.y < _height;
    
            if (xCorrect && yCorrect)
                return _squares[coordinates.x, coordinates.y];
    
            var className = nameof(Chessboard);
            var methodName = nameof(GetSquareBasedOnCoordinates);
            var ghostSquareName = nameof(GhostSquare);
            
            Debug.Log($"In method [{className}.{methodName}()] coordinates are equal ({coordinates.x};{coordinates.y})! Returned {ghostSquareName}");
            return _ghostSquare;
        }
    
        public void TestLogAboutChessboard()
        {
            var stringBuilder = new StringBuilder();
    
            for (int y = _height - 1; y >= 0; y--)
            {
                stringBuilder.Clear();
    
                for (int x = 0; x < _length; x++)
                    stringBuilder.Append($"{_squares[x,y]} ");
    
                Debug.Log(stringBuilder.ToString());
            }
        }
    }
}