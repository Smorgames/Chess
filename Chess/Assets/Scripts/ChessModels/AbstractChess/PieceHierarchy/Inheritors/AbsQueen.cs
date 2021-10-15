using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsQueen : AbsLinearlyMovingAbsPiece
    {
        public override PieceType MyType => PieceType.Queen;
        
        private static Vector2Int _upRightDir = new Vector2Int(1, 1);
        private static Vector2Int _upLeftDir = new Vector2Int(-1, 1);
        private static Vector2Int _downRightDir = new Vector2Int(1, -1);
        private static Vector2Int _downLeftDir = new Vector2Int(-1, -1);

        public AbsQueen(PieceColor color) : base(color) { }

        public override List<AbsSquare> PossibleMoves(AbsSquare absSquare)
        {
            return GetMovesBasedOnActionType(absSquare, PieceAction.Movement);
        }

        public override List<AbsSquare> PossibleAttackMoves(AbsSquare absSquare)
        {
            return GetMovesBasedOnActionType(absSquare, PieceAction.Attack);
        }

        private List<AbsSquare> GetMovesBasedOnActionType(AbsSquare absSquareWithPiece, PieceAction actionType)
        {
            var moves = new List<AbsSquare>();
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                IterativelyDirectionallyFillPossibleMoves(moves, absSquareWithPiece, dir, actionType);

            return moves;
        }
    
        public override string ToString() => $"{MyColor} queen";
    }
}