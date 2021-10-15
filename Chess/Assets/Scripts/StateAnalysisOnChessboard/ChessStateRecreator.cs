using System.Collections.Generic;
using AbstractChess;
using UnityEngine;

namespace AnalysisOfChessState.Recreator
{
    public class ChessStateRecreator
    {
        private readonly ChessPieceCreator _chessPieceCreator;
        
        public ChessStateRecreator()
        {
            _chessPieceCreator = new ChessPieceCreator();
        }
        
        public AbsChessboard RecreateChessState(AbsChessboard abstractAbsChessboard, List<ChessToken> chessTokens)
        {
            var dict = _chessPieceCreator.GetDictionaryOfAbstractPieces(chessTokens);
                
            foreach (var pair in dict)
            {
                var square = abstractAbsChessboard.GetSquareBasedOnCoordinates(pair.Key);
                square.AbsPieceOnThisSquare = pair.Value;
            }

            return abstractAbsChessboard;
        }
        
        public AbsChessboard CreateAbsBoardAndRecreateChessState(Vector2Int chessboardSize, List<ChessToken> chessTokens)
        {
            var abstractChessboard = new AbsChessboard(chessboardSize);
            return RecreateChessState(abstractChessboard, chessTokens);
        }
        
        private class ChessPieceCreator
        {
            public Dictionary<Vector2Int, AbsPiece> GetDictionaryOfAbstractPieces(List<ChessToken> chessTokens)
            {
                var dict = new Dictionary<Vector2Int, AbsPiece>();

                foreach (var token in chessTokens)
                {
                    AbsPiece piece;
                    
                    switch (token.MyPieceType)
                    {
                        case PieceType.Pawn:
                            piece = new AbsPawn(token.MyPieceColor);
                            break;
                        case PieceType.Rook:
                            piece = new AbsRook(token.MyPieceColor);
                            break;
                        case PieceType.Knight:
                            piece = new AbsKnight(token.MyPieceColor);
                            break;
                        case PieceType.Bishop:
                            piece = new AbsBishop(token.MyPieceColor);
                            break;
                        case PieceType.Queen:
                            piece = new AbsQueen(token.MyPieceColor);
                            break;
                        case PieceType.King:
                            piece = new AbsKing(token.MyPieceColor);
                            break;
                        default:
                            Debug.LogError($"{nameof(token)} of type {nameof(PieceType)} is never assign");
                            return null;
                    }

                    dict.Add(token.Coordinates, piece);
                }

                return dict;
            }
        }
    }
}
