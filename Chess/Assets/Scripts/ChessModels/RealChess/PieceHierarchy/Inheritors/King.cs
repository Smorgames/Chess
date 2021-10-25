using System.Collections.Generic;

public class King : Piece
{
    public override PieceType MyType => PieceType.King;
    public override string TypeCode => "K";

    private List<IRealSquare> _attackTurns = new List<IRealSquare>();

    public override List<IRealSquare> GetAttacks(IRealSquare square)
    {
        return _attackTurns;
    }

    public override List<IRealSquare> GetMoves(IRealSquare square)
    {
        _attackTurns.Clear();

        var x = square.Coordinates.x;
        var y = square.Coordinates.y;

        var supposedMoves = new List<IRealSquare>();

        var upSquare = square.Board.GetSquareWithCoordinates(x, y + 1);
        var upRightSquare = square.Board.GetSquareWithCoordinates(x + 1, y + 1);
        var rightSquare = square.Board.GetSquareWithCoordinates(x + 1, y);
        var downRightSquare = square.Board.GetSquareWithCoordinates(x + 1, y - 1);
        var downSquare = square.Board.GetSquareWithCoordinates(x, y - 1);
        var downLeftSquare = square.Board.GetSquareWithCoordinates(x - 1, y - 1);
        var leftSquare = square.Board.GetSquareWithCoordinates(x - 1, y);
        var upLeftSquare = square.Board.GetSquareWithCoordinates(x - 1, y + 1);

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