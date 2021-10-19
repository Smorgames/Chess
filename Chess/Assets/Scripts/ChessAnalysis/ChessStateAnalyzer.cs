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
        private StateCodeHandler _codeHandler;
        private StateRecreator _recreator;

        public ChessStateAnalyzer()
        {
            _analyzer = new CheckAnalyzer();
            _codeHandler = new StateCodeHandler();
            _recreator = new StateRecreator();
        }

        public Chessboard ArrangePiecesOnChessboard(Chessboard board, StateCode code)
        {
            var tokens = _codeHandler.GetTokens(code);
            _recreator.ArrangePiecesOnRealBoard(tokens, board);
            return board;
        }

        public List<Square> GetCorrectMoves(Chessboard board, Square squareWithPiece, List<Square> pieceMoves)
        {
            var correctMoves = new List<Square>();
            
            var boardSize = new Vector2Int(board.Size.x, board.Size.y);
            
            var tokens = _codeHandler.GetTokens(board);
            var code = _codeHandler.GetStateCode(tokens);
        
            foreach (var move in pieceMoves)
            {
                var oldCoord = squareWithPiece.Coordinates.x.ToString() + squareWithPiece.Coordinates.y.ToString();
                var oldPosIndex = code.Value.IndexOf(oldCoord, StringComparison.Ordinal);
                
                var newCoord = move.Coordinates.x.ToString() + move.Coordinates.y.ToString();

                var sb = new StringBuilder();

                for (int i = 0; i < code.Value.Length; i++)
                {
                    var letter = code.Value[i].ToString();
                    
                    if (i == oldPosIndex)
                    {
                        letter = newCoord;
                        ++i;
                    }

                    sb.Append(letter);
                }

                var newCode = new StateCode(sb.ToString());
                var check = IsCheckForAbstractKing(newCode, boardSize, squareWithPiece.PieceOnSquare.ColorData.Color);

                if (!check)
                    correctMoves.Add(move);
            }
        
            return correctMoves;
        }

        public bool IsCheckForAbstractKing(StateCode chessStateCode, Vector2Int boardSize, PieceColor kingColor)
        {
            var tokens = _codeHandler.GetTokens(chessStateCode);
            var board = _recreator.GetAbsChessboard(boardSize, tokens);
            return _analyzer.IsCheckForAbsKing(board, kingColor);
        }
    }
}