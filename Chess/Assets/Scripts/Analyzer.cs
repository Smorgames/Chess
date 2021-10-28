﻿using System.Collections.Generic;
using System.Text;
using AbstractChess;
using UnityEngine;

public class Analyzer
{
    // Code system:
    // Example of code: 3;5;w;p;1_1;6;b;Q;0_1;1;w;r;0_~8;8
    // Token: 3;5;w;p;1
    // Position: (3;5)
    // Color: White - w; Black - b
    // Chess piece type: Pawn
    // Is first move of piece: 1 = true, 0 = false
    // Chess board size follows after character 's' (8;8)

    #region f(x) encode chess board to state code
    
    public static ChessCode EncodeChessBoard(IChessBoard chessBoard)
    {
        var stringBuilder = new StringBuilder();
        
        for (int x = 0; x < chessBoard.Size.x; x++)
        for (int y = 0; y < chessBoard.Size.y; y++)
            if (TryGetPieceStateCode(chessBoard.Squares[x, y], out var pieceState))
                stringBuilder.Append(pieceState);

        var piecesState = stringBuilder.ToString();
        var boardSize = GetBoardSizeCode(chessBoard);
        var whoseTurn = chessBoard.WhoseTurn;

        return new ChessCode(piecesState, boardSize, whoseTurn);
    }
    private static string GetBoardSizeCode(IChessBoard chessBoard)
    {
        var x = chessBoard.Size.x.ToString();
        var y = chessBoard.Size.y.ToString();

        return $"{x};{y}";
    }
    private static bool TryGetPieceStateCode(ISquare square, out string pieceState)
    {
        var piece = square.PieceOnIt;

        if (piece == null)
        {
            pieceState = null;
            return false;
        }

        var x = square.Coordinates.x.ToString();
        var y = square.Coordinates.y.ToString();
        var color = piece.ColorCode;
        var type = piece.TypeCode;
        var isFirstMove = piece.IsFirstMove ? "1" : "0";

        pieceState = $"{x};{y};{color};{type};{isFirstMove}_";
        return true;
    }

    #endregion

    #region f(x) convert real chess board to abstract chess board

    public static AbsChessBoard AbsBoardFromRealBoard(RealChessBoard realBoard)
    {
        var chessCode = EncodeChessBoard(realBoard);
        return AbsBoardFromChessCode(chessCode);
    }

    #endregion
    
    #region f(x) create abstract chess board from chess code

    public static AbsChessBoard AbsBoardFromChessCode(ChessCode chessCode)
    {
        var pieceCodes = PieceCodeListFromChessCode(chessCode);
        var absBoard = CreateAbsBoardBasedOnSizeCode(chessCode.BoardSize);

        foreach (var pieceCode in pieceCodes)
        {
            var absPieceToken = CreateAbsPieceTokenFromCode(pieceCode);
            var x = absPieceToken.SquareCoordinates.x;
            var y = absPieceToken.SquareCoordinates.y;
            
            absBoard.AbsSquares[x, y].AbsPieceOnIt = absPieceToken.AbsPiece;
        }

        absBoard.WhoseTurn = chessCode.WhoseTurn;
        return absBoard;
    }
    private static IEnumerable<string> PieceCodeListFromChessCode(ChessCode chessCode)
    {
        var piecesState = chessCode.PiecesState;
        var pieceCodes = new List<string>();
        
        for (int i = 0; i < piecesState.Length; ++i)
        {
            var pieceCode = new StringBuilder();
            var j = i;

            while (piecesState[j] != '_')
            {
                pieceCode.Append(piecesState[j]);
                ++j;
            }

            pieceCodes.Add(pieceCode.ToString());
            i = j;
        }

        return pieceCodes;
    }

    private static AbsChessBoard CreateAbsBoardBasedOnSizeCode(string sizeCode)
    {
        var size = BoardSizeFromCode(sizeCode);
        return new AbsChessBoard(size);
    }
    private static Vector2Int BoardSizeFromCode(string sizeCode)
    {
        var x = CoordinateFromCode(sizeCode, out sizeCode);
        var y = CoordinateFromCode(sizeCode, out sizeCode);

        return new Vector2Int(x, y);
    }

    private static int CoordinateFromCode(string code, out string newCode)
    {
        var sb = new StringBuilder();
        var i = 0;

        while (i < code.Length && code[i] != ';')
        {
            sb.Append(code[i]);
            ++i;
        }
        
        var parsed = int.TryParse(sb.ToString(), out var value);
        
        if (i < code.Length && code[i] == ';') sb.Append(code[i]);

        if (parsed)
        {
            newCode = code.Remove(0, sb.Length);
            return value;
        }

        newCode = "";
        Debug.LogError($"Incorrect value for {nameof(code)}! It must be integer, but equals: [{sb}]");
        return 0;
    }
    
    private static AbsPieceToken CreateAbsPieceTokenFromCode(string pieceCode)
    {
        var pieceData = PieceDataFromCode(pieceCode);
        var absPiece = CreateAbsPieceBasedOnPieceData(pieceData);

        return new AbsPieceToken(pieceData.Coordinates, absPiece, pieceData.IsFirstMove);
    }

    private static RealPieceToken CreateRealPieceTokenFromCode(string pieceCode)
    {
        var pieceData = PieceDataFromCode(pieceCode);
        var realPiece = CreateRealPieceBasedOnPieceData(pieceData);

        return new RealPieceToken(pieceData.Coordinates, realPiece, pieceData.IsFirstMove);
    }

    private static PieceData PieceDataFromCode(string pieceCode)
    {
        var x = CoordinateFromCode(pieceCode, out pieceCode);
        var y = CoordinateFromCode(pieceCode, out pieceCode);

        var i = 0;
        var colorCode = pieceCode[i].ToString();
        i += 2;

        var typeCode = pieceCode[i].ToString();
        i += 2;
        
        var isFirstMove = pieceCode[i] == '1';
        var coordinates = new Vector2Int(x, y);

        return new PieceData(coordinates, colorCode, typeCode, isFirstMove);
    }

    private static AbsPiece CreateAbsPieceBasedOnPieceData(PieceData pieceData)
    {
        if (pieceData.TypeCode == "p") return new AbsPawn(pieceData.ColorCode, pieceData.IsFirstMove);
        if (pieceData.TypeCode == "r") return new AbsRook(pieceData.ColorCode, pieceData.IsFirstMove);
        if (pieceData.TypeCode == "k") return new AbsKnight(pieceData.ColorCode, pieceData.IsFirstMove);
        if (pieceData.TypeCode == "b") return new AbsBishop(pieceData.ColorCode, pieceData.IsFirstMove);
        if (pieceData.TypeCode == "Q") return new AbsQueen(pieceData.ColorCode, pieceData.IsFirstMove);
        if (pieceData.TypeCode == "K") return new AbsKing(pieceData.ColorCode, pieceData.IsFirstMove);
        return null;
    }

    public static RealChessBoard RecreatePiecesFromChessCodeOnRealBoard(ChessCode chessCode, RealChessBoard realBoard)
    {
        var pieceCodes = PieceCodeListFromChessCode(chessCode);

        foreach (var pieceCode in pieceCodes)
        {
            var realPieceToken = CreateRealPieceTokenFromCode(pieceCode);
            var x = realPieceToken.SquareCoordinates.x;
            var y = realPieceToken.SquareCoordinates.y;

            var square = realBoard.RealSquares[x, y];
            square.RealPieceOnIt = realPieceToken.RealPiece;
            square.RealPieceOnIt.transform.position = square.transform.position;
        }

        realBoard.WhoseTurn = chessCode.WhoseTurn;
        return realBoard;
    }

    private static RealPiece CreateRealPieceBasedOnPieceData(PieceData pieceData)
    {
        var prefabsStorage = SingletonRegistry.Instance.PrefabsStorage;

        if (pieceData.TypeCode == "p") return Object.Instantiate(prefabsStorage.Pawn, Vector3.zero, Quaternion.identity).SetColorCode(pieceData.ColorCode);
        if (pieceData.TypeCode == "r") return Object.Instantiate(prefabsStorage.Rook, Vector3.zero, Quaternion.identity).SetColorCode(pieceData.ColorCode);
        if (pieceData.TypeCode == "k") return Object.Instantiate(prefabsStorage.Knight, Vector3.zero, Quaternion.identity).SetColorCode(pieceData.ColorCode);
        if (pieceData.TypeCode == "b") return Object.Instantiate(prefabsStorage.Bishop, Vector3.zero, Quaternion.identity).SetColorCode(pieceData.ColorCode);
        if (pieceData.TypeCode == "Q") return Object.Instantiate(prefabsStorage.Queen, Vector3.zero, Quaternion.identity).SetColorCode(pieceData.ColorCode);
        if (pieceData.TypeCode == "K") return Object.Instantiate(prefabsStorage.King, Vector3.zero, Quaternion.identity).SetColorCode(pieceData.ColorCode);
        return null;
    }

    #endregion

    #region f(x) create real chess board from chess code

    public static RealChessBoard RealBoardFromChessCode(ChessCode chessCode)
    {
        var realBoard = CreateRealBoardBasedOnSizeCode(chessCode.BoardSize);
        return RecreatePiecesFromChessCodeOnRealBoard(chessCode, realBoard);
    }
    
    private static RealChessBoard CreateRealBoardBasedOnSizeCode(string sizeCode) => CreateRealBoardBasedOnSizeCode(sizeCode, Vector2.zero);
    private static RealChessBoard CreateRealBoardBasedOnSizeCode(string sizeCode, Vector2 boardCenter)
    {
        var size = BoardSizeFromCode(sizeCode);
        return ChessboardBuilder.BuildArbitraryChessBoard(boardCenter, size);
    }

    #endregion

    #region f(x) find out is mate for King

    public static bool IsMateForKing(IChessBoard board)
    {
        var actionSquares = new List<ISquare>();

        for (int x = 0; x < board.Size.x; x++)
        for (int y = 0; y < board.Size.y; y++)
        {
            var square = board.Squares[x, y];
            var piece = square.PieceOnIt;

            if (piece != null && piece.ColorCode == board.WhoseTurn)
            {
                actionSquares.AddRange(piece.GetMoves(square));
                actionSquares.AddRange(piece.GetAttacks(square));

                if (actionSquares.Count > 0) return false;
            }
        }

        return true;
    }

    #endregion

    #region f(x) returns squares where piece can go and don't create check situation for its king

    public static List<ISquare> MovesWithoutCheckForKing(ISquare squareWithPiece, List<ISquare> supposedMovesOrAttacks, ActionType actionType)
    {
        var movesWithoutCheck = new List<ISquare>();
        var currentCode = EncodeChessBoard(squareWithPiece.Board);
        var kingColor = squareWithPiece.Board.WhoseTurn;

        foreach (var pieceMove in supposedMovesOrAttacks)
        {
            var codeWhenPieceMoved = actionType == ActionType.Movement ? 
                GetCodeWhenPieceMovedFromSquareToOther(currentCode, squareWithPiece, pieceMove) :
                GetCodeWhenPieceAttackFromSquareToOther(currentCode, squareWithPiece, pieceMove);

            var checkForKing = IsCheckForKingBasedOnChessCode(codeWhenPieceMoved, kingColor);

            if (!checkForKing)
                movesWithoutCheck.Add(pieceMove);
        }

        return movesWithoutCheck;
    }

    public static bool IsCheckForKingBasedOnChessCode(ChessCode code, string kingColorCode)
    {
        var absBoard = AbsBoardFromChessCode(code);
        var allAttackMoves = new List<ISquare>();

        AddAllEnemiesAttackMoves(absBoard, allAttackMoves, kingColorCode);

        foreach (var attackMove in allAttackMoves)
        {
            var kingIsUnderAttack = attackMove.PieceOnIt.TypeCode == "K";

            if (kingIsUnderAttack) 
                return true;
        }

        return false;
    }

    private static void AddAllEnemiesAttackMoves(IChessBoard chessBoard, List<ISquare> allAttackMoves, string codeOfKingsColor)
    {
        for (int x = 0; x < chessBoard.Size.x; x++)
            for (int y = 0; y < chessBoard.Size.y; y++)
            {
                var absPiece = chessBoard.Squares[x, y].PieceOnIt;

                if (absPiece != null && absPiece.ColorCode != codeOfKingsColor)
                {
                    var squareWithAbsPiece = chessBoard.Squares[x, y];
                    var pieceAttackMoves = absPiece.GetAttacks(squareWithAbsPiece);

                    allAttackMoves.AddRange(pieceAttackMoves);
                }
            }
    }

    private static ChessCode GetCodeWhenPieceAttackFromSquareToOther(ChessCode current, ISquare from, ISquare to)
    {
        TryGetPieceStateCode(from, out var fromPieceState);
        TryGetPieceStateCode(to, out var toPieceState);

        var fromCoordinates = GetCodeOfSquaresCoordinates(from);
        var toCoordinates = GetCodeOfSquaresCoordinates(to);

        var piecesState = current.PiecesState;

        var newPiecesState = piecesState.Replace(fromPieceState, "");
        newPiecesState = newPiecesState.Replace(toPieceState, fromPieceState);
        newPiecesState = newPiecesState.Replace(fromCoordinates, toCoordinates);

        return new ChessCode(newPiecesState, current.BoardSize, current.WhoseTurn);
    }

    private static ChessCode GetCodeWhenPieceMovedFromSquareToOther(ChessCode current, ISquare from, ISquare to)
    {
        var fromCoordinates = GetCodeOfSquaresCoordinates(from);
        var toCoordinates = GetCodeOfSquaresCoordinates(to);
        var piecesState = current.PiecesState;

        var newPiecesState = piecesState.Replace(fromCoordinates, toCoordinates);

        return new ChessCode(newPiecesState, current.BoardSize, current.WhoseTurn);
    }

    private static string GetCodeOfSquaresCoordinates(ISquare square)
    {
        var x = square.Coordinates.x.ToString();
        var y = square.Coordinates.y.ToString();
        
        return $"{x};{y}";
    }

    #endregion

    #region f(x) arrange chess pieces on real chess board

    

    #endregion
}

public class AbsPieceToken
{
    public readonly Vector2Int SquareCoordinates;
    public readonly AbsPiece AbsPiece;
    public readonly bool IsFirstMove;

    public AbsPieceToken(Vector2Int squareCoordinates, AbsPiece absPiece, bool isFirstMove)
    {
        SquareCoordinates = squareCoordinates;
        AbsPiece = absPiece;
        IsFirstMove = isFirstMove;
    }
}

public class RealPieceToken
{
    public readonly Vector2Int SquareCoordinates;
    public readonly RealPiece RealPiece;
    public readonly bool IsFirstMove;

    public RealPieceToken(Vector2Int squareCoordinates, RealPiece realPiece, bool isFirstMove)
    {
        SquareCoordinates = squareCoordinates;
        RealPiece = realPiece;
        IsFirstMove = isFirstMove;
    }
}

public class PieceData
{
    public readonly Vector2Int Coordinates;
    public readonly string ColorCode, TypeCode;
    public readonly bool IsFirstMove;

    public PieceData(Vector2Int coordinates, string colorCode, string typeCode, bool isFirstMove)
    {
        Coordinates = coordinates;
        ColorCode = colorCode;
        TypeCode = typeCode;
        IsFirstMove = isFirstMove;
    }
}