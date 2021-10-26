using UnityEngine;

namespace AbstractChess
{
    public class AbsChessBoard : IChessBoard
    {
        public Vector2Int Size => _size;
        private Vector2Int _size;

        public ISquare[,] Squares
        {
            get
            {
                if (_squares != null) return _squares;
                
                var length = _absSquares.GetLength(0);
                var height = _absSquares.GetLength(1);
                _squares = new ISquare[length, height];

                for (int x = 0; x < length; x++)
                for (int y = 0; y < height; y++)
                    _squares[x, y] = _absSquares[x, y];

                return _squares;
            }
        }
        private ISquare[,] _squares;

        public AbsSquare[,] AbsSquares => _absSquares;
        private AbsSquare[,] _absSquares;
    
        public ISquare GhostSquare { get => _ghostSquare; set => _ghostSquare = value; }
        private static ISquare _ghostSquare;

        public string WhoseTurn { get; set; }

        public AbsChessBoard(Vector2Int boardSize)
        {
            Constructor(boardSize);
        }

        public AbsChessBoard(int length, int height)
        {
            var boardSize = new Vector2Int(length, height);
            Constructor(boardSize);
        }

        private void Constructor(Vector2Int boardSize)
        {
            _size = boardSize;
            _ghostSquare = new AbsSquare(new Vector2Int(-1, -1), this);
            
            _absSquares = new AbsSquare[_size.x, _size.y];

            for (int x = 0; x < _size.x; x++)
            for (int y = 0; y < _size.y; y++)
                _absSquares[x, y] = new AbsSquare(new Vector2Int(x, y), this);
        }

        public ISquare SquareWithCoordinates(Vector2Int coordinates)
        {
            var xCorrect = coordinates.x >= 0 && coordinates.x < _size.x;
            var yCorrect = coordinates.y >= 0 && coordinates.y < _size.y;
    
            if (xCorrect && yCorrect)
                return _absSquares[coordinates.x, coordinates.y];
    
            return _ghostSquare;
        }

        public ISquare SquareWithCoordinates(int x, int y)
        {
            var coordinates = new Vector2Int(x, y);
            return SquareWithCoordinates(coordinates);
        }
    }
}