using System.Collections.Generic;
using AnalysisOfChessState;

public class Pawn : Piece
{
    public override PieceType MyType => PieceType.Pawn;

    private bool _isFirstTurn = true;

    public override List<Square> GetPossibleAttackTurns(Square square)
    {
        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        int colorMultiplyer = ColorData.Multiplier;

        List<Square> attackTurns = new List<Square>();

        Square firstSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 1 * colorMultiplyer, y + 1 * colorMultiplyer);
        Square secondSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 1 * colorMultiplyer, y + 1 * colorMultiplyer);

        if (IsPieceStandsOnSquare(firstSquare) && IsPieceOnSquareHasOppositeColor(firstSquare))
            attackTurns.Add(firstSquare);

        if (IsPieceStandsOnSquare(secondSquare) && IsPieceOnSquareHasOppositeColor(secondSquare))
            attackTurns.Add(secondSquare);

        return attackTurns;
    }
    
    public override List<Square> GetPossibleMoveTurns(Square square)
    {
        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        int colorMultiplyer = ColorData.Multiplier;

        List<Square> moveTurns = new List<Square>();

        Square firstSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x, y + 1 * colorMultiplyer);

        if (IsPieceStandsOnSquare(firstSquare))
            return moveTurns;

        moveTurns.Add(firstSquare);

        if (_isFirstTurn)
        {
            Square secondSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x, y + 2 * colorMultiplyer);

            if (IsPieceStandsOnSquare(secondSquare))
                return moveTurns;

            moveTurns.Add(secondSquare);
        }
        
        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetCorrectMoves(SingletonRegistry.Instance.Board, square, moveTurns);
        
        return moves;
    }

    public override void Move(Square square)
    {
        base.Move(square);

        if (_isFirstTurn)
            _isFirstTurn = false;
    }
}