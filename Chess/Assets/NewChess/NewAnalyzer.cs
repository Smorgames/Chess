using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NewAnalyzer
{
    public static List<NewSquare> MovesWithoutCheckForKing(NewSquare square, List<NewSquare> supposedMoves)
    {
        var kingColor = square.PieceOnIt.ColorCode;
        var board = square.Board;

        var correctMoves = new List<NewSquare>();
        
        foreach (var move in supposedMoves)
        {
            board.PlacePieceFromSquareToOther(square, move);
            var check = CheckForKing(board, kingColor);
            
            if (!check) correctMoves.Add(move);
            board.UNDO_PlacePieceFromSquareToOther();
        }

        return correctMoves;
    }
    private static bool CheckForKing(NewBoard board, string kingColor)
    {
        var enemyColor = kingColor == "w" ? "b" : "w";
        UpdateSupposedMovesForIterativeMovingPiece(board, enemyColor);

        var allEnemiesMoves = new List<NewSquare>();

        for (var x = 0; x < board.Size.x; x++)
        for (var y = 0; y < board.Size.y; y++)
        {
            var enemy = board.Squares[x, y].PieceOnIt;

            if (enemy == null || enemy.ColorCode == kingColor) continue;

            var enemyMoves = enemy.SupposedMoves;

            foreach (var move in enemyMoves)
                allEnemiesMoves.Add(move);
        }

        foreach (var move in allEnemiesMoves)
        {
            var attackedPiece = move.PieceOnIt;
            if (attackedPiece == null) continue;

            if (attackedPiece is NewKing) return true;
        }

        return false;
    }
    private static void UpdateSupposedMovesForIterativeMovingPiece(NewBoard board, string color)
    {
        for (var x = 0; x < board.Size.x; x++)
        for (var y = 0; y < board.Size.y; y++)
        {
            var piece = board.Squares[x, y].PieceOnIt;

            if (piece == null || piece.ColorCode != color) continue;

            if (piece is UniversallyMovingPieces)
                piece.UpdateSupposedMoves(board.Squares[x, y]);
        }
    }

    public static bool MateForKing(NewBoard board, string kingColor)
    {
        var possibleMoves = new List<NewSquare>();
        
        for (var x = 0; x < board.Size.x; x++)
        for (var y = 0; y < board.Size.y; y++)
        {
            var piece = board.Squares[x, y].PieceOnIt;

            if (piece == null || piece.ColorCode != kingColor) continue;
            
            var piecesMoves = piece.SupposedMoves;
            if (piecesMoves.Count > 0) return false;
        }

        return true;
    }

    public static StateCode EncodeBoard(NewBoard board)
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
        var whoseTurnCode = NewGameManager.Instance.WhoseTurn;

        return new StateCode($"{piecesCode}/{boardSizeCode}/{whoseTurnCode}");
    }
    private static bool TryGetPieceCode(NewSquare square, out string pieceCode)
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
    private static string GetBoardSizeCode(NewBoard board) => $"{board.Size.x};{board.Size.y}";

    public static List<PieceStateData> GetPieceStateDataFromStateCode(StateCode stateCode)
    {
        var pieceStateDataList = new List<PieceStateData>();
        var pieceCodes = PieceCodeListFromStateCode(stateCode);

        foreach (var code in pieceCodes)
        {
            ParsePieceCode(code, out var squareCoord, out var piece);
            var pieceStateData = new PieceStateData(squareCoord, piece);
            pieceStateDataList.Add(pieceStateData);
        }

        return pieceStateDataList;
    }
    private static List<string> PieceCodeListFromStateCode(StateCode stateCode)
    {
        var stateCodeValue = stateCode.Value;
        var pieceCodeList = new List<string>();

        for (int i = 0; i < stateCodeValue.Length; ++i)
        {
            if (stateCodeValue[i] == '/') 
                return pieceCodeList;

            var pieceCode = new StringBuilder();
            var j = i;

            while (stateCodeValue[j] != '_')
            {
                pieceCode.Append(stateCodeValue[j]);
                ++j;
            }

            pieceCodeList.Add(pieceCode.ToString());
            i = j;
        }

        return pieceCodeList;
    }
    private static void ParsePieceCode(string pieceCode, out Vector2Int squareCoordinates, out NewPiece piece)
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

    public static NewBoard ArrangePiecesOnBoard(List<PieceStateData> pieceStateDataList, NewBoard board)
    {
        foreach (var pieceStateData in pieceStateDataList)
        {
            var square = board.Squares[pieceStateData.SquareCoordinates.x, pieceStateData.SquareCoordinates.y];
            square.PieceOnIt = pieceStateData.Piece;
            pieceStateData.Piece.transform.position = square.transform.position + NewPiece.Offset;
        }

        return board;
    }
}

public class PieceStateData
{
    public readonly Vector2Int SquareCoordinates;
    public readonly NewPiece Piece;

    public PieceStateData(Vector2Int squareCoordinates, NewPiece piece)
    {
        SquareCoordinates = squareCoordinates;
        Piece = piece;
    }
}

public static class DataAboutPieces
{
    private static Dictionary<string, NewPiece> _pieceCodeAndPiecePrefabDict = new Dictionary<string, NewPiece>();

    public static NewPiece GetPiecePrefabFromPieceCode(string typeCode)
    {
        if (_pieceCodeAndPiecePrefabDict.Count == 0)
            FillPieceCodeAndPiecePrefabDict();

        if (_pieceCodeAndPiecePrefabDict.TryGetValue(typeCode, out var prefab)) return prefab;
        Debug.LogError($"Incorrect {nameof(typeCode)} value!");
        return null;
    }

    private static void FillPieceCodeAndPiecePrefabDict()
    {
        var storage = ReferenceRegistry.Instance.PrefabsStorage;

        var keyValueList = new List<Tuple<string, NewPiece>>()
        {
            new Tuple<string, NewPiece>("p", storage.Pawn),
            new Tuple<string, NewPiece>("r", storage.Rook),
            new Tuple<string, NewPiece>("n", storage.Knight),
            new Tuple<string, NewPiece>("b", storage.Bishop),
            new Tuple<string, NewPiece>("q", storage.Queen),
            new Tuple<string, NewPiece>("k", storage.King)
        };

        foreach (var keyValue in keyValueList)
            _pieceCodeAndPiecePrefabDict.Add(keyValue.Item1, keyValue.Item2);
    }
}

public class StateCode
{
    public readonly string Value;

    public StateCode(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;
}