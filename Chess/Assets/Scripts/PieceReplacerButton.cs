using UnityEngine;
using UnityEngine.UI;

public class PieceReplacerButton : MonoBehaviour
{
    [SerializeField] private Piece _pieceThatReplaces;
    [SerializeField] private Image _image;
    [SerializeField] private PieceTypes _pieceType;
        
    private Sprite _whiteSprite;
    private Sprite _blackSprite;
    
    private Square _squareWithPieceNeedReplace;

    public void SetSprite(string colorCode)
    {
        if (_whiteSprite == null || _blackSprite == null)
        {
            var spritesStorage = ReferenceRegistry.Instance.MySpritesStorage;
            var spritesCouple = spritesStorage.TryGetGraphicDataByPieceType(_pieceType);
            _whiteSprite = spritesCouple.WhiteSprite;
            _blackSprite = spritesCouple.BlackSprite;
        }
        
        _image.sprite = colorCode == "w" ? _whiteSprite : _blackSprite;
    }
    public void SetSquareWithPieceNeedReplace(Square square) => _squareWithPieceNeedReplace = square;
    
    public void Replace()
    {
        if (_squareWithPieceNeedReplace == null) return;

        var pawn = _squareWithPieceNeedReplace.PieceOnIt;
        
        var piece = Instantiate(_pieceThatReplaces, transform.position, Quaternion.identity);
        piece.Init(pawn.ColorCode, false);
        Game.Instance.PlayerWhoseTurn.RemovePiece(pawn);
        pawn.Death();
        Game.Instance.PlayerWhoseTurn.AddPiece(piece);
        piece.transform.position = _squareWithPieceNeedReplace.transform.position + Piece.Offset;
        _squareWithPieceNeedReplace.PieceOnIt = piece;
        Game.Instance.CurrentState = Game.GameState.Playing;
        Piece.OnPieceMoved?.Invoke(piece, new PieceMovedEventArgs());
        _squareWithPieceNeedReplace = null;
        ReferenceRegistry.Instance.MyPawnPromotion.Deactivate();
    }
}