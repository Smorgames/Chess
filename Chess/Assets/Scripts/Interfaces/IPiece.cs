using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{
    string TypeCode { get; }
    string ColorCode { get; }
    bool IsFirstMove { get; }
    List<ISquare> GetMoves(ISquare squareWithPiece);
    List<ISquare> GetAttacks(ISquare square);
}

public interface IRealPiece : IPiece
{
    Transform PiecesTransform { get; }
    void Move(IRealSquare square);
    List<IRealSquare> GetRealMoves(IRealSquare realSquare);
    List<IRealSquare> GetRealAttacks(IRealSquare realSquare);
}

public interface IAbsPiece : IPiece
{
    
}