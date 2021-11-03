using UnityEngine;
using UnityEngine.UI;

public class PieceReplacerButton : MonoBehaviour
{
    [SerializeField] private Piece _pieceThatReplaces;

    public Sprite WhiteSprite => _whiteSprite;
    [SerializeField] private Sprite _whiteSprite;
    public Sprite BlackSprite => _blackSprite;
    [SerializeField] private Sprite _blackSprite;

    [SerializeField] private Image _image; 
    
    private Square _squareWithPieceNeedReplace;

    public void SetSprite(string colorCode) => _image.sprite = colorCode == "w" ? _whiteSprite : _blackSprite;
    public void SetSquareWithPieceNeedReplace(Square square) => _squareWithPieceNeedReplace = square;
    
    public void Replace()
    {
        if (_squareWithPieceNeedReplace == null) return;

        var pawn = _squareWithPieceNeedReplace.PieceOnIt;
        
        var piece = Instantiate(_pieceThatReplaces, transform.position, Quaternion.identity);
        piece.Init(pawn.ColorCode, false);
        GameManager.Instance.PlayerWhoseTurn.RemovePiece(pawn);
        pawn.Death();
        GameManager.Instance.PlayerWhoseTurn.AddPiece(piece);
        piece.transform.position = _squareWithPieceNeedReplace.transform.position + Piece.Offset;
        _squareWithPieceNeedReplace.PieceOnIt = piece;
        GameManager.Instance.CurrentState = GameManager.GameState.Playing;
        Piece.OnPieceMoved?.Invoke(piece, new PieceMovedEventArgs());
        _squareWithPieceNeedReplace = null;
        ReferenceRegistry.Instance.MyPawnPromotion.Deactivate();
    }
}