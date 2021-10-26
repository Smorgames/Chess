using System.Text;

public class SquareHandler
{
    public static bool TryGetStateCodeOfSquare(RealSquare realSquare, out ChessCode chessCode)
    {
        var piece = realSquare.PieceOnIt;
        if (piece == null)
        {
            chessCode = new ChessCode("");
            return false;
        }

        var codeValue = StateCodeValueGetter.CodeValue(realSquare);
        chessCode = new ChessCode(codeValue);
        
        return true;
    }

    private class StateCodeValueGetter
    {
        public static string CodeValue(RealSquare realSquare)
        {
            var stringBuilder = new StringBuilder();
            var piece = realSquare.PieceOnIt;
            
            var xValue = realSquare.Coordinates.x.ToString();
            var yValue = realSquare.Coordinates.y.ToString();
            var colorValue = piece.ColorCode;
            var pieceTypeValue = piece.TypeCode;
            var firstMoveValue = piece.IsFirstMove ? "1" : "0";

            stringBuilder.Append($"[{xValue};{yValue};{colorValue};{pieceTypeValue};{firstMoveValue}]");

            return stringBuilder.ToString();
        }
    }
}