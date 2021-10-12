using UnityEngine;

namespace AbstractChess
{
    public class Token
    {
        public readonly Vector2Int Coordinates;
        public readonly Piece Piece;

        public Token(Vector2Int coordinates, Piece piece)
        {
            Coordinates = coordinates;
            Piece = piece;
        }
    }
}