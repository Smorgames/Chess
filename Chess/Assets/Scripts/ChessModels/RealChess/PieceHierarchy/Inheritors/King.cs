using System.Collections.Generic;

public class King : Piece
{
    public override PieceType MyType => PieceType.King;
    
    private List<Square> _attackTurns = new List<Square>();

    public override List<Square> GetPossibleAttackTurns(Square square)
    {
        return _attackTurns;
    }

    public override List<Square> GetPossibleMoveTurns(Square square)
    {
        _attackTurns.Clear();

        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        List<Square> turns = new List<Square>();

        Square upSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x, y + 1);
        Square upRightSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 1, y + 1);
        Square rightSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 1, y);
        Square downRightSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 1, y - 1);
        Square downSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x, y - 1);
        Square downLeftSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 1, y - 1);
        Square leftSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 1, y);
        Square upLeftSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 1, y + 1);

        Square[] predictTurns = 
            { upSquare, upRightSquare, rightSquare, downRightSquare, downSquare, downLeftSquare, leftSquare, upLeftSquare};

        for (int i = 0; i < predictTurns.Length; i++)
        {
            if (IsPieceStandsOnSquare(predictTurns[i]))
            {
                if (IsPieceOnSquareHasOppositeColor(predictTurns[i]))
                {
                    _attackTurns.Add(predictTurns[i]);
                    continue;
                }
            }
            else
                turns.Add(predictTurns[i]);
        }
        
        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetCorrectMoves(SingletonRegistry.Instance.Board, square, turns);

        return moves;
    }

    protected override void ResetAttackTurns()
    {
        _attackTurns.Clear();
    }
}