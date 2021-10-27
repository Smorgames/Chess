using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsQueen : LinearlyMovingAbsPiece
    {        
        public override string TypeCode => "Q";

        public AbsQueen(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove)
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
            var directions = new List<Vector2Int>()
                { UpDirection, DownDirection, LeftDirection, RightDirection, UpRightDirection, UpLeftDirection, DownRightDirection, DownLeftDirection };
            FillAttacksAndMovesSquares(square, directions);
        }

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} queen";
        }
    }
}