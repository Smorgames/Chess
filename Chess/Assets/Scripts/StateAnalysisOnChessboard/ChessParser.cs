using System.Collections.Generic;
using System.Text;
using AbstractChess;
using UnityEngine;

namespace AnalysisOfChessState.Parser
{
    public class ChessParser
    {
        public List<ChessToken> Parse(string chessStateCode)
        {
            chessStateCode = chessStateCode.Trim(' ');
            var chessTokens = new List<ChessToken>(); 

            for (int i = 0; i < chessStateCode.Length; i += 4)
            {
                var xString = chessStateCode[i].ToString();
                var xIsInt = int.TryParse(xString, out var x);

                if (!xIsInt)
                {
                    var className = nameof(ChessParser);
                    var methodName = nameof(Parse);
                    var variableName = nameof(xString);

                    Debug.LogError(
                        $"In method {className}.{methodName}() token '{variableName}' = {xString}. It's not an integer!");
                    return null;
                }

                var yString = chessStateCode[i + 1].ToString();
                var yIsInt = int.TryParse(yString, out var y);

                if (!yIsInt)
                {
                    var className = nameof(ChessParser);
                    var methodName = nameof(Parse);
                    var variableName = nameof(yString);

                    Debug.LogError(
                        $"In method {className}.{methodName}() token '{variableName}' = {yString}. It's not an integer!");
                    return null;
                }

                var colorString = chessStateCode[i + 2];
                PieceColor pieceColor;

                if (colorString == 'w')
                    pieceColor = PieceColor.White;
                else if (colorString == 'b')
                    pieceColor = PieceColor.Black;
                else
                {
                    var className = nameof(ChessParser);
                    var methodName = nameof(Parse);
                    var variableName = nameof(colorString);

                    Debug.LogError(
                        $"In method {className}.{methodName}() token '{variableName}' = {colorString}. It's incorrect value!");
                    return null;
                }

                var pieceString = chessStateCode[i + 3].ToString();
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
                        var className = nameof(ChessParser);
                        var methodName = nameof(Parse);
                        var variableName = nameof(pieceString);
                        
                        Debug.LogError($"In method {className}.{methodName}() token '{variableName}' = {pieceString}. It's incorrect value!");
                        return null;
                }

                var coordinates = new Vector2Int(x, y);
                var chessToken = new ChessToken(coordinates, pieceColor, pieceType);
                chessTokens.Add(chessToken);
            }

            return chessTokens;
        }

        public List<ChessToken> Parse(AbsChessboard abstractAbsChessboard)
        {
            var chessTokens = new List<ChessToken>();

            foreach (var square in abstractAbsChessboard.Squares)
            {
                var pieceOnSquare = square.AbsPieceOnThisSquare;
                
                if (pieceOnSquare != null)
                {
                    var coordinates = square.Coordinates;
                    var color = pieceOnSquare.MyColor;
                    var type = pieceOnSquare.MyType;
                    
                    var token = new ChessToken(coordinates, color, type);
                    chessTokens.Add(token);
                }
            }

            return chessTokens;
        }

        public string GetChessStateCode(AbsChessboard abstractAbsChessboard)
        {
            var chessTokens = Parse(abstractAbsChessboard);
            return GetChessStateCode(chessTokens);
        }
        
        private string GetChessStateCode(List<ChessToken> chessTokens)
        {
            var stringBuilder = new StringBuilder();
            
            foreach (var token in chessTokens)
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

            return stringBuilder.ToString();
        }
    }
}