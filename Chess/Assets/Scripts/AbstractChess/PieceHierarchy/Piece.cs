using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public abstract class Piece
    {
        public Color MyColor => _myColor;
        private Color _myColor;
    
        protected bool _isFirstMove = true;

        public Piece(Color color)
        {
            _myColor = color;
        }
    
        public abstract List<Square> PossibleMoves(Square square);
        public abstract List<Square> PossibleAttackMoves(Square square);

        public enum Color
        {
            None,
            White,
            Black
        }
    }
    
    public interface IMoveDirection
    {
        Vector2Int MoveDirection { get; }
    }
}