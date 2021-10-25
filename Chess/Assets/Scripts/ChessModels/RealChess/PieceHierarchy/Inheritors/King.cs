using System.Collections.Generic;

public class King : Piece
{
    public override PieceType MyType => PieceType.King;
    public override string TypeCode => "K";

    private List<IRealSquare> _attackTurns = new List<IRealSquare>();

    public override List<IRealSquare> GetRealAttacks(IRealSquare realSquare)
    {
        return _attackTurns;
    }

    public override List<IRealSquare> GetRealMoves(IRealSquare realSquare)
    {
        _attackTurns.Clear();

        var x = realSquare.Coordinates.x;
        var y = realSquare.Coordinates.y;

        var supposedMoves = new List<IRealSquare>();

        var upSquare = realSquare.Board.GetSquareWithCoordinates(x, y + 1);
        var upRightSquare = realSquare.Board.GetSquareWithCoordinates(x + 1, y + 1);
        var rightSquare = realSquare.Board.GetSquareWithCoordinates(x + 1, y);
        var downRightSquare = realSquare.Board.GetSquareWithCoordinates(x + 1, y - 1);
        var downSquare = realSquare.Board.GetSquareWithCoordinates(x, y - 1);
        var downLeftSquare = realSquare.Board.GetSquareWithCoordinates(x - 1, y - 1);
        var leftSquare = realSquare.Board.GetSquareWithCoordinates(x - 1, y);
        var upLeftSquare = realSquare.Board.GetSquareWithCoordinates(x - 1, y + 1);

        var predictTurns = new IRealSquare[]
            { upSquare, upRightSquare, rightSquare, downRightSquare, downSquare, downLeftSquare, leftSquare, upLeftSquare};

        for (int i = 0; i < predictTurns.Length; i++)
        {
            if (!predictTurns[i].IsGhost)
            {
                if (PieceStandsOnSquare(predictTurns[i]))
                {
                    if (PieceOnSquareHasOppositeColor(predictTurns[i]))
                    {
                        _attackTurns.Add(predictTurns[i]);
                        continue;
                    }
                }
                else
                    supposedMoves.Add(predictTurns[i]);
            }
        }

        return supposedMoves;
    }

    protected override void ResetAttackTurns()
    {
        _attackTurns.Clear();
    }
}