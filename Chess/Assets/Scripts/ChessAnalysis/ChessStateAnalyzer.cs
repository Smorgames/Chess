using System;
using System.Collections.Generic;
using System.Text;
using AbstractChess;
using AnalysisOfChessState.Analyzer;
using AnalysisOfChessState.CodeHandler;
using AnalysisOfChessState.Recreator;
using UnityEngine;

namespace AnalysisOfChessState
{
    public class ChessStateAnalyzer
    {
        private CheckAnalyzer _analyzer;
        private StateRecreator _recreator;

        public ChessStateAnalyzer()
        {
            _analyzer = new CheckAnalyzer();
            _recreator = new StateRecreator();
        }

        public Chessboard ArrangePiecesOnChessboard(Chessboard board, ChessCode code)
        {
            var tokens = StateCodeHandler.GetTokens(code);
            _recreator.ArrangePiecesOnRealBoard(tokens, board);
            return board;
        }

        public List<Square> GetMovesWithoutCheck(Square square, List<Square> supposedPieceMoves, ActionType actionType)
        {
            var movesWithoutCheck = new List<Square>();
            var boardSize = new Vector2Int(square.Board.Size.x, square.Board.Size.y);
            var code = StateCodeHandler.GetStateCode(square.Board);
            var pieceCoordinates = square.Coordinates.x.ToString() + square.Coordinates.y.ToString();
                        
            foreach (var supposedMove in supposedPieceMoves)
            {
                var newPieceCoordinates = supposedMove.Coordinates.x.ToString() + supposedMove.Coordinates.y.ToString();
                var newCodeValue = code.PiecesState;
                
                if (actionType == ActionType.Attack)
                {
                    var index = code.PiecesState.IndexOf(newPieceCoordinates, StringComparison.Ordinal);
                    var sb = new StringBuilder();

                    for (int i = index; i <= index + 3; i++)
                        sb.Append(code.PiecesState[i]);

                    newCodeValue = newCodeValue.Replace(sb.ToString(), "");
                }
                
                newCodeValue = newCodeValue.Replace(pieceCoordinates, newPieceCoordinates);
            
                var newCode = new ChessCode(newCodeValue);
                var check = IsCheckForAbstractKing(newCode, boardSize, square.PieceOnIt.MyColor);
            
                if (!check)
                    movesWithoutCheck.Add(supposedMove);
            }
                    
            return movesWithoutCheck;
        }
        
        // public List<Square> GetMovesWithoutCheck(AbsSquare square, List<AbsSquare> supposedPieceMoves, ActionType actionType)
        // {
        //     var movesWithoutCheck = new List<AbsSquare>();
        //     var boardSize = new Vector2Int(square.Board.Length, square.Board.Height);
        //     var code = _codeHandler.GetStateCode(square.Board);
        //     var pieceCoordinates = square.Coordinates.x.ToString() + square.Coordinates.y.ToString();
        //                 
        //     foreach (var supposedMove in supposedPieceMoves)
        //     {
        //         var newPieceCoordinates = supposedMove.Coordinates.x.ToString() + supposedMove.Coordinates.y.ToString();
        //         var newCodeValue = code.Value;
        //         
        //         if (actionType == ActionType.Attack)
        //         {
        //             var index = code.Value.IndexOf(newPieceCoordinates, StringComparison.Ordinal);
        //             var sb = new StringBuilder();
        //
        //             for (int i = index; i <= index + 3; i++)
        //                 sb.Append(code.Value[i]);
        //
        //             newCodeValue = newCodeValue.Replace(sb.ToString(), "");
        //         }
        //         
        //         newCodeValue = newCodeValue.Replace(pieceCoordinates, newPieceCoordinates);
        //     
        //         var newCode = new StateCode(newCodeValue);
        //         var check = IsCheckForAbstractKing(newCode, boardSize, square.AbsPieceOnIt.MyColor);
        //     
        //         if (!check)
        //             movesWithoutCheck.Add(supposedMove);
        //     }
        //             
        //     return movesWithoutCheck;
        // }

        public bool IsCheckForAbstractKing(ChessCode chessChessCode, Vector2Int boardSize, PieceColor kingColor)
        {
            var tokens = StateCodeHandler.GetTokens(chessChessCode);
            var board = _recreator.GetAbsChessboard(boardSize, tokens);
            return _analyzer.CheckForAbstractKing(board, kingColor);
        }

        public bool MateForAbstractKing(Chessboard board, PieceColor kingColor)
        {
            var boardSize = board.Size;
            var tokens = StateCodeHandler.GetTokens(board);
            
            var absBoard = _recreator.GetAbsChessboard(boardSize, tokens);

            return MateForAbstractKing(absBoard, kingColor);
        }
        
        public bool MateForAbstractKing(ChessCode chessCode, Vector2Int boardSize, PieceColor kingColor)
        {
            var tokens = StateCodeHandler.GetTokens(chessCode);
            var absBoard = _recreator.GetAbsChessboard(boardSize, tokens);

            return MateForAbstractKing(absBoard, kingColor);
        }
        
        public bool MateForAbstractKing(AbsChessboard absBoard, PieceColor kingColor)
        {
            var pieceMoves = new List<AbsSquare>();

            foreach (var square in absBoard.Squares)
            {
                var absPiece = square.AbsPieceOnIt;

                if (absPiece == null || absPiece.MyColor != kingColor) 
                    continue;
                
                pieceMoves = absPiece.PossibleAttackMoves(square);
                pieceMoves.AddRange(absPiece.PossibleMoves(square));

                return pieceMoves.Count <= 0;
            }
            
            return pieceMoves.Count <= 0;
        }

        public AbsChessboard AbsBoardBasedOnCode(ChessCode code, Vector2Int boardSize)
        {
            var absBoard = new AbsChessboard(boardSize);
        
            for (int i = 0; i < code.PiecesState.Length; ++i)
            {
                if (code.PiecesState[i] == '[')
                {
                    var coordinates = new Vector2Int();
                    var pieceColor = PieceColor.Black;
                    var pieceType = PieceType.Bishop;
                    var isFirstMove = true;
                    
                    for (int j = i + 1; j < code.PiecesState.Length; ++j)
                    {
                        if (code.PiecesState[j] == ']')
                            break;
                        
                        var semicolonCount = 0;
        
                        if (code.PiecesState[j] == ';')
                            ++semicolonCount;
        
                        if (semicolonCount == 0)
                        {
                            var stringBuilder = new StringBuilder();

                            for (int k = j; k < code.PiecesState.Length; k++)
                            {
                                if (code.PiecesState[k] == ';')
                                {
                                    coordinates.x = int.Parse(stringBuilder.ToString());
                                    ++semicolonCount;
                                    j = k;
                                    break;
                                }
                                
                                stringBuilder.Append(code.PiecesState[k].ToString());
                            }
                        }
                        if (semicolonCount == 1)
                        {
                            var stringBuilder = new StringBuilder();

                            for (int k = j; k < code.PiecesState.Length; k++)
                            {
                                if (code.PiecesState[k] == ';')
                                {
                                    coordinates.y = int.Parse(stringBuilder.ToString());
                                    ++semicolonCount;
                                    j = k;
                                    break;
                                }
                                
                                stringBuilder.Append(code.PiecesState[k].ToString());
                            }
                        }
                        if (semicolonCount == 2)
                        {
                            if (code.PiecesState[j] == 'w') pieceColor = PieceColor.White;
                            if (code.PiecesState[j] == 'b') pieceColor = PieceColor.Black;
                            ++semicolonCount;
                            ++j;
                        }
                        if (semicolonCount == 3)
                        {
                            if (code.PiecesState[j] == 'p') pieceType = PieceType.Pawn;
                            if (code.PiecesState[j] == 'r') pieceType = PieceType.Rook;
                            if (code.PiecesState[j] == 'k') pieceType = PieceType.Knight;
                            if (code.PiecesState[j] == 'b') pieceType = PieceType.Bishop;
                            if (code.PiecesState[j] == 'Q') pieceType = PieceType.Queen;
                            if (code.PiecesState[j] == 'K') pieceType = PieceType.King;
                            ++semicolonCount;
                            ++j;
                        }
                        if (semicolonCount == 4)
                        {
                            isFirstMove = code.PiecesState[j] == '1' ? true : false;
                            ++semicolonCount;
                            ++j;
                        }

                        var piece = GetAbsPiece(pieceType, pieceColor);
                        piece.IsFirstMove = isFirstMove;

                        absBoard.Squares[coordinates.x, coordinates.y].AbsPieceOnIt = piece;
                    }
                }
            }

            return absBoard;
        }

        private AbsPiece GetAbsPiece(PieceType pieceType, PieceColor pieceColor)
        {
            if (pieceType == PieceType.Pawn) return new AbsPawn(pieceColor);
            if (pieceType == PieceType.Rook) return new AbsRook(pieceColor);
            if (pieceType == PieceType.Knight) return new AbsKnight(pieceColor);
            if (pieceType == PieceType.Bishop) return new AbsBishop(pieceColor);
            if (pieceType == PieceType.Queen) return new AbsQueen(pieceColor);
            if (pieceType == PieceType.King) return new AbsKing(pieceColor);
            return null;
        }
    }
}