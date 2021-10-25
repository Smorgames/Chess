using System.Collections.Generic;
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

    #region f(x) encode real chess board to state code
    
    public static ChessCode EncodeRealBoard(IChessBoard realBoard)
    {
        var stringBuilder = new StringBuilder();
        
        for (int x = 0; x < realBoard.Size.x; x++)
        for (int y = 0; y < realBoard.Size.y; y++)
            if (TryGetPieceStateCode(realBoard.Squares[x, y], out var pieceState))
                stringBuilder.Append(pieceState);

        var piecesState = stringBuilder.ToString();
        var boardSize = GetBoardSizeCode(realBoard);
        var whoseTurn = realBoard.WhoseTurn;

        return new ChessCode(piecesState, boardSize, whoseTurn);
    }

    private static string GetBoardSizeCode(IChessBoard realBoard)
    {
        var x = realBoard.Size.x.ToString();
        var y = realBoard.Size.y.ToString();

        return $"{x};{y}";
    }

    private static bool TryGetPieceStateCode(IRealSquare square, out string pieceState)
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

    public static AbsChessboard AbsBoardFromRealBoard(Chessboard realBoard)
    {
        var chessCode = EncodeRealBoard(realBoard);
        return AbsBoardFromChessCode(chessCode);
    }

    #endregion

    #region f(x) convert chess code to abstract chess board

    public static AbsChessboard AbsBoardFromChessCode(ChessCode chessCode)
    {
        var absBoard = CreateAbsBoardFormBoardSizeCode(chessCode.BoardSize);
        var piecesState = chessCode.PiecesState;

        for (int i = 0; i < piecesState.Length; ++i)
        {
            var stringBuilder = new StringBuilder();
            var j = i;

            while (piecesState[j] != '_')
            {
                stringBuilder.Append(piecesState[j]);
                ++j;
            }

            var absPieceToken = CreateAbsPieceTokenFromCode(stringBuilder.ToString());
            
            var x = absPieceToken.SquareCoordinates.x;
            var y = absPieceToken.SquareCoordinates.y;

            absBoard.Squares[x, y].PieceOnIt = absPieceToken.ChessPiece;
            i = j;
        }

        return absBoard;
    }

    private static AbsChessboard CreateAbsBoardFormBoardSizeCode(string boardSizeCode)
    {
        var stringBuilder = new StringBuilder();
        var i = 0;

        while (boardSizeCode[i] != ';')
        {
            stringBuilder.Append(boardSizeCode[i]);
            ++i;
        }

        ++i;
        int.TryParse(stringBuilder.ToString(), out var x);
        stringBuilder.Clear();

        while (i < boardSizeCode.Length)
        {
            stringBuilder.Append(boardSizeCode[i]);
            ++i;
        }

        int.TryParse(stringBuilder.ToString(), out var y);

        return new AbsChessboard(x, y);
    }

    private static AbsPieceToken CreateAbsPieceTokenFromCode(string pieceCode)
    {
        var stringBuilder = new StringBuilder();
        var i = 0;

        while (pieceCode[i] != ';')
        {
            stringBuilder.Append(pieceCode[i]);
            ++i;
        }

        ++i;
        int.TryParse(stringBuilder.ToString(), out var x);
        stringBuilder.Clear();
        
        while (pieceCode[i] != ';')
        {
            stringBuilder.Append(pieceCode[i]);
            ++i;
        }

        ++i;
        int.TryParse(stringBuilder.ToString(), out var y);

        var colorCode = pieceCode[i].ToString();
        i += 2;

        var typeCode = pieceCode[i].ToString();
        i += 2;

        var coordinates = new Vector2Int(x, y);
        var isFirstMove = pieceCode[i] == '1' ? true : false;
        var absPiece = CreateAbsPieceBasedOnPieceParameters(typeCode, colorCode, isFirstMove);

        return new AbsPieceToken(coordinates, absPiece, isFirstMove);
    }

    private static AbsPiece CreateAbsPieceBasedOnPieceParameters(string typeCode, string colorCode, bool isFirstMove)
    {
        if (typeCode == "p") return new AbsPawn(colorCode, isFirstMove);
        if (typeCode == "r") return new AbsRook(colorCode, isFirstMove);
        if (typeCode == "k") return new AbsKnight(colorCode, isFirstMove);
        if (typeCode == "b") return new AbsBishop(colorCode, isFirstMove);
        if (typeCode == "Q") return new AbsQueen(colorCode, isFirstMove);
        if (typeCode == "K") return new AbsKing(colorCode, isFirstMove);
        return null;
    }

    #endregion

    public static List<IRealSquare> GetMovesWithoutCheckForKing(IRealSquare squareWithPiece, ActionType actionType)
    {
        var movesWithoutCheck = new List<IRealSquare>();
        var piecesMoves = actionType == ActionType.Movement ? 
            squareWithPiece.PieceOnIt.GetMoves(squareWithPiece) : squareWithPiece.PieceOnIt.GetAttacks(squareWithPiece);

        var currentCode = EncodeRealBoard(squareWithPiece.Board);
        var kingColor = squareWithPiece.PieceOnIt.ColorCode;

        foreach (var pieceMove in piecesMoves)
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
        var allAttackMoves = new List<IRealSquare>();

        AddAllEnemiesAttackMoves(absBoard, allAttackMoves, kingColorCode);

        foreach (var attackMove in allAttackMoves)
        {
            var kingIsUnderAttack = attackMove.PieceOnIt.ColorCode == "K";

            if (kingIsUnderAttack) 
                return true;
        }

        return false;
    }

    private static void AddAllEnemiesAttackMoves(IChessBoard board, List<IRealSquare> allAttackMoves, string codeOfKingsColor)
    {
        for (int x = 0; x < board.Size.x; x++)
            for (int y = 0; y < board.Size.y; y++)
            {
                var absPiece = board.Squares[x, y].PieceOnIt;

                if (absPiece != null && absPiece.ColorCode != codeOfKingsColor)
                {
                    var squareWithAbsPiece = board.Squares[x, y];
                    var pieceAttackMoves = absPiece.GetAttacks(squareWithAbsPiece);

                    allAttackMoves.AddRange(pieceAttackMoves);
                }
            }
    }

    private static ChessCode GetCodeWhenPieceAttackFromSquareToOther(ChessCode current, IRealSquare from, IRealSquare to)
    {
        TryGetPieceStateCode(from, out var fromPieceState);
        TryGetPieceStateCode(to, out var toPieceState);
        var piecesState = current.PiecesState;

        var newPiecesState = piecesState.Replace(fromPieceState, "");
        newPiecesState = newPiecesState.Replace(toPieceState, fromPieceState);

        return new ChessCode(newPiecesState, current.BoardSize, current.WhoseTurn);
    }

    private static ChessCode GetCodeWhenPieceMovedFromSquareToOther(ChessCode current, IRealSquare from, IRealSquare to)
    {
        var fromCoordinates = GetCodeOfSquaresCoordinates(from);
        var toCoordinates = GetCodeOfSquaresCoordinates(to);
        var piecesState = current.PiecesState;

        var newPiecesState = piecesState.Replace(fromCoordinates, toCoordinates);

        return new ChessCode(newPiecesState, current.BoardSize, current.WhoseTurn);
    }

    private static string GetCodeOfSquaresCoordinates(IRealSquare square)
    {
        var x = square.Coordinates.x.ToString();
        var y = square.Coordinates.y.ToString();
        
        return $"{x};{y}";
    }
}

public class AbsPieceToken
{
    public readonly Vector2Int SquareCoordinates;
    public readonly IPiece ChessPiece;
    public readonly bool IsFirstMove;

    public AbsPieceToken(Vector2Int squareCoordinates, IPiece chessPiece, bool isFirstMove)
    {
        SquareCoordinates = squareCoordinates;
        ChessPiece = chessPiece;
        IsFirstMove = isFirstMove;
    }
} 