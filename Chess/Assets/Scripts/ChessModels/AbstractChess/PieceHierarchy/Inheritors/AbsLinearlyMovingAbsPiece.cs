﻿using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public abstract class AbsLinearlyMovingAbsPiece : AbsPiece
    {
        protected AbsLinearlyMovingAbsPiece(PieceColor color) : base(color) { }

        protected void IterativelyDirectionallyFillPossibleMoves(
            List<AbsSquare> listToFill, AbsSquare pieceAbsSquare, Vector2Int moveDirection, PieceAction actionType)
        {
            var x = pieceAbsSquare.Coordinates.x;
            var y = pieceAbsSquare.Coordinates.y;

            var chessboard = pieceAbsSquare.Board;

            var boardLenght = GetLongSideOfChessboard(chessboard);
        
            for (int i = 1; i < boardLenght; i++)
            {
                var coordinates = new Vector2Int(x + i * moveDirection.x, y + i * moveDirection.y);
                var square = chessboard.GetSquareBasedOnCoordinates(coordinates);

                if (square == chessboard.GhostAbsSquare)
                    break;

                var pieceOnSquare = square.AbsPieceOnThisSquare;
            
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

        private int GetLongSideOfChessboard(AbsChessboard absChessboard)
        {
            var boardLenght = absChessboard.Length;
            var boardHeight = absChessboard.Height;

            return boardLenght > boardHeight ? boardLenght : boardHeight;
        }
    
        public enum PieceAction
        {
            Attack,
            Movement
        }
    }
}