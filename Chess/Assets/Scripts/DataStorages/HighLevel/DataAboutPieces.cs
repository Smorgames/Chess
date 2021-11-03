using System;
using System.Collections.Generic;
using UnityEngine;

public static class DataAboutPieces
{
    private static Dictionary<string, Piece> _pieceCodeAndPiecePrefabDict = new Dictionary<string, Piece>();

    public static Piece GetPiecePrefabFromPieceCode(string typeCode)
    {
        if (_pieceCodeAndPiecePrefabDict.Count == 0)
            FillPieceCodeAndPiecePrefabDict();

        if (_pieceCodeAndPiecePrefabDict.TryGetValue(typeCode, out var prefab)) return prefab;
        Debug.LogError($"Incorrect {nameof(typeCode)} value!");
        return null;
    }

    private static void FillPieceCodeAndPiecePrefabDict()
    {
        var storage = ReferenceRegistry.Instance.MyPrefabsStorage;

        var keyValueList = new List<Tuple<string, Piece>>()
        {
            new Tuple<string, Piece>("p", storage.Pawn),
            new Tuple<string, Piece>("r", storage.Rook),
            new Tuple<string, Piece>("n", storage.Knight),
            new Tuple<string, Piece>("b", storage.Bishop),
            new Tuple<string, Piece>("q", storage.Queen),
            new Tuple<string, Piece>("k", storage.King)
        };

        foreach (var keyValue in keyValueList)
            _pieceCodeAndPiecePrefabDict.Add(keyValue.Item1, keyValue.Item2);
    }
}