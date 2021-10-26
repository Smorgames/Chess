using System.Collections.Generic;
using UnityEngine;

public class PrefabsStorage : MonoBehaviour
{    
    public Pawn Pawn => _pawn;
    [SerializeField] private Pawn _pawn;
    public Rook Rook => _rook;
    [SerializeField] private Rook _rook;
    public Knight Knight => _knight;
    [SerializeField] private Knight _knight;
    public Bishop Bishop => _bishop;
    [SerializeField] private Bishop _bishop;
    public Queen Queen => _queen;
    [SerializeField] private Queen _queen;
    public King King => _king;
    [SerializeField] private King _king;
    
    public List<GameObject> SquaresPrefabs => _squaresPrefabs;
    [Header("Squares prefabs")]
    [SerializeField] private List<GameObject> _squaresPrefabs;
    public RealSquare GhostRealSquare => ghostRealSquare;
    [SerializeField] private RealSquare ghostRealSquare;
}