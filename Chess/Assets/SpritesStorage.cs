using UnityEngine;

public class SpritesStorage : MonoBehaviour
{
    public Sprite BlackPieceSprite => _blackPieceSprite;
    [SerializeField] private Sprite _blackPieceSprite;

    public Sprite WhitePieceSprite => _whitePieceSprite;
    [SerializeField] private Sprite _whitePieceSprite;
}