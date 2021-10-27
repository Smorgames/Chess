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

        public override List<ISquare> GetMoves(ISquare square)
        {
            var supposedMoves = new List<ISquare>();
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                supposedMoves = SetMoves(ActionType.Movement, square, directions);
            
            return Analyzer.MovesWithoutCheckForKing(square, supposedMoves, ActionType.Movement);
        }

        private List<ISquare> SetMoves(ActionType actionType, ISquare squareWithPiece, List<Vector2Int> moveDirections)
        {
            var squares = new List<ISquare>();

            foreach (var dir in moveDirections)
            {
                var x = squareWithPiece.Coordinates.x;
                var y = squareWithPiece.Coordinates.y;
                var coordinates = new Vector2Int(x + dir.x, y + dir.y);

                var square = squareWithPiece.Board.SquareWithCoordinates(coordinates);
                var pieceOnSquare = squareWithPiece.PieceOnIt;
                
                var conditionForAddAttackMove = actionType == ActionType.Attack && pieceOnSquare != null && ColorCode != pieceOnSquare.ColorCode;
                
                if (conditionForAddAttackMove) 
                    squares.Add(square);

                var conditionForAddMoves = actionType == ActionType.Movement && pieceOnSquare == null;
                
                if (conditionForAddMoves)
                    squares.Add(square);
            }

            return squares;
        }

        public override List<ISquare> GetAttacks(ISquare square)
        {
            var supposedMoves = new List<ISquare>();
            var directions = new List<Vector2Int>()
            {
                _upRightDir, _upLeftDir, _downLeftDir, _downRightDir,
                Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
            };

            foreach (var dir in directions)
                supposedMoves = SetMoves(ActionType.Attack, square, directions);
            
            return Analyzer.MovesWithoutCheckForKing(square, supposedMoves, ActionType.Attack);
        }

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} king";
        }
    }
}