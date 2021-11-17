using System;
using System.Collections.Generic;
using UnityEngine;

public class GraphicDataStorage : MonoBehaviour
{
    public PieceGraphicData PawnGraphicData => _pawnGraphicData;
    [SerializeField] private PieceGraphicData _pawnGraphicData;
    
    public PieceGraphicData RookGraphicData => _rookGraphicData;
    [SerializeField] private PieceGraphicData _rookGraphicData;
    
    public PieceGraphicData KnightGraphicData => _knightGraphicData;
    [SerializeField] private PieceGraphicData _knightGraphicData;
    
    public PieceGraphicData BishopGraphicData => _bishopGraphicData;
    [SerializeField] private PieceGraphicData _bishopGraphicData;
    
    public PieceGraphicData QueenGraphicData => _queenGraphicData;
    [SerializeField] private PieceGraphicData _queenGraphicData;
    
    public PieceGraphicData KingGraphicData => _kingGraphicData;
    [SerializeField] private PieceGraphicData _kingGraphicData;

    private Dictionary<PieceTypes, PieceGraphicData> _pieceTypeGraphicDataDict =
        new Dictionary<PieceTypes, PieceGraphicData>();

    private void Awake()
    {
        _pieceTypeGraphicDataDict.Add(PieceTypes.Pawn, _pawnGraphicData);
        _pieceTypeGraphicDataDict.Add(PieceTypes.Rook, _rookGraphicData);
        _pieceTypeGraphicDataDict.Add(PieceTypes.Knight, _knightGraphicData);
        _pieceTypeGraphicDataDict.Add(PieceTypes.Bishop, _bishopGraphicData);
        _pieceTypeGraphicDataDict.Add(PieceTypes.Queen, _queenGraphicData);
        _pieceTypeGraphicDataDict.Add(PieceTypes.King, _kingGraphicData);
    }

    public PieceGraphicData TryGetGraphicDataByPieceType(PieceTypes type)
    {
        _pieceTypeGraphicDataDict.TryGetValue(type, out var value);
        return value;
    }
}

[Serializable]
public class PieceGraphicData
{
    public Sprite WhiteSprite;
    public Sprite BlackSprite;
    public GameObject WhiteEffects;
    public GameObject BlackEffects;
}