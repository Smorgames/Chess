using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsBishop : AbsLinearlyMovingAbsPiece
    {        
        private static Vector2Int _upRightDir = new Vector2Int(1, 1);
        private static Vector2Int _upLeftDir = new Vector2Int(-1, 1);
        private static Vector2Int _downRightDir = new Vector2Int(1, -1);
        private static Vector2Int _downLeftDir = new Vector2Int(-1, -1);
    
        private List<AbsSquare> _attackMoves = new List<AbsSquare>();

        public override string TypeCode => "b";

        public AbsBishop(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove) { }

        public override List<ISquare> GetMoves(ISquare square)
        {
            var moves = GetPossibleMoves(square, ActionType.Movement);
            return moves;
        }

        public override List<ISquare> GetAttacks(ISquare square)
        {
            var moves = GetPossibleMoves(square, ActionType.Attack);
            return moves;
        }

        private List<ISquare> GetPossibleMoves(ISquare pieceAbsSquare, ActionType actionType)
        {
            var possibleMoves = new List<ISquare>();
            var directions = new List<Vector2Int>() { _upRightDir, _upLeftDir, _downLeftDir, _downRightDir };

            foreach (var dir in directions)
                IterativelyDirectionallyFillPossibleMoves(possibleMoves, pieceAbsSquare, dir, actionType);

            return possibleMoves;
        }

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} bishop";
        }
    }
}