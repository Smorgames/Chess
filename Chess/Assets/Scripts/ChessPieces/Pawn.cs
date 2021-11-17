using System.Collections.Generic;

public class Pawn : Piece
{
    public override string TypeCode => "p";

    private int _verticalMoveMultiplier => ColorCode == "w" ? 1 : -1;

    public override void UpdateSupposedMoves(Square squareWithPiece)
    {
        if (squareWithPiece == null) return;
        SupposedMoves.Clear();

        var x = squareWithPiece.Coordinates.x;
        var y = squareWithPiece.Coordinates.y;
        
        var firstAttackSquare = squareWithPiece.MyBoard.SquareWithCoordinates(x + 1, y + 1 * _verticalMoveMultiplier);
        var secondAttackSquare = squareWithPiece.MyBoard.SquareWithCoordinates(x - 1, y + 1 * _verticalMoveMultiplier);
        
        TryAddSquareThatPawnAttacks(firstAttackSquare, SupposedMoves);
        TryAddSquareThatPawnAttacks(secondAttackSquare, SupposedMoves);

        var firstMoveSquare = squareWithPiece.MyBoard.SquareWithCoordinates(x, y + 1 * _verticalMoveMultiplier);
        if (firstMoveSquare == null) return;
        if (firstMoveSquare.PieceOnIt == null) SupposedMoves.Add(firstMoveSquare);
        else return;

        if (!IsFirstMove) return;
        
        var secondMoveSquare = squareWithPiece.MyBoard.SquareWithCoordinates(x, y + 2 * _verticalMoveMultiplier);
        if (secondMoveSquare == null) return;
        if (secondMoveSquare.PieceOnIt == null) SupposedMoves.Add(secondMoveSquare);
    }

    public override void MoveTo(Square square)
    {
        transform.position = square.transform.position + Offset;
        if (IsFirstMove) IsFirstMove = false;

        var promotionCoordinate = ColorCode == "w" ? square.MyBoard.Size.y - 1 : 0;
        if (square.Coordinates.y == promotionCoordinate)
            ReferenceRegistry.Instance.MyPawnPromotion.Activate(this);
        else
            OnPieceMoved?.Invoke(this, new PieceMovedEventArgs());
    }

    private void TryAddSquareThatPawnAttacks(Square square, List<Square> supposedSquaresForMove)
    {
        if (square == null) return;
        if (square.PieceOnIt != null && square.PieceOnIt.ColorCode != ColorCode)
            supposedSquaresForMove.Add(square);
        if (square.MyEnPassant.Possible && square.MyEnPassant.MyPawn.ColorCode != ColorCode)
            supposedSquaresForMove.Add(square);
    }

    protected override void SetSprite()
    {
        var pawnSprites = ReferenceRegistry.Instance.MySpritesStorage.PawnGraphicData;
        _renderer.sprite = ColorCode == "w" ? pawnSprites.WhiteSprite : pawnSprites.BlackSprite;
    }
}