using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsKing : AbsPiece
    {       
        private static Vector2Int _upRightDir = new Vector2Int(1, 1);
        private static Vector2Int _upLeftDir = new Vector2Int(-1, 1);
        private static Vector2Int _downRightDir = new Vector2Int(1, -1);
        private static Vector2Int _downLeftDir = new Vector2Int(-1, -1);

        public override string TypeCode => "K";

        public AbsKing(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove) { }

        public override List<IAbsSquare> GetMoves(ISquare square)
        {
            var supposedMoves = new List<IAbsSquare>();
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                supposedMoves = SetMoves(MovesType.Movement, square, directions);
            
            return supposedMoves;
        }

        private List<IAbsSquare> SetMoves(MovesType movesType, ISquare squareWithPiece, List<Vector2Int> moveDirections)
        {
            var squares = new List<IAbsSquare>();

            foreach (var dir in moveDirections)
            {
                var x = squareWithPiece.Coordinates.x;
                var y = squareWithPiece.Coordinates.y;
                var coordinates = new Vector2Int(x + dir.x, y + dir.y);

                var square = squareWithPiece.Board.GetSquareWithCoordinates(coordinates);
                var pieceOnSquare = squareWithPiece.PieceOnIt;
                
                var conditionForAddAttackMove = movesType == MovesType.Attack && pieceOnSquare != null && ColorCode != pieceOnSquare.ColorCode;
                
                if (conditionForAddAttackMove) 
                    squares.Add(square);

                var conditionForAddMoves = movesType == MovesType.Movement && pieceOnSquare == null;
                
                if (conditionForAddMoves)
                    squares.Add(square);
            }

            return squares;
        }

        public override List<IRealSquare> GetAttacks(IRealSquare square)
        {
            var supposeMoves = new List<IRealSquare>();
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                supposeMoves = SetMoves(MovesType.Attack, square, directions);
            
            return supposeMoves;
        }

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} king";
        }

        private enum MovesType
        {
            Attack,
            Movement
        }
    }
}