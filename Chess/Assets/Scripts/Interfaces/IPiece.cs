using System.Collections.Generic;
using UnityEngine;

public interface IPiece
{
    string TypeCode { get; }
    string ColorCode { get; }
    bool IsFirstMove { get; }
    List<ISquare> GetMoves(ISquare squareWithPiece);
    List<ISquare> GetAttacks(ISquare squareWithPiece);
}

public interface IRealPiece : IPiece
{
    Transform piecesTransform { get; }
    void Move(IRealSquare square);
    new List<IRealSquare> GetMoves(ISquare squareWithPiece);
    new List<IRealSquare> GetAttacks(ISquare squareWithPiece);
}

public interface IAbsPiece : IPiece
{
    new List<IAbsSquare> GetMoves(ISquare squareWithPiece);
    new List<IAbsSquare> GetAttacks(ISquare squareWithPiece);
}