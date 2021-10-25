using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsQueen : AbsLinearlyMovingAbsPiece
    {        
        private static Vector2Int _upRightDir = new Vector2Int(1, 1);
        private static Vector2Int _upLeftDir = new Vector2Int(-1, 1);
        private static Vector2Int _downRightDir = new Vector2Int(1, -1);
        private static Vector2Int _downLeftDir = new Vector2Int(-1, -1);

        public override string TypeCode => "Q";

        public AbsQueen(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove) { }

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
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                IterativelyDirectionallyFillPossibleMoves(moves, squareWithPiece, dir, actionType);
            
            return moves;
        }
    
        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} queen";
        }
    }
}