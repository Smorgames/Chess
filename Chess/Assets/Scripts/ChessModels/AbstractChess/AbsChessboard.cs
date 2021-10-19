using UnityEngine;

namespace AbstractChess
{
    public class AbsChessboard
    {
        public int Length => _length;
        private int _length;
    
        public int Height => _height;
        private int _height;
    
        public AbsSquare[,] Squares => _squares;
        private AbsSquare[,] _squares;
    
        public AbsSquare GhostAbsSquare => _ghostAbsSquare;
        private static AbsSquare _ghostAbsSquare;
    
        public AbsChessboard(int length, int height)
        {
            ConstructorInitialization(length, height);
        }

        public AbsChessboard(Vector2Int boardSize)
        {
            ConstructorInitialization(boardSize.x, boardSize.y);
        }

        private void ConstructorInitialization(int length, int height)
        {
            _length = length;
            _height = height;
    
            if (_ghostAbsSquare == null)
                _ghostAbsSquare = new AbsSquare(-1, -1, this) { Board = this };
            
            _squares = new AbsSquare[length, height];

            for (int x = 0; x < length; x++)
            for (int y = 0; y < height; y++)
                _squares[x, y] = new AbsSquare(x, y, this);
        }

        public AbsSquare GetSquareBasedOnCoordinates(Vector2Int coordinates)
        {
            var xCorrect = coordinates.x >= 0 && coordinates.x < _length;
            var yCorrect = coordinates.y >= 0 && coordinates.y < _height;
    
            if (xCorrect && yCorrect)
                return _squares[coordinates.x, coordinates.y];
    
            // var className = nameof(Chessboard);
            // var methodName = nameof(GetSquareBasedOnCoordinates);
            // var ghostSquareName = nameof(GhostSquare);
            
            //Debug.Log($"In method [{className}.{methodName}()] coordinates are equal ({coordinates.x};{coordinates.y})! Returned {ghostSquareName}");
            return _ghostAbsSquare;
        }
    }
}