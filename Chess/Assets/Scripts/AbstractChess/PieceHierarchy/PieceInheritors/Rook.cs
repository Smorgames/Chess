using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class Rook : LinearlyMovingPiece
    {
        public Rook(Color color) : base(color) { }

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
            var directions = new List<Vector2Int>() { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (var dir in directions)
                IterativelyDirectionallyFillPossibleMoves(moves, squareWithPiece, dir, actionType);

            return moves;
        }

        public override string ToString() => $"{MyColor} rook";
    }
}