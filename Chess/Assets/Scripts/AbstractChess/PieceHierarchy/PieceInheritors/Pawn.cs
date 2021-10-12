using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{ 
    public class Pawn : Piece, IMoveDirection
    {
        public Vector2Int MoveDirection
        {
            get
            {
                if (MyColor == Color.White)
                    return Vector2Int.up;
                
                if (MyColor == Color.Black)
                    return Vector2Int.down;
                
                return Vector2Int.zero;
            }
        }
        
        public Pawn(Color color) : base(color) { }

        public override List<Square> PossibleMoves(Square square)
        {
            var moves = new List<Square>();

            var firstMoveSquare = GetSquareToMove(square, 1);
            
            if (firstMoveSquare.PieceOnThisSquare != null) 
                return moves;
            
            moves.Add(firstMoveSquare);

            if (_isFirstMove)
            {
                var secondMoveSquare = GetSquareToMove(square, 2);
                
                if (secondMoveSquare.PieceOnThisSquare != null) 
                    return moves;
                
                moves.Add(secondMoveSquare);
            }

            return moves;
        }
        private Square GetSquareToMove(Square squareWithChessPiece, int moveDistance)
        {
            var x = squareWithChessPiece.Coordinates.x + moveDistance * MoveDirection.x;
            var y = squareWithChessPiece.Coordinates.y + moveDistance * MoveDirection.y;
            
            var coordinates = new Vector2Int(x, y);

            return squareWithChessPiece.Board.GetSquareBasedOnCoordinates(coordinates);
        }

        public override List<Square> PossibleAttackMoves(Square square)
        {
            var attackSquares = new List<Square>();
            
            var firstAttackSquare = GetAttackSquare(square, 1);
            var secondAttackSquare = GetAttackSquare(square, -1);
            var supposedAttackSquares = new List<Square>() { firstAttackSquare, secondAttackSquare };

            foreach (var s in supposedAttackSquares)
                if (s.PieceOnThisSquare != null)
                {
                    var squareChessPieceColor = s.PieceOnThisSquare.MyColor;

                    if (MyColor != squareChessPieceColor)
                        attackSquares.Add(s);
                }

            return attackSquares;
        }
        private Square GetAttackSquare(Square squareWithChessPiece, int xOffset)
        {
            var x = squareWithChessPiece.Coordinates.x + xOffset;
            var y = squareWithChessPiece.Coordinates.y + MoveDirection.y;
            var coordinates = new Vector2Int(x, y);
            
            return squareWithChessPiece.Board.GetSquareBasedOnCoordinates(coordinates);
        }
        
        public override string ToString() => $"{MyColor} pawn";
    }
}