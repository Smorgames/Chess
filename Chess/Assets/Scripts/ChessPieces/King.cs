using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : UniversallyMovingPiece
{
    public override string TypeCode => "k";

    public List<CastlingMove> CastlingMoves { get; } = new List<CastlingMove>();

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
            if (castlingMove.KingSquare != null && castlingMove.RookSquare != null)
                SupposedMoves.Add(castlingMove.KingSquare);
    }
}

public class CastlingMove
{
    public Rook CastleRook { get; private set; }
    public Square RookSquare { get; private set; }
    public Square KingSquare { get; private set; }

    public CastlingMove(Rook rook)
    {
        Reset();
        CastleRook = rook;
    }

    public void CastlePossible(Rook rook, Square rookSquare, Square kingSquare)
    {
        CastleRook = rook;
        RookSquare = rookSquare;
        KingSquare = kingSquare;
    }

    public void CastleImpossible() => Reset();

    private void Reset()
    {
        RookSquare = KingSquare = null;
        CastleRook = null;
    }
}