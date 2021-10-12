using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public abstract class LinearlyMovingPiece : Piece
    {
        protected LinearlyMovingPiece(Color color) : base(color) { }

        protected void IterativelyDirectionallyFillPossibleMoves(
            List<Square> listToFill, Square pieceSquare, Vector2Int moveDirection, PieceAction actionType)
        {
            var x = pieceSquare.Coordinates.x;
            var y = pieceSquare.Coordinates.y;

            var chessboard = pieceSquare.Board;

            var boardLenght = GetLongSideOfChessboard(chessboard);
        
            for (int i = 1; i < boardLenght; i++)
            {
                var coordinates = new Vector2Int(x + i * moveDirection.x, y + i * moveDirection.y);
                var square = chessboard.GetSquareBasedOnCoordinates(coordinates);

                if (square == chessboard.GhostSquare)
                    break;

                var pieceOnSquare = square.PieceOnThisSquare;
            
                if (pieceOnSquare != null)
                {
                    var pieceHasOppositeColor = MyColor != pieceOnSquare.MyColor;
                    var pieceActionTypeIsAttack = actionType == PieceAction.Attack;
                
                    if (pieceHasOppositeColor && pieceActionTypeIsAttack)
                        listToFill.Add(square);
                
                    break;
                }

                if (actionType == PieceAction.Movement)
                    listToFill.Add(square);
            }
        }

        private int GetLongSideOfChessboard(Chessboard chessboard)
        {
            var boardLenght = chessboard.Length;
            var boardHeight = chessboard.Height;

            return boardLenght > boardHeight ? boardLenght : boardHeight;
        }
    
        public enum PieceAction
        {
            Attack,
            Movement
        }
    }
}