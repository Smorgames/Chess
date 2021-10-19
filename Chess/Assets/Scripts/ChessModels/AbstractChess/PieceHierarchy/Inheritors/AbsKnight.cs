using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class AbsKnight : AbsPiece
    {
        public override PieceType MyType => PieceType.Knight;
        
        private Vector2Int _upLeftDir = new Vector2Int(-1, 2);
        private Vector2Int _upRightDir = new Vector2Int(1, 2);
        
        private Vector2Int _rightUpDir = new Vector2Int(2, 1);
        private Vector2Int _rightDownDir = new Vector2Int(2, -1);
        
        private Vector2Int _downRightDir = new Vector2Int(1, -2);
        private Vector2Int _downLeftDir = new Vector2Int(-1, -2);
        
        private Vector2Int _leftDownDir = new Vector2Int(-2, -1);
        private Vector2Int _leftUpDir = new Vector2Int(-2, 1);

        private List<Vector2Int> _directions;

        public AbsKnight(PieceColor color) : base(color)
        { 
            _directions = new List<Vector2Int>()
            {
                _upLeftDir, _upRightDir, _rightDownDir, _rightUpDir,
                _downLeftDir, _downRightDir, _leftDownDir, _leftUpDir
            };
        }

        public override List<AbsSquare> PossibleMoves(AbsSquare absSquare)
        {
            return GetMoves(absSquare, MovesType.Movement);
        }

        public override List<AbsSquare> PossibleAttackMoves(AbsSquare absSquare)
        {
            return GetMoves(absSquare, MovesType.Attack);
        }

        private List<AbsSquare> GetMoves(AbsSquare squareWithPiece, MovesType movesType)
        {
            var squares = new List<AbsSquare>();

            foreach (var dir in _directions)
            {
                var square = squareWithPiece.Board.GetSquareBasedOnCoordinates(squareWithPiece.Coordinates + dir);
                var pieceOnSquare = square.AbsPieceOnThisSquare;

                var conditionForAddAttackMove = movesType == MovesType.Attack && pieceOnSquare != null && MyColor != pieceOnSquare.MyColor;
                
                if (conditionForAddAttackMove) 
                    squares.Add(square);

                var conditionForAddMoves = movesType == MovesType.Movement && pieceOnSquare == null;
                
                if (conditionForAddMoves)
                    squares.Add(square);
            }

            return squares;
        }
        
        public override string ToString() => $"{MyColor} knight";
        
        private enum MovesType
        {
            Attack,
            Movement
        }
    }
}