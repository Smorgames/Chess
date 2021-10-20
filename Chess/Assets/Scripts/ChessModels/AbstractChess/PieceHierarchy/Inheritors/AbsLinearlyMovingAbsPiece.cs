using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AbstractChess
{
    public abstract class AbsLinearlyMovingAbsPiece : AbsPiece
    {
        protected AbsLinearlyMovingAbsPiece(PieceColor color) : base(color) { }

        protected void IterativelyDirectionallyFillPossibleMoves(
            List<AbsSquare> listToFill, AbsSquare squareWithPiece, Vector2Int moveDirection, ActionType actionType)
        {
            var x = squareWithPiece.Coordinates.x;
            var y = squareWithPiece.Coordinates.y;

            var chessboard = squareWithPiece.Board;

            var boardLenght = GetLongSideOfChessboard(chessboard);
        
            for (int i = 1; i < boardLenght; i++)
            {
                var coordinates = new Vector2Int(x + i * moveDirection.x, y + i * moveDirection.y);
                var square = chessboard.GetSquareBasedOnCoordinates(coordinates);

                if (square == chessboard.GhostAbsSquare)
                    break;

                var pieceOnSquare = square.AbsPieceOnIt;
            
                if (pieceOnSquare != null)
                {
                    var pieceHasOppositeColor = MyColor != pieceOnSquare.MyColor;
                    var pieceActionTypeIsAttack = actionType == ActionType.Attack;
                
                    if (pieceHasOppositeColor && pieceActionTypeIsAttack)
                        listToFill.Add(square);
                
                    break;
                }

                if (actionType == ActionType.Movement)
                    listToFill.Add(square);
            }
        }

        private int GetLongSideOfChessboard(AbsChessboard absChessboard)
        {
            var boardLenght = absChessboard.Size.x;
            var boardHeight = absChessboard.Size.y;

            return boardLenght > boardHeight ? boardLenght : boardHeight;
        }
    }
}