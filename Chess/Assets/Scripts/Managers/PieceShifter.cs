using System.Linq;
using UnityEngine;

public class PieceShifter : MonoBehaviour
{
    private Square _fromSquare;
    private Square _toSquare;

    private void SelectSquareFromGo(object s, ActivePieceClickedArgs a)
    {
        _fromSquare = a.MySquare;
    }

    private void ClickedOnEmptySquare(object s, EmptySquareClickedArgs a)
    {
        if (_fromSquare == null) return;

        if (PieceCanGoToSquare(a.MySquare))
            _toSquare = a.MySquare;

        if (_fromSquare != null && _toSquare != null)
            MoveChessPiece();

        ResetChessBoardState(a.MySquare);
    }
    private void ResetChessBoardState(Square square)
    {
        ReferenceRegistry.Instance.MyPieceHighlighter.DeactivateAllSquaresHighlights(square.Board);
        ResetSquareFields();
    }
    private void ResetSquareFields() => _fromSquare = _toSquare = null;

    private void SelectSquareToGo(object s, InactivePieceClickedArgs a)
    {
        if (PieceCanGoToSquare(a.MySquare))
            _toSquare = a.MySquare;

        if (_fromSquare != null && _toSquare != null)
            MoveChessPiece();

        ResetChessBoardState(a.MySquare);
    }
    private bool PieceCanGoToSquare(Square square) => ReferenceRegistry.Instance.MyPieceHighlighter.PieceMoves.Any(move => square == move);
    private void MoveChessPiece()
    {
        var piece = _fromSquare.PieceOnIt;
        _fromSquare.PieceOnIt = null;
        
        var enemyPiece = _toSquare.PieceOnIt;
        if (enemyPiece != null)
        {
            var player = Game.Instance.GetPlayerBasedOnColorCode(enemyPiece.ColorCode);
            player.RemovePiece(enemyPiece);
            enemyPiece.Death();
        }
        _toSquare.PieceOnIt = piece;

        if (piece is King king)
        {
            foreach (var castlingMove in king.CastlingMoves)
            {
                if (_toSquare == castlingMove.KingSquare)
                {
                    var rook = castlingMove.CastleRook;
                    var rookSquare = castlingMove.RookSquare;

                    rookSquare.Board.SquareWithPiece(rook).PieceOnIt = null;
                    rook.ChangePosition(rookSquare);
                    rookSquare.PieceOnIt = rook;
                }
            }
        }

        if (piece is Pawn pawn)
        {
            if (_toSquare.MyEnPassant.Possible && pawn.ColorCode != _toSquare.MyEnPassant.MyPawn.ColorCode)
            {
                var enemyPawn = _toSquare.MyEnPassant.MyPawn;
                var player = Game.Instance.GetPlayerBasedOnColorCode(enemyPawn.ColorCode);
                player.RemovePiece(enemyPawn);
                enemyPawn.Death();
            }

            if (pawn.IsFirstMove)
            {
                var board = Game.Instance.GameBoard;
                var yMoveFactor = pawn.ColorCode == "w" ? 1 : -1;

                if (Mathf.Abs(_fromSquare.Coordinates.y - _toSquare.Coordinates.y) == 2)
                {
                    var x = _toSquare.Coordinates.x;
                    var y = _fromSquare.Coordinates.y + yMoveFactor;
                    var enPassantSquare = board.SquareWithCoordinates(x, y);

                    enPassantSquare.ActivateEnPassant(pawn);
                }
            }
        }
        
        piece.MoveTo(_toSquare);
    }

    #region Events
    private void Start()
    {
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        Square.OnPieceWhoseTurnNowClicked += SelectSquareFromGo;
        Square.OnPieceWhoNotTurnNowClicked += SelectSquareToGo;
        Square.OnEmptySquareClicked += ClickedOnEmptySquare;
    }

    private void UnsubscribeEvents()
    {
        Square.OnPieceWhoseTurnNowClicked -= SelectSquareFromGo;
        Square.OnPieceWhoNotTurnNowClicked -= SelectSquareToGo;
        Square.OnEmptySquareClicked -= ClickedOnEmptySquare;
    }
    #endregion
}