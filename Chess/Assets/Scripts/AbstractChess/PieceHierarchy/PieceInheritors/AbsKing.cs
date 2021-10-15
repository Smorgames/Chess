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

        public override List<Square> PossibleMoves(Square square)
        {
            var moves = new List<Square>();
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                moves = SetMoves(MovesType.Movement, square, directions);
            
            return moves;
        }

        private List<Square> SetMoves(MovesType movesType, Square squareWithChessPiece, List<Vector2Int> moveDirections)
        {
            var squares = new List<Square>();

            foreach (var dir in moveDirections)
            {
                var x = squareWithChessPiece.Coordinates.x;
                var y = squareWithChessPiece.Coordinates.y;
                var coordinates = new Vector2Int(x + dir.x, y + dir.y);

                var square = squareWithChessPiece.Board.GetSquareBasedOnCoordinates(coordinates);
                var pieceOnSquare = squareWithChessPiece.AbsPieceOnThisSquare;
                
                var conditionForAddAttackMove = movesType == MovesType.Attack && pieceOnSquare != null && MyColor != pieceOnSquare.MyColor;
                
                if (conditionForAddAttackMove) 
                    squares.Add(square);

                var conditionForAddMoves = movesType == MovesType.Movement && pieceOnSquare == null;
                
                if (conditionForAddMoves)
                    squares.Add(square);
            }

            return squares;
        }

        public override List<Square> PossibleAttackMoves(Square square)
        {
            var moves = new List<Square>();
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                moves = SetMoves(MovesType.Attack, square, directions);
            
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