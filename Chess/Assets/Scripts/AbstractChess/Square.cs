using UnityEngine;

namespace AbstractChess
{
    public class Square
    {
        public Chessboard Board { get => _board; set => _board = value; }
        private static Chessboard _board;

        public Piece PieceOnThisSquare;

        public Vector2Int Coordinates => new Vector2Int(_x, _y);
        private readonly int _x, _y;

        public Square(int x, int y, Chessboard board)
        {
            _x = x;
            _y = y;
        
            if (_board == null)
                _board = board;
        }
    
        public Square(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            var pieceText = PieceOnThisSquare == null ? "Empty square" : $"[{PieceOnThisSquare}]";
            return $"[{_x};{_y} {pieceText}]";
        }
    }
}