using System.Collections.Generic;
using AbstractChess;
using UnityEngine;

namespace AnalysisOfChessState.Recreator
{
    public class StateRecreator
{
    private PrefabsStorage _storage;

    public StateRecreator()
    {
        _storage = SingletonRegistry.Instance.PrefabsStorage;
    }

    public void ArrangePiecesOnRealBoard(List<PieceToken> tokens, Chessboard realBoard)
    {
        var dict = GetRealPieceCoordinateDict(tokens);

        foreach (var square in realBoard.Squares)
        {
            if (square.PieceOnIt != null)
                Object.Destroy(square.PieceOnIt);
            
            var squareWithCertainCoordinatesHasPiece = dict.TryGetValue(square.Coordinates, out var piece);

            if (squareWithCertainCoordinatesHasPiece)
            {
                square.PieceOnIt = piece;
                piece.transform.position = square.transform.position + Piece.Offset;
            }
        }
    }

    private Dictionary<Vector2Int, Piece> GetRealPieceCoordinateDict(List<PieceToken> tokens)
    {
        var dict = new Dictionary<Vector2Int, Piece>();

        foreach (var token in tokens)
        {
            var piece = GetRealPieceBasedOnType(token);
            var coordinates = token.Coordinates;

            dict.Add(coordinates, piece);
        }

        return dict;
    }

    private Piece GetRealPieceBasedOnType(PieceToken token)
    {
        switch (token.MyPieceType)
        {
            case PieceType.Pawn:
                if (token.MyPieceColor == PieceColor.Black)
                    return Object.Instantiate(_storage.BlackPawn, Vector3.zero, Quaternion.identity)
                        .GetComponent<Pawn>();
                else
                    return Object.Instantiate(_storage.WhitePawn, Vector3.zero, Quaternion.identity)
                        .GetComponent<Pawn>();
            case PieceType.Rook:
                if (token.MyPieceColor == PieceColor.Black)
                    return Object.Instantiate(_storage.BlackRook, Vector3.zero, Quaternion.identity)
                        .GetComponent<Rook>();
                else
                    return Object.Instantiate(_storage.WhiteRook, Vector3.zero, Quaternion.identity)
                        .GetComponent<Rook>();
            case PieceType.Knight:
                if (token.MyPieceColor == PieceColor.Black)
                    return Object.Instantiate(_storage.BlackKnight, Vector3.zero, Quaternion.identity)
                        .GetComponent<Knight>();
                else
                    return Object.Instantiate(_storage.WhiteKnight, Vector3.zero, Quaternion.identity)
                        .GetComponent<Knight>();
            case PieceType.Bishop:
                if (token.MyPieceColor == PieceColor.Black)
                    return Object.Instantiate(_storage.BlackBishop, Vector3.zero, Quaternion.identity)
                        .GetComponent<Bishop>();
                else
                    return Object.Instantiate(_storage.WhiteBishop, Vector3.zero, Quaternion.identity)
                        .GetComponent<Bishop>();
            case PieceType.Queen:
                if (token.MyPieceColor == PieceColor.Black)
                    return Object.Instantiate(_storage.BlackQueen, Vector3.zero, Quaternion.identity)
                        .GetComponent<Queen>();
                else
                    return Object.Instantiate(_storage.WhiteQueen, Vector3.zero, Quaternion.identity)
                        .GetComponent<Queen>();
            case PieceType.King:
                if (token.MyPieceColor == PieceColor.Black)
                    return Object.Instantiate(_storage.BlackKing, Vector3.zero, Quaternion.identity)
                        .GetComponent<King>();
                else
                    return Object.Instantiate(_storage.WhiteKing, Vector3.zero, Quaternion.identity)
                        .GetComponent<King>();
        }

        return null;
    }

    public AbsChessboard GetAbsChessboard(Vector2Int chessboardSize, List<PieceToken> chessTokens)
    {
        var abstractChessboard = new AbsChessboard(chessboardSize);
        var dict = GetDictionaryOfAbstractPieces(chessTokens);

        foreach (var square in abstractChessboard.Squares)
            square.AbsPieceOnIt = null;


        foreach (var pair in dict)
        {
            var square = abstractChessboard.GetSquareBasedOnCoordinates(pair.Key);
            square.AbsPieceOnIt = pair.Value;
        }

        return abstractChessboard;
    }
    
    private Dictionary<Vector2Int, AbsPiece> GetDictionaryOfAbstractPieces(List<PieceToken> chessTokens)
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