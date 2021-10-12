﻿using System.Collections.Generic;

namespace AbstractChess
{
    public class ChessboardArranger
    {
        public void ArrangeChessPiecesOnBoard(List<Token> tokens, Chessboard board)
        {
            foreach (var token in tokens)
            {
                var square = board.GetSquareBasedOnCoordinates(token.Coordinates);
                square.PieceOnThisSquare = token.Piece;
            }
        }
    }
}