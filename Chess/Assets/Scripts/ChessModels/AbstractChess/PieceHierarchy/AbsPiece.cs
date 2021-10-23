using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public abstract class AbsPiece
    {
        public readonly string ColorCode;

        public readonly PieceColor MyColor;
        public abstract PieceType MyType { get; }
    
        public bool IsFirstMove = true;

        public AbsPiece(PieceColor color)
        {
            MyColor = color;
            ColorCode = color == PieceColor.White ? "w" : "b";
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