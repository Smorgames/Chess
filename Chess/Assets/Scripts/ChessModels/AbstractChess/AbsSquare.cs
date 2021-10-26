using UnityEngine;

namespace AbstractChess
{
    public class AbsSquare : ISquare
    {
        public bool IsGhost { get; }
        public IPiece PieceOnIt => AbsPieceOnIt;
        public AbsPiece AbsPieceOnIt { get; set; }

        public IChessBoard Board => _board;
        private readonly AbsChessBoard _board;
        
        public Vector2Int Coordinates { get; private set; }

        public AbsSquare(Vector2Int coordinates, AbsChessBoard board)
        {
            Coordinates = coordinates;
            _board = board;
        }

        public override string ToString() => $"[{Coordinates.x};{Coordinates.y} {PieceOnIt}]";
    }
}