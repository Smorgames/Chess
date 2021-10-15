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

        public override List<Square> PossibleMoves(Square square)
        {
            return GetMovesBasedOnActionType(square, PieceAction.Movement);
        }

        public override List<Square> PossibleAttackMoves(Square square)
        {
            return GetMovesBasedOnActionType(square, PieceAction.Attack);
        }

        private List<Square> GetMovesBasedOnActionType(Square squareWithPiece, PieceAction actionType)
        {
            var moves = new List<Square>();
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                IterativelyDirectionallyFillPossibleMoves(moves, squareWithPiece, dir, actionType);

            return moves;
        }
    
        public override string ToString() => $"{MyColor} queen";
    }
}