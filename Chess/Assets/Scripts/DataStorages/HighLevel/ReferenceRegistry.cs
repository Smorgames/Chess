using UnityEngine;

public class ReferenceRegistry : MonoBehaviour
{
    #region Singleton

    public static ReferenceRegistry Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    #endregion
    
    public PrefabsStorage MyPrefabsStorage => _prefabsStorage;
    [SerializeField] private PrefabsStorage _prefabsStorage;

    public PieceMovesHighlighter MyPieceMovesHighlighter => _pieceMovesHighlighter;
    [SerializeField] private PieceMovesHighlighter _pieceMovesHighlighter;
}