using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsBishop : AbsLinearlyMovingAbsPiece
    {
        public override PieceType MyType => PieceType.Bishop;
        
        private static Vector2Int _upRightDir = new Vector2Int(1, 1);
        private static Vector2Int _upLeftDir = new Vector2Int(-1, 1);
        private static Vector2Int _downRightDir = new Vector2Int(1, -1);
        private static Vector2Int _downLeftDir = new Vector2Int(-1, -1);
    
        private List<AbsSquare> _attackMoves = new List<AbsSquare>();
    
        public AbsBishop(PieceColor color) : base(color) { }

        public override List<AbsSquare> PossibleMoves(AbsSquare absSquare)
        {
            var moves = GetPossibleMoves(absSquare, ActionType.Movement);
            return moves;
        }

        public override List<AbsSquare> PossibleAttackMoves(AbsSquare absSquare)
        {
            var moves = GetPossibleMoves(absSquare, ActionType.Attack);
            return moves;
        }

        private List<AbsSquare> GetPossibleMoves(AbsSquare pieceAbsSquare, ActionType actionType)
        {
            var possibleMoves = new List<AbsSquare>();
            var directions = new List<Vector2Int>() { _upRightDir, _upLeftDir, _downLeftDir, _downRightDir };

            foreach (var dir in directions)
                IterativelyDirectionallyFillPossibleMoves(possibleMoves, pieceAbsSquare, dir, actionType);

            return possibleMoves;
        }
    
        public override string ToString() => $"{MyColor} bishop";
    }
}