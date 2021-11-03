using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ChessCodeHandler
{
    public static ChessCode EncodeBoard(Board board)
    {
        var stringBuilder = new StringBuilder();

        for (int x = 0; x < board.Size.x; x++)
        for (int y = 0; y < board.Size.y; y++)
        {
            if (TryGetPieceCode(board.Squares[x, y], out var pieceState))
                stringBuilder.Append(pieceState);
        }

        var piecesCode = stringBuilder.ToString();
        var boardSizeCode = GetBoardSizeCode(board);
        var whoseTurnCode = GameManager.Instance.WhoseTurn;

        return new ChessCode($"{piecesCode}/{boardSizeCode}/{whoseTurnCode}");
    }
    private static bool TryGetPieceCode(Square square, out string pieceCode)
    {
        var piece = square.PieceOnIt;

        if (piece == null)
        {
            pieceCode = null;
            return false;
        }

        var x = square.Coordinates.x.ToString();
        var y = square.Coordinates.y.ToString();
        var color = piece.ColorCode;
        var type = piece.TypeCode;
        var isFirstMove = piece.IsFirstMove ? "t" : "f";

        pieceCode = $"{x};{y};{color}{type}{isFirstMove}_";
        return true;
    }
    private static string GetBoardSizeCode(Board board) => $"{board.Size.x};{board.Size.y}";
    
    public static List<SignatureOfPiece> GetPieceSignaturesFromChessCode(ChessCode chessCode)
    {
        var pieceSignatures = new List<SignatureOfPiece>();
        var pieceCodes = GetPieceCodesFromChessCode(chessCode);

        foreach (var code in pieceCodes)
        {
            ParsePieceCode(code, out var squareCoord, out var piece);
            var pieceStateData = new SignatureOfPiece(squareCoord, piece);
            pieceSignatures.Add(pieceStateData);
        }

        return pieceSignatures;
    }
    private static List<string> GetPieceCodesFromChessCode(ChessCode chessCode)
    {
        var codeValue = chessCode.Value;
        var pieceCodes = new List<string>();

        for (int i = 0; i < codeValue.Length; ++i)
        {
            if (codeValue[i] == '/') return pieceCodes;

            var pieceCode = new StringBuilder();
            var j = i;

            while (codeValue[j] != '_')
            {
                pieceCode.Append(codeValue[j]);
                ++j;
            }

            pieceCodes.Add(pieceCode.ToString());
            i = j;
        }

        return pieceCodes;
    }
    private static void ParsePieceCode(string pieceCode, out Vector2Int squareCoordinates, out Piece piece)
    {
        var stringBuilder = new StringBuilder();

        var i = 0;
        while (pieceCode[i] != ';')
        {
            stringBuilder.Append(pieceCode[i]);
            ++i;
        } ++i; int.TryParse(stringBuilder.ToString(), out var x);
        stringBuilder.Clear();
        while (pieceCode[i] != ';')
        {
            stringBuilder.Append(pieceCode[i]);
            ++i;
        }
        ++i; int.TryParse(stringBuilder.ToString(), out var y);
        var colorCode = pieceCode[i].ToString(); ++i;
        var typeCode = pieceCode[i].ToString(); ++i;
        var isFirstMove = pieceCode[i].ToString() == "t";

        var piecePrefab = DataAboutPieces.GetPiecePrefabFromPieceCode(typeCode);

        piece = UnityEngine.Object.Instantiate(piecePrefab, Vector3.zero, Quaternion.identity);
        piece.Init(colorCode, isFirstMove);
        squareCoordinates = new Vector2Int(x, y);
    }
}