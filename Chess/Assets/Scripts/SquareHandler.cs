using System.Text;

public class SquareHandler
{
    public static bool TryGetStateCodeOfSquare(Square square, out StateCode stateCode)
    {
        var piece = square.PieceOnIt;
        if (piece == null)
        {
            stateCode = new StateCode("");
            return false;
        }

        var codeValue = StateCodeValueGetter.CodeValue(square);
        stateCode = new StateCode(codeValue);
        
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
            var colorValue = GetColorValue(piece);
            var pieceTypeValue = piece.TypeCodeValue;
            var firstMoveValue = piece.IsFirstMove ? "1" : "0";

            stringBuilder.Append($"[{xValue};{yValue};{colorValue};{pieceTypeValue};{firstMoveValue}]");

            return stringBuilder.ToString();
        }
        
        private static string GetColorValue(Piece piece)
        {
            if (piece.MyColor == PieceColor.White) return "w";
            if (piece.MyColor == PieceColor.Black) return "b";
            return "";
        }
    }
}