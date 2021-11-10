using System;
using System.Collections.Generic;
using UnityEngine;

public class SpritesStorage : MonoBehaviour
{
    public PieceSpriteCouple PawnSprites => _pawnSprites;
    [SerializeField] private PieceSpriteCouple _pawnSprites;
    
    public PieceSpriteCouple RookSprites => _rookSprites;
    [SerializeField] private PieceSpriteCouple _rookSprites;
    
    public PieceSpriteCouple KnightSprites => _knightSprites;
    [SerializeField] private PieceSpriteCouple _knightSprites;
    
    public PieceSpriteCouple BishopSprites => _bishopSprites;
    [SerializeField] private PieceSpriteCouple _bishopSprites;
    
    public PieceSpriteCouple QueenSprites => _queenSprites;
    [SerializeField] private PieceSpriteCouple _queenSprites;
    
    public PieceSpriteCouple KingSprites => _kingSprites;
    [SerializeField] private PieceSpriteCouple _kingSprites;

    private Dictionary<PieceTypes, PieceSpriteCouple> _pieceTypeSpritesDict =
        new Dictionary<PieceTypes, PieceSpriteCouple>();

    private void Awake()
    {
        _pieceTypeSpritesDict.Add(PieceTypes.Pawn, _pawnSprites);
        _pieceTypeSpritesDict.Add(PieceTypes.Rook, _rookSprites);
        _pieceTypeSpritesDict.Add(PieceTypes.Knight, _knightSprites);
        _pieceTypeSpritesDict.Add(PieceTypes.Bishop, _bishopSprites);
        _pieceTypeSpritesDict.Add(PieceTypes.Queen, _queenSprites);
        _pieceTypeSpritesDict.Add(PieceTypes.King, _kingSprites);
    }

    public PieceSpriteCouple TryGetSpritesByPieceType(PieceTypes type)
    {
        _pieceTypeSpritesDict.TryGetValue(type, out var value);
        return value;
    }
}

[Serializable]
public class PieceSpriteCouple
{
    public Sprite White;
    public Sprite Black;
}