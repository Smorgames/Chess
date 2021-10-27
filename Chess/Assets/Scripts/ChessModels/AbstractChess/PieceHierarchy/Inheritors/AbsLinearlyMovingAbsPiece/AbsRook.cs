using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsRook : LinearlyMovingAbsPiece
    {
        public override string TypeCode => "r";

        public AbsRook(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove)
        {
            
        }

        public override List<ISquare> GetAttacks(ISquare square)
        {
            FillAttacksAndMoves(square);
            return Attacks;
        }
    
        public override List<ISquare> GetMoves(ISquare square)
        {
            FillAttacksAndMoves(square);
            return Moves;
        }

        private void FillAttacksAndMoves(ISquare square)
        {
            var directions = new List<Vector2Int>() { UpDirection, DownDirection, LeftDirection, RightDirection };
            FillAttacksAndMovesSquares(square, directions);
        }

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} rook";
        }
    }
}