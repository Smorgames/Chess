using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsRook : AbsLinearlyMovingAbsPiece
    {
        public override string TypeCode => "r";

        public AbsRook(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove) { }

        public override List<IRealSquare> GetMoves(IRealSquare square)
        {
            return GetMovesBasedOnActionType(square, ActionType.Movement);
        }

        public override List<IRealSquare> GetAttacks(IRealSquare square)
        {
            return GetMovesBasedOnActionType(square, ActionType.Attack);
        }
        
        private List<IRealSquare> GetMovesBasedOnActionType(IRealSquare squareWithPiece, ActionType actionType)
        {
            var moves = new List<IRealSquare>();
            var directions = new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (var dir in directions)
                IterativelyDirectionallyFillPossibleMoves(moves, squareWithPiece, dir, actionType);

            return moves;
        }

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} rook";
        }
    }
}