
using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public class ChessStateParser
    {
        public List<Token> Parse(string chessState)
        {
            chessState = chessState.Trim(' ');
            var tokens = new List<Token>(); 

            for (int i = 0; i < chessState.Length; i += 4)
            {
                var xString = chessState[i].ToString();
                var isXinteger = int.TryParse(xString, out var x);

                if (!isXinteger)
                {
                    var className = nameof(CheckAnalyzer);
                    var methodName = nameof(Parse);
                    var variableName = nameof(xString);

                    Debug.LogError(
                        $"In method {className}.{methodName}() token '{variableName}' = {xString}. It's not an integer!");
                    return null;
                }

                var yString = chessState[i + 1].ToString();
                var isYinteger = int.TryParse(yString, out var y);

                if (!isYinteger)
                {
                    var className = nameof(CheckAnalyzer);
                    var methodName = nameof(Parse);
                    var variableName = nameof(yString);

                    Debug.LogError(
                        $"In method {className}.{methodName}() token '{variableName}' = {yString}. It's not an integer!");
                    return null;
                }

                var colorString = chessState[i + 2];
                Piece.Color color;

                if (colorString == 'w')
                    color = Piece.Color.White;
                else if (colorString == 'b')
                    color = Piece.Color.Black;
                else
                {
                    var className = nameof(CheckAnalyzer);
                    var methodName = nameof(Parse);
                    var variableName = nameof(colorString);

                    Debug.LogError(
                        $"In method {className}.{methodName}() token '{variableName}' = {colorString}. It's incorrect value!");
                    return null;
                }

                var pieceString = chessState[i + 3].ToString();
                Piece piece;

                switch (pieceString)
                {
                    case "p":
                        piece = new Pawn(color);
                        break;
                    case "r":
                        piece = new Rook(color);
                        break;
                    case "k":
                        piece = new Knight(color);
                        break;
                    case "b":
                        piece = new Bishop(color);
                        break;
                    case "Q":
                        piece = new Queen(color);
                        break;
                    case "K":
                        piece = new King(color);
                        break;
                    default:
                        var className = nameof(CheckAnalyzer);
                        var methodName = nameof(Parse);
                        var variableName = nameof(pieceString);
                        
                        Debug.LogError($"In method {className}.{methodName}() token '{variableName}' = {pieceString}. It's incorrect value!");
                        return null;
                }

                var coordinates = new Vector2Int(x, y);
                var chessToken = new Token(coordinates, piece);
                tokens.Add(chessToken);
            }

            return tokens;
        }
    }
}