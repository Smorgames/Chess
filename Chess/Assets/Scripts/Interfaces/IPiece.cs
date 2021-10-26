using System.Collections.Generic;

public interface IPiece
{
    string TypeCode { get; }
    string ColorCode { get; }
    bool IsFirstMove { get; }
    List<ISquare> GetMoves(ISquare square);
    List<ISquare> GetAttacks(ISquare square);
}