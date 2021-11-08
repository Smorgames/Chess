using System.Collections.Generic;
using UnityEngine;

public class King : UniversallyMovingPiece
{
    public override string TypeCode => "k";

    public Dictionary<Rook, CastlingMove> CastlingMoves = new Dictionary<Rook, CastlingMove>();

    public override void UpdateSupposedMoves(Square squareWithPiece)
    {
        SupposedMoves.Clear();
        var directions = new List<Vector2Int>() { _up, _down, _left, _right, _upRight, _upLeft, _downRight, _downLeft };
        SupposedMoves = OneDirectionOneMove(squareWithPiece, directions);

        AddCastlingMovesToSupposedMoves();
    }

    private void AddCastlingMovesToSupposedMoves()
    {
        foreach (var castlingMove in CastlingMoves)
            if (castlingMove.Value.KingSquare != null && castlingMove.Value.RookSquare != null)
                SupposedMoves.Add(castlingMove.Value.KingSquare);
    }
}

public class CastlingMove
{
    public Square RookSquare;
    public Square KingSquare;
}