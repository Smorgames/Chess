using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsRook : AbsLinearlyMovingAbsPiece
    {
        public override PieceType MyType => PieceType.Rook;
        
        public AbsRook(PieceColor color) : base(color) { }

        public override List<AbsSquare> PossibleMoves(AbsSquare absSquare)
        {
            return GetMovesBasedOnActionType(absSquare, ActionType.Movement);
        }

        public override List<AbsSquare> PossibleAttackMoves(AbsSquare absSquare)
        {
            return GetMovesBasedOnActionType(absSquare, ActionType.Attack);
        }
        
        private List<AbsSquare> GetMovesBasedOnActionType(AbsSquare absSquareWithPiece, ActionType actionType)
        {
            var moves = new List<AbsSquare>();
            var directions = new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (var dir in directions)
                IterativelyDirectionallyFillPossibleMoves(moves, absSquareWithPiece, dir, actionType);

            return moves;
        }

        public override string ToString() => $"{MyColor} rook";
    }
}