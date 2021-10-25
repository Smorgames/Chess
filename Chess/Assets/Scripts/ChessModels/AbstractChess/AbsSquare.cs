﻿using UnityEngine;

namespace AbstractChess
{
    public class AbsSquare : IAbsSquare
    {
        public bool IsGhost { get; private set; }

        public IPiece PieceOnIt { get; set; }

        public IChessBoard Board => _board;
        private readonly IChessBoard _board;
        
        public Vector2Int Coordinates => _coordinates;
        private readonly Vector2Int _coordinates;

        public AbsSquare(Vector2Int coordinates, AbsChessboard board)
        {
            _coordinates = coordinates;
            _board = board;
        }

        public override string ToString() => $"[{_coordinates.x};{_coordinates.y} {PieceOnIt}]";
    }
}