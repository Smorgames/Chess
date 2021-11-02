using System.Collections.Generic;
using UnityEngine;

public class PrefabsStorage : MonoBehaviour
{    
    public NewPawn Pawn => _pawn;
    [Header("Chess piece prefabs")]
    [SerializeField] private NewPawn _pawn;
    public NewRook Rook => _rook;
    [SerializeField] private NewRook _rook;
    public NewKnight Knight => _knight;
    [SerializeField] private NewKnight _knight;
    public NewBishop Bishop => _bishop;
    [SerializeField] private NewBishop _bishop;
    public NewQueen Queen => _queen;
    [SerializeField] private NewQueen _queen;
    public NewKing King => _king;
    [SerializeField] private NewKing _king;
    
    public List<GameObject> SquaresPrefabs => _squaresPrefabs;
    [Header("Squares prefabs")]
    [SerializeField] private List<GameObject> _squaresPrefabs;
    // public RealSquare GhostRealSquare => ghostRealSquare;
    // [SerializeField] private RealSquare ghostRealSquare;
}