using System.Collections.Generic;

public class King : Piece
{
    public override PieceType MyType => PieceType.King;
    public override string TypeCodeValue => "K";

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

        Square upSquare = square.Board.GetSquareWithCoordinates(x, y + 1);
        Square upRightSquare = square.Board.GetSquareWithCoordinates(x + 1, y + 1);
        Square rightSquare = square.Board.GetSquareWithCoordinates(x + 1, y);
        Square downRightSquare = square.Board.GetSquareWithCoordinates(x + 1, y - 1);
        Square downSquare = square.Board.GetSquareWithCoordinates(x, y - 1);
        Square downLeftSquare = square.Board.GetSquareWithCoordinates(x - 1, y - 1);
        Square leftSquare = square.Board.GetSquareWithCoordinates(x - 1, y);
        Square upLeftSquare = square.Board.GetSquareWithCoordinates(x - 1, y + 1);

        Square[] predictTurns = 
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
                    turns.Add(predictTurns[i]);
            }
        }
        
        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetMovesWithoutCheck(square, turns, ActionType.Movement);

        return moves;
    }

    protected override void ResetAttackTurns()
    {
        _attackTurns.Clear();
    }
}