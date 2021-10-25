using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AbstractChess
{
    public abstract class AbsLinearlyMovingAbsPiece : AbsPiece
    {
        protected AbsLinearlyMovingAbsPiece(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove) { }

        protected void IterativelyDirectionallyFillPossibleMoves(
            List<IRealSquare> listToFill, IRealSquare squareWithPiece, Vector2Int moveDirection, ActionType actionType)
        {
            var x = squareWithPiece.Coordinates.x;
            var y = squareWithPiece.Coordinates.y;

            var chessboard = squareWithPiece.Board;

            var boardLenght = GetLongSideOfChessboard(chessboard);
        
            for (int i = 1; i < boardLenght; i++)
            {
                var coordinates = new Vector2Int(x + i * moveDirection.x, y + i * moveDirection.y);
                var square = chessboard.GetSquareWithCoordinates(coordinates);

                if (square == chessboard.GhostSquare)
                    break;

                var pieceOnSquare = square.PieceOnIt;
            
                if (pieceOnSquare != null)
                {
                    var pieceHasOppositeColor = ColorCode != pieceOnSquare.ColorCode;
                    var pieceActionTypeIsAttack = actionType == ActionType.Attack;
                
                    if (pieceHasOppositeColor && pieceActionTypeIsAttack)
                        listToFill.Add(square);
                
                    break;
                }

                if (actionType == ActionType.Movement)
                    listToFill.Add(square);
            }
        }

        private int GetLongSideOfChessboard(IChessBoard chessboard)
        {
            var boardLenght = chessboard.Size.x;
            var boardHeight = chessboard.Size.y;

            return boardLenght > boardHeight ? boardLenght : boardHeight;
        }
    }
}