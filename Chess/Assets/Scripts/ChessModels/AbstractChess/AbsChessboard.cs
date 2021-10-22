using UnityEngine;

namespace AbstractChess
{
    public class AbsChessboard
    {
        public Vector2Int Size => _size;
        private Vector2Int _size;
        
        public AbsSquare[,] Squares => _squares;
        private AbsSquare[,] _squares;
    
        public AbsSquare GhostAbsSquare => _ghostAbsSquare;
        private static AbsSquare _ghostAbsSquare;

        public AbsChessboard(Vector2Int boardSize)
        {
            Constructor(boardSize);
        }

        public AbsChessboard(int length, int height)
        {
            var boardSize = new Vector2Int(length, height);
            Constructor(boardSize);
        }

        private void Constructor(Vector2Int boardSize)
        {
            _size = boardSize;
            _ghostAbsSquare = new AbsSquare(new Vector2Int(-1, -1), this);
            
            _squares = new AbsSquare[_size.x, _size.y];

            for (int x = 0; x < _size.x; x++)
            for (int y = 0; y < _size.y; y++)
                _squares[x, y] = new AbsSquare(new Vector2Int(x, y), this);
        }

        public AbsSquare GetSquareBasedOnCoordinates(Vector2Int coordinates)
        {
            var xCorrect = coordinates.x >= 0 && coordinates.x < _size.x;
            var yCorrect = coordinates.y >= 0 && coordinates.y < _size.y;
    
            if (xCorrect && yCorrect)
                return _squares[coordinates.x, coordinates.y];
    
            return _ghostAbsSquare;
        }
    }
}