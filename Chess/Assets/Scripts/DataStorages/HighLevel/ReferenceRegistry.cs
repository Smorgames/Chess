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

    public PieceHighlighter MyPieceHighlighter => pieceHighlighter;
    [SerializeField] private PieceHighlighter pieceHighlighter;

    public PawnPromotion MyPawnPromotion => _pawnPromotion;
    [SerializeField] private PawnPromotion _pawnPromotion;

    public SpritesStorage MySpritesStorage => _spritesStorage;
    [SerializeField] private SpritesStorage _spritesStorage;
}