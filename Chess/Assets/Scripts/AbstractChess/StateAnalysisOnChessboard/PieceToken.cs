using UnityEngine;

namespace AbstractChess
{
    public class PieceToken
    {
        public readonly Vector2Int Coordinates;
        public readonly Piece Piece;

        public PieceToken(Vector2Int coordinates, Piece piece)
        {
            Coordinates = coordinates;
            Piece = piece;
        }
    }
}