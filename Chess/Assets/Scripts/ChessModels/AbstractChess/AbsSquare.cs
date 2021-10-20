using UnityEngine;

namespace AbstractChess
{
    public class AbsSquare
    {
        public AbsPiece AbsPieceOnIt { get; set; }

        public AbsChessboard Board => _board;
        private readonly AbsChessboard _board;
        
        public Vector2Int Coordinates => _coordinates;
        private readonly Vector2Int _coordinates;

        public AbsSquare(Vector2Int coordinates, AbsChessboard board)
        {
            _coordinates = coordinates;
            _board = board;
        }

        public override string ToString() => $"[{_coordinates.x};{_coordinates.y} {AbsPieceOnIt}]";
    }
}