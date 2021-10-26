using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{ 
    public class AbsPawn : AbsPiece, IMoveDirection
    {
        public override string TypeCode => "p";
        
        public Vector2Int MoveDirection => ColorCode == "w" ? Vector2Int.up : Vector2Int.down;

        public AbsPawn(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove) { }

        public override List<ISquare> GetMoves(ISquare absSquare)
        {
            var moves = new List<ISquare>();

            var firstMoveSquare = GetSquareToMove(absSquare, 1);
            
            if (firstMoveSquare.PieceOnIt != null) 
                return moves;
            
            moves.Add(firstMoveSquare);

            if (IsFirstMove)
            {
                var secondMoveSquare = GetSquareToMove(absSquare, 2);
                
                if (secondMoveSquare.PieceOnIt != null) 
                    return moves;
                
                moves.Add(secondMoveSquare);
            }

            return moves;
        }
        private ISquare GetSquareToMove(ISquare absSquareWithChessPiece, int moveDistance)
        {
            var x = absSquareWithChessPiece.Coordinates.x + moveDistance * MoveDirection.x;
            var y = absSquareWithChessPiece.Coordinates.y + moveDistance * MoveDirection.y;
            
            var coordinates = new Vector2Int(x, y);

            return absSquareWithChessPiece.Board.SquareWithCoordinates(coordinates);
        }

        public override List<ISquare> GetAttacks(ISquare square)
        {
            var attackSquares = new List<ISquare>();
            
            var firstAttackSquare = GetAttackSquare(square, 1);
            var secondAttackSquare = GetAttackSquare(square, -1);
            var supposedAttackSquares = new List<ISquare>() { firstAttackSquare, secondAttackSquare };

            foreach (var s in supposedAttackSquares)
                if (s.PieceOnIt != null)
                {
                    var squareChessPieceColor = s.PieceOnIt.ColorCode;

                    if (ColorCode != squareChessPieceColor)
                        attackSquares.Add(s);
                }

            return attackSquares;
        }
        private ISquare GetAttackSquare(ISquare absSquareWithChessPiece, int xOffset)
        {
            var x = absSquareWithChessPiece.Coordinates.x + xOffset;
            var y = absSquareWithChessPiece.Coordinates.y + MoveDirection.y;
            var coordinates = new Vector2Int(x, y);
            
            return absSquareWithChessPiece.Board.SquareWithCoordinates(coordinates);
        }
        
        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} pawn";
        }
    }
}