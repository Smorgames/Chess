﻿using System.Text;

public class SquareHandler
{
    public static bool TryGetStateCodeOfSquare(Square square, out ChessCode chessCode)
    {
        var piece = square.PieceOnIt;
        if (piece == null)
        {
            chessCode = new ChessCode("");
            return false;
        }

        var codeValue = StateCodeValueGetter.CodeValue(square);
        chessCode = new ChessCode(codeValue);
        
        return true;
    }

    private class StateCodeValueGetter
    {
        public static string CodeValue(Square square)
        {
            var stringBuilder = new StringBuilder();
            var piece = square.PieceOnIt;
            
            var xValue = square.Coordinates.x.ToString();
            var yValue = square.Coordinates.y.ToString();
            var colorValue = piece.ColorCode;
            var pieceTypeValue = piece.TypeCode;
            var firstMoveValue = piece.IsFirstMove ? "1" : "0";

            stringBuilder.Append($"[{xValue};{yValue};{colorValue};{pieceTypeValue};{firstMoveValue}]");

            return stringBuilder.ToString();
        }
    }
}