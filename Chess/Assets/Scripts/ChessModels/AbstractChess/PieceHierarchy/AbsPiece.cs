using System.Collections.Generic;
using UnityEngine;

namespace AbstractChess
{
    public abstract class AbsPiece : IAbsPiece
    {
        public abstract string TypeCode { get; }

        public string ColorCode => _colorCode;
        private string _colorCode;

        public bool IsFirstMove => _isFirstMove;

        public Transform piecesTransform => null;

        private bool _isFirstMove = true;

        public AbsPiece(string colorCode, bool isFirstMove)
        {
            _isFirstMove = isFirstMove;
            _colorCode = colorCode;
        }

        public abstract List<ISquare> GetMoves(ISquare absSquare);
        public abstract List<ISquare> GetAttacks(ISquare square);
    }
    
    public interface IMoveDirection
    {
        Vector2Int MoveDirection { get; }
    }
}