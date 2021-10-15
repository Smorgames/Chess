using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public abstract class AbsPiece
    {
        public readonly PieceColor MyColor;
        public abstract PieceType MyType { get; }
    
        protected bool _isFirstMove = true;

        public AbsPiece(PieceColor color)
        {
            MyColor = color;
        }
    
        public abstract List<AbsSquare> PossibleMoves(AbsSquare absSquare);
        public abstract List<AbsSquare> PossibleAttackMoves(AbsSquare absSquare);

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