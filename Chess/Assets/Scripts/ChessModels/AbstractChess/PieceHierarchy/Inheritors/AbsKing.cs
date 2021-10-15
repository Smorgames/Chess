using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsKing : AbsPiece
    {
        public override PieceType MyType => PieceType.King;
        
        private static Vector2Int _upRightDir = new Vector2Int(1, 1);
        private static Vector2Int _upLeftDir = new Vector2Int(-1, 1);
        private static Vector2Int _downRightDir = new Vector2Int(1, -1);
        private static Vector2Int _downLeftDir = new Vector2Int(-1, -1);

        public AbsKing(PieceColor color) : base(color) { }

        public override List<AbsSquare> PossibleMoves(AbsSquare absSquare)
        {
            var moves = new List<AbsSquare>();
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                moves = SetMoves(MovesType.Movement, absSquare, directions);
            
            return moves;
        }

        private List<AbsSquare> SetMoves(MovesType movesType, AbsSquare absSquareWithChessPiece, List<Vector2Int> moveDirections)
        {
            var squares = new List<AbsSquare>();

            foreach (var dir in moveDirections)
            {
                var x = absSquareWithChessPiece.Coordinates.x;
                var y = absSquareWithChessPiece.Coordinates.y;
                var coordinates = new Vector2Int(x + dir.x, y + dir.y);

                var square = absSquareWithChessPiece.Board.GetSquareBasedOnCoordinates(coordinates);
                var pieceOnSquare = absSquareWithChessPiece.AbsPieceOnThisSquare;
                
                var conditionForAddAttackMove = movesType == MovesType.Attack && pieceOnSquare != null && MyColor != pieceOnSquare.MyColor;
                
                if (conditionForAddAttackMove) 
                    squares.Add(square);

                var conditionForAddMoves = movesType == MovesType.Movement && pieceOnSquare == null;
                
                if (conditionForAddMoves)
                    squares.Add(square);
            }

            return squares;
        }

        public override List<AbsSquare> PossibleAttackMoves(AbsSquare absSquare)
        {
            var moves = new List<AbsSquare>();
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                moves = SetMoves(MovesType.Attack, absSquare, directions);
            
            return moves;
        }
        
        public override string ToString() => $"{MyColor} king";
        
        private enum MovesType
        {
            Attack,
            Movement
        }
    }
}