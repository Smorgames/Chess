using UnityEngine;

namespace AbstractChess
{
    public class Chessboard
    {
        public int Length => _length;
        private int _length;
    
        public int Height => _height;
        private int _height;
    
        public Square[,] Squares => _squares;
        private Square[,] _squares;
    
        public Square GhostSquare => _ghostSquare;
        private static Square _ghostSquare;
    
        public Chessboard(int length, int height)
        {
            ConstructorInitialization(length, height);
        }

        public Chessboard(Vector2Int boardSize)
        {
            ConstructorInitialization(boardSize.x, boardSize.y);
        }

        private void ConstructorInitialization(int length, int height)
        {
            _length = length;
            _height = height;
    
            if (_ghostSquare == null)
                _ghostSquare = new Square(-1, -1, this) { Board = this };
            
            _squares = new Square[length, height];

            for (int x = 0; x < length; x++)
            for (int y = 0; y < height; y++)
                _squares[x, y] = new Square(x, y);
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
    }
}