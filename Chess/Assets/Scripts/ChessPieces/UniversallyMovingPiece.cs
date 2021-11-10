using System.Collections.Generic;
using UnityEngine;

public abstract class UniversallyMovingPiece : Piece
{
    protected static Vector2Int _up = Vector2Int.up;
    protected static Vector2Int _down = Vector2Int.down;
    protected static Vector2Int _left= Vector2Int.left;
    protected static Vector2Int _right = Vector2Int.right;

    protected static Vector2Int _upRight = new Vector2Int(1, 1);
    protected static Vector2Int _upLeft = new Vector2Int(-1, 1);
    protected static Vector2Int _downRight = new Vector2Int(1, -1);
    protected static Vector2Int _downLeft = new Vector2Int(-1, -1);

    protected static List<Square> IterativelyAddedSquares(Square square, List<Vector2Int> directions) => IterativelyAddedSquares(square, directions, false);
    protected static List<Square> IterativelyAddedSquares(Square square, List<Vector2Int> directions, bool isItRook)
    {
        var supposedSquares = new List<Square>();
        var size = square.Board.Size;
        var longestBoardSide = size.x >= size.y ? size.x : size.y;

        var canCastle = false;
        
        foreach (var direction in directions)
        {
            for (int i = 1; i < longestBoardSide; i++)
            {
                var supposedMove = square.Board.SquareWithCoordinates(square.Coordinates.x + direction.x * i, square.Coordinates.y + direction.y * i);

                if (supposedMove == null) break;
                
                if (supposedMove.PieceOnIt != null)
                {
                    if (isItRook && supposedMove.PieceOnIt is King king)
                    {
                        var rook = (Rook) square.PieceOnIt;
                        var kingCastlingMove = king.GetCastlingMoveBasedOnRook(rook);

                        canCastle = CanCastle(square, supposedMove);
                        if (canCastle)
                        {
                            var castlingSquares = GetCastlingMove(square, supposedMove, direction);
                            kingCastlingMove.CastlePossible(rook, castlingSquares.RookSquare, castlingSquares.KingSquare);
                        }
                    }

                    if (supposedMove.PieceOnIt.ColorCode != square.PieceOnIt.ColorCode)
                        supposedSquares.Add(supposedMove);

                    break;
                }
                
                supposedSquares.Add(supposedMove);
            }
        }

        if (isItRook && !canCastle)
        {
            var player = Game.Instance.GetPlayerBasedOnColorCode(square.PieceOnIt.ColorCode);
            var king = player.MyKing;
            var rook = (Rook)square.PieceOnIt;

            king.GetCastlingMoveBasedOnRook(rook).CastleImpossible();
        }
            
        return supposedSquares;
    }

    private static CastlingMove GetCastlingMove(Square rookSquare, Square kingSquare, Vector2Int rookDirection)
    {
        var kingMoveVector = -rookDirection * 2;
        var kingCoord = kingSquare.Coordinates + kingMoveVector;
        var rookCoord = kingCoord + rookDirection;
        var board = kingSquare.Board;
        var kingCastlingSquare = board.SquareWithCoordinates(kingCoord);
        var rookCastlingSquare = board.SquareWithCoordinates(rookCoord);

        var castlingMove = new CastlingMove(null);
        castlingMove.CastlePossible(null, rookCastlingSquare, kingCastlingSquare);

        return castlingMove;
    }

    private static bool CanCastle(Square rookSquare, Square kingSquare)
    {
        var king = (King)kingSquare.PieceOnIt;
        var rook = (Rook)rookSquare.PieceOnIt;
        var isCheck = CheckMateAnalyser.CheckForKing(kingSquare.PieceOnIt.ColorCode);
        return king is King && king.ColorCode == rook.ColorCode && rook.IsFirstMove && king.IsFirstMove && !isCheck;
    }
    
    protected static List<Square> OneDirectionOneMove(Square square, List<Vector2Int> directions)
    {
        var supposedMoves = new List<Square>();
        
        foreach (var direction in directions)
        {
            var supposedMove = square.Board.SquareWithCoordinates(square.Coordinates + direction);
            if (supposedMove == null) continue;

            if (supposedMove.PieceOnIt == null)
            {
                supposedMoves.Add(supposedMove);
                continue;
            }
            
            if (supposedMove.PieceOnIt.ColorCode != square.PieceOnIt.ColorCode) supposedMoves.Add(supposedMove);
        }

        return supposedMoves;
    }
}