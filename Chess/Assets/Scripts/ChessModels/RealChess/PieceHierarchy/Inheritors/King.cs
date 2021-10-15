using System.Collections.Generic;

public class King : Piece
{
    public override PieceType MyType => PieceType.King;
    
    private List<Square> _attackTurns = new List<Square>();

    public override List<Square> GetPossibleAttackTurns(Square squareWithThis)
    {
        return _attackTurns;
    }

    public override List<Square> GetPossibleMoveTurns(Square squareWithThis)
    {
        _attackTurns.Clear();

        int x = squareWithThis.Coordinates.x;
        int y = squareWithThis.Coordinates.y;

        List<Square> turns = new List<Square>();

        Square upSquare = _squareHandler.GetSquareWithCoordinates(x, y + 1);
        Square upRightSquare = _squareHandler.GetSquareWithCoordinates(x + 1, y + 1);
        Square rightSquare = _squareHandler.GetSquareWithCoordinates(x + 1, y);
        Square downRightSquare = _squareHandler.GetSquareWithCoordinates(x + 1, y - 1);
        Square downSquare = _squareHandler.GetSquareWithCoordinates(x, y - 1);
        Square downLeftSquare = _squareHandler.GetSquareWithCoordinates(x - 1, y - 1);
        Square leftSquare = _squareHandler.GetSquareWithCoordinates(x - 1, y);
        Square upLeftSquare = _squareHandler.GetSquareWithCoordinates(x - 1, y + 1);

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

        return turns;
    }

    protected override void ResetAttackTurns()
    {
        _attackTurns.Clear();
    }
}