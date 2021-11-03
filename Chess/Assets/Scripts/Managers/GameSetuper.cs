using System;
using System.Collections.Generic;

public class GameSetuper
{
    public static EventHandler<PiecesArrangedArgs> OnPiecesArranged;
    public static Board ArrangePiecesOnBoard(List<SignatureOfPiece> pieceSignatures, Board board)
    {
        foreach (var signature in pieceSignatures)
        {
            var square = board.Squares[signature.SquareCoordinates.x, signature.SquareCoordinates.y];
            square.PieceOnIt = signature.Piece;
            signature.Piece.transform.position = square.transform.position + Piece.Offset;
        }

        OnPiecesArranged?.Invoke(null, new PiecesArrangedArgs());
        return board;
    }
}

public class PiecesArrangedArgs
{
    
}