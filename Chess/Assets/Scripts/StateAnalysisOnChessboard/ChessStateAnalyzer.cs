using AbstractChess;
using AnalysisOfChessState.Analyzer;
using AnalysisOfChessState.Parser;
using AnalysisOfChessState.Recreator;
using UnityEngine;

namespace AnalysisOfChessState
{
    public class ChessStateAnalyzer
    {
        private CheckAnalyzer _analyzer;
        private ChessParser _parser;
        private ChessStateRecreator _recreator;

        public ChessStateAnalyzer()
        {
            _analyzer = new CheckAnalyzer();
            _parser = new ChessParser();
            _recreator = new ChessStateRecreator();
        }

        public bool IsCheckForAbstractKing(AbsChessboard abstractBoard, PieceColor kingColor)
        {
            return _analyzer.IsCheckForAbsKing(abstractBoard, kingColor);
        }

        public bool IsCheckForAbstractKing(string chessStateCode, Vector2Int boardSize, PieceColor kingColor)
        {
            var tokens = _parser.Parse(chessStateCode);
            var board = _recreator.CreateAbsBoardAndRecreateChessState(boardSize, tokens);
            return IsCheckForAbstractKing(board, kingColor);
        }
    }
}