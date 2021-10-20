using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{ 
    public class AbsPawn : AbsPiece, IMoveDirection
    {
        public override PieceType MyType => PieceType.Pawn;
        
        public Vector2Int MoveDirection
        {
            get
            {
                if (MyColor == PieceColor.White)
                    return Vector2Int.up;
                
                if (MyColor == PieceColor.Black)
                    return Vector2Int.down;
                
                return Vector2Int.zero;
            }
        }
        
        public AbsPawn(PieceColor color) : base(color) { }

        public override List<AbsSquare> PossibleMoves(AbsSquare absSquare)
        {
            var moves = new List<AbsSquare>();

            var firstMoveSquare = GetSquareToMove(absSquare, 1);
            
            if (firstMoveSquare.AbsPieceOnIt != null) 
                return moves;
            
            moves.Add(firstMoveSquare);

            if (IsFirstMove)
            {
                var secondMoveSquare = GetSquareToMove(absSquare, 2);
                
                if (secondMoveSquare.AbsPieceOnIt != null) 
                    return moves;
                
                moves.Add(secondMoveSquare);
            }

            return moves;
        }
        private AbsSquare GetSquareToMove(AbsSquare absSquareWithChessPiece, int moveDistance)
        {
            var x = absSquareWithChessPiece.Coordinates.x + moveDistance * MoveDirection.x;
            var y = absSquareWithChessPiece.Coordinates.y + moveDistance * MoveDirection.y;
            
            var coordinates = new Vector2Int(x, y);

            return absSquareWithChessPiece.Board.GetSquareBasedOnCoordinates(coordinates);
        }

        public override List<AbsSquare> PossibleAttackMoves(AbsSquare absSquare)
        {
            var attackSquares = new List<AbsSquare>();
            
            var firstAttackSquare = GetAttackSquare(absSquare, 1);
            var secondAttackSquare = GetAttackSquare(absSquare, -1);
            var supposedAttackSquares = new List<AbsSquare>() { firstAttackSquare, secondAttackSquare };

            foreach (var s in supposedAttackSquares)
                if (s.AbsPieceOnIt != null)
                {
                    var squareChessPieceColor = s.AbsPieceOnIt.MyColor;

                    if (MyColor != squareChessPieceColor)
                        attackSquares.Add(s);
                }

            return attackSquares;
        }
        private AbsSquare GetAttackSquare(AbsSquare absSquareWithChessPiece, int xOffset)
        {
            var x = absSquareWithChessPiece.Coordinates.x + xOffset;
            var y = absSquareWithChessPiece.Coordinates.y + MoveDirection.y;
            var coordinates = new Vector2Int(x, y);
            
            return absSquareWithChessPiece.Board.GetSquareBasedOnCoordinates(coordinates);
        }
        
        public override string ToString() => $"{MyColor} pawn";
    }
}