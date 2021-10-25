using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsKnight : AbsPiece
    {        
        private static Vector2Int _upLeftDir = new Vector2Int(-1, 2);
        private static Vector2Int _upRightDir = new Vector2Int(1, 2);
        
        private static Vector2Int _rightUpDir = new Vector2Int(2, 1);
        private static Vector2Int _rightDownDir = new Vector2Int(2, -1);
        
        private static Vector2Int _downRightDir = new Vector2Int(1, -2);
        private static Vector2Int _downLeftDir = new Vector2Int(-1, -2);
        
        private static Vector2Int _leftDownDir = new Vector2Int(-2, -1);
        private static Vector2Int _leftUpDir = new Vector2Int(-2, 1);

        private List<Vector2Int> _directions;

        public override string TypeCode => "k";

        public AbsKnight(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove)
        { 
            _directions = new List<Vector2Int>()
            {
                _upLeftDir, _upRightDir, _rightDownDir, _rightUpDir,
                _downLeftDir, _downRightDir, _leftDownDir, _leftUpDir
            };
        }

        public override List<IRealSquare> GetMoves(IRealSquare square)
        {
            return GetMoves(square, MovesType.Movement);
        }

        public override List<IRealSquare> GetAttacks(IRealSquare square)
        {
            return GetMoves(square, MovesType.Attack);
        }

        private List<IRealSquare> GetMoves(IRealSquare squareWithPiece, MovesType movesType)
        {
            var squares = new List<IRealSquare>();

            foreach (var dir in _directions)
            {
                var square = squareWithPiece.Board.GetSquareWithCoordinates(squareWithPiece.Coordinates + dir);
                var pieceOnSquare = square.PieceOnIt;

                var conditionForAddAttackMove = movesType == MovesType.Attack && pieceOnSquare != null && ColorCode != pieceOnSquare.ColorCode;
                
                if (conditionForAddAttackMove) 
                    squares.Add(square);

                var conditionForAddMoves = movesType == MovesType.Movement && pieceOnSquare == null;
                
                if (conditionForAddMoves)
                    squares.Add(square);
            }

            return squares;
        }

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} knight";
        }

        private enum MovesType
        {
            Attack,
            Movement
        }
    }
}