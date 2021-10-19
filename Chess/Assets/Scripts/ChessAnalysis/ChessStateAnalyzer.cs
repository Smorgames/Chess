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

        public List<Square> GetMovesWithoutCheck(Chessboard board, Square squareWithPiece, List<Square> supposedPieceMoves, ActionType actionType)
        {
            var movesWithoutCheck = new List<Square>();
                        
            var boardSize = new Vector2Int(board.Size.x, board.Size.y);
                        
            var tokens = _codeHandler.GetTokens(board);
            var code = _codeHandler.GetStateCode(tokens);
                        
            var pieceCoordinates = squareWithPiece.Coordinates.x.ToString() + squareWithPiece.Coordinates.y.ToString();
                        
            foreach (var supposedMove in supposedPieceMoves)
            {
                var newPieceCoordinates = supposedMove.Coordinates.x.ToString() + supposedMove.Coordinates.y.ToString();
                var newCodeValue = code.Value;
                
                if (actionType == ActionType.AttackMove)
                {
                    var index = code.Value.IndexOf(newPieceCoordinates, StringComparison.Ordinal);
                    var sb = new StringBuilder();

                    for (int i = index; i <= index + 3; i++)
                        sb.Append(code.Value[i]);

                    newCodeValue = newCodeValue.Replace(sb.ToString(), "");
                }
                
                newCodeValue = newCodeValue.Replace(pieceCoordinates, newPieceCoordinates);
            
                var newCode = new StateCode(newCodeValue);
                var check = IsCheckForAbstractKing(newCode, boardSize, squareWithPiece.PieceOnSquare.MyColor);
            
                if (!check)
                    movesWithoutCheck.Add(supposedMove);
            }
                    
            return movesWithoutCheck;
        }

        public bool IsCheckForAbstractKing(StateCode chessStateCode, Vector2Int boardSize, PieceColor kingColor)
        {
            var tokens = _codeHandler.GetTokens(chessStateCode);
            var board = _recreator.GetAbsChessboard(boardSize, tokens);
            return _analyzer.IsCheckForAbsKing(board, kingColor);
        }
    }
}