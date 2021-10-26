﻿using System.Collections.Generic;
using UnityEngine;

public class PrefabsStorage : MonoBehaviour
{
    public Pawn BlackPawn => _blackPawn;
    [Header("Black chess pieces")]
    [SerializeField] private Pawn _blackPawn;
    public Rook BlackRook => _blackRook;
    [SerializeField] private Rook _blackRook;
    public Knight BlackKnight => _blackKnight;
    [SerializeField] private Knight _blackKnight;
    public Bishop BlackBishop => _blackBishop;
    [SerializeField] private Bishop _blackBishop;
    public Queen BlackQueen => _blackQueen;
    [SerializeField] private Queen _blackQueen;
    public King BlackKing => _blackKing;
    [SerializeField] private King _blackKing;
    
    public Pawn WhitePawn => _whitePawn;
    [Header("White chess pieces")]
    [SerializeField] private Pawn _whitePawn;
    public Rook WhiteRook => _whiteRook;
    [SerializeField] private Rook _whiteRook;
    public Knight WhiteKnight => _whiteKnight;
    [SerializeField] private Knight _whiteKnight;
    public Bishop WhiteBishop => _whiteBishop;
    [SerializeField] private Bishop _whiteBishop;
    public Queen WhiteQueen => _whiteQueen;
    [SerializeField] private Queen _whiteQueen;
    public King WhiteKing => _whiteKing;
    [SerializeField] private King _whiteKing;
    
    public List<GameObject> SquaresPrefabs => _squaresPrefabs;
    [Header("Squares prefabs")]
    [SerializeField] private List<GameObject> _squaresPrefabs;
    public RealSquare GhostRealSquare => ghostRealSquare;
    [SerializeField] private RealSquare ghostRealSquare;
}