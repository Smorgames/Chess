using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AnalysisOfChessState.CodeHandler
{
    public class StateCodeHandler
    {
        public List<PieceToken> GetTokens(StateCode stateCode)
        {
            var code = stateCode.Value.Trim(' ');
            var chessTokens = new List<PieceToken>(); 

            for (int i = 0; i < code.Length; i += 4)
            {
                var xString = code[i].ToString();
                var xIsInt = int.TryParse(xString, out var x);

                if (!xIsInt)
                {
                    var className = nameof(StateCodeHandler);
                    var methodName = nameof(GetTokens);
                    var variableName = nameof(xString);

                    Debug.LogError(
                        $"In method {className}.{methodName}() token '{variableName}' = {xString}. It's not an integer!");
                    return null;
                }

                var yString = code[i + 1].ToString();
                var yIsInt = int.TryParse(yString, out var y);

                if (!yIsInt)
                {
                    var className = nameof(StateCodeHandler);
                    var methodName = nameof(GetTokens);
                    var variableName = nameof(yString);

                    Debug.LogError(
                        $"In method {className}.{methodName}() token '{variableName}' = {yString}. It's not an integer!");
                    return null;
                }

                var colorString = code[i + 2];
                PieceColor pieceColor;

                if (colorString == 'w')
                    pieceColor = PieceColor.White;
                else if (colorString == 'b')
                    pieceColor = PieceColor.Black;
                else
                {
                    var className = nameof(StateCodeHandler);
                    var methodName = nameof(GetTokens);
                    var variableName = nameof(colorString);

                    Debug.LogError(
                        $"In method {className}.{methodName}() token '{variableName}' = {colorString}. It's incorrect value!");
                    return null;
                }

                var pieceString = code[i + 3].ToString();
                PieceType pieceType;

                switch (pieceString)
                {
                    case "p":
                        pieceType = PieceType.Pawn;
                        break;
                    case "r":
                        pieceType = PieceType.Rook;
                        break;
                    case "k":
                        pieceType = PieceType.Knight;
                        break;
                    case "b":
                        pieceType = PieceType.Bishop;
                        break;
                    case "Q":
                        pieceType = PieceType.Queen;
                        break;
                    case "K":
                        pieceType = PieceType.King;
                        break;
                    default:
                        var className = nameof(StateCodeHandler);
                        var methodName = nameof(GetTokens);
                        var variableName = nameof(pieceString);
                        
                        Debug.LogError($"In method {className}.{methodName}() token '{variableName}' = {pieceString}. It's incorrect value!");
                        return null;
                }

                var coordinates = new Vector2Int(x, y);
                var chessToken = new PieceToken(coordinates, pieceColor, pieceType);
                chessTokens.Add(chessToken);
            }

            return chessTokens;
        }

        public StateCode GetStateCode(List<PieceToken> tokens)
        {
            var stringBuilder = new StringBuilder();
            
            foreach (var token in tokens)
            {
                stringBuilder.Append(token.Coordinates.x);
                stringBuilder.Append(token.Coordinates.y);

                switch (token.MyPieceColor)
                {
                    case PieceColor.White:
                        stringBuilder.Append("w");
                        break;
                    case PieceColor.Black:
                        stringBuilder.Append("b");
                        break;
                }

                switch (token.MyPieceType)
                {
                    case PieceType.Pawn:
                        stringBuilder.Append("p");
                        break;
                    case PieceType.Rook:
                        stringBuilder.Append("r");
                        break;
                    case PieceType.Knight:
                        stringBuilder.Append("k");
                        break;
                    case PieceType.Bishop:
                        stringBuilder.Append("b");
                        break;
                    case PieceType.Queen:
                        stringBuilder.Append("Q");
                        break;
                    case PieceType.King:
                        stringBuilder.Append("K");
                        break;
                }
            }

            return new StateCode(stringBuilder.ToString());
        }
        
        public List<PieceToken> GetTokens(Chessboard chessboard)
        {
            var tokens = new List<PieceToken>();
            
            foreach (var square in chessboard.Squares)
            {
                if (square.PieceOnSquare != null)
                {
                    var token = GetTokenBasedOnSquare(square);
                    tokens.Add(token);
                }
            }

            return tokens;
        }

        private PieceToken GetTokenBasedOnSquare(Square square)
        {
            var piece = square.PieceOnSquare;
            var coordinates = square.Coordinates;

            if (piece is Pawn)
                return new PieceToken(coordinates, piece.ColorData.Color, PieceType.Pawn);
            if (piece is Rook)
                return new PieceToken(coordinates, piece.ColorData.Color, PieceType.Rook);
            if (piece is Knight)
                return new PieceToken(coordinates, piece.ColorData.Color, PieceType.Knight);
            if (piece is Bishop)
                return new PieceToken(coordinates, piece.ColorData.Color, PieceType.Bishop);
            if (piece is Queen)
                return new PieceToken(coordinates, piece.ColorData.Color, PieceType.Queen);
            if (piece is King)
                return new PieceToken(coordinates, piece.ColorData.Color, PieceType.King);

            return null;
        }
    }
}