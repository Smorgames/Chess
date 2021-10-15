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
    
        private List<Square> _attackMoves = new List<Square>();
    
        public AbsBishop(PieceColor color) : base(color) { }

        public override List<Square> PossibleMoves(Square square)
        {
            var moves = GetPossibleMoves(square, PieceAction.Movement);
            return moves;
        }

        public override List<Square> PossibleAttackMoves(Square square)
        {
            var moves = GetPossibleMoves(square, PieceAction.Attack);
            return moves;
        }

        private List<Square> GetPossibleMoves(Square pieceSquare, PieceAction actionType)
        {
            var possibleMoves = new List<Square>();
            var directions = new List<Vector2Int>() { _upRightDir, _upLeftDir, _downLeftDir, _downRightDir };

            foreach (var dir in directions)
                IterativelyDirectionallyFillPossibleMoves(possibleMoves, pieceSquare, dir, actionType);

            return possibleMoves;
        }
    
        public override string ToString() => $"{MyColor} bishop";
    }
}