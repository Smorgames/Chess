using System.Collections.Generic;

public class Knight : Piece
{
    public override PieceType MyType => PieceType.Knight;
    public override List<Square> GetPossibleAttackTurns(Square squareWithThis)
    {
        int x = squareWithThis.Coordinates.x;
        int y = squareWithThis.Coordinates.y;

        List<Square> attackTurns = new List<Square>();

        Square firstSquare = _squareHandler.GetSquareWithCoordinates(x + 2, y + 1);
        Square secondSquare = _squareHandler.GetSquareWithCoordinates(x + 2, y - 1);
        Square thirdSquare = _squareHandler.GetSquareWithCoordinates(x - 2, y + 1);
        Square fourthSquare = _squareHandler.GetSquareWithCoordinates(x - 2, y - 1);
        Square fifthSquare = _squareHandler.GetSquareWithCoordinates(x + 1, y + 2);
        Square sixthSquare = _squareHandler.GetSquareWithCoordinates(x + 1, y - 2);
        Square seventhSquare = _squareHandler.GetSquareWithCoordinates(x - 1, y + 2);
        Square eighthSquare = _squareHandler.GetSquareWithCoordinates(x - 1, y - 2);

        Square[] predictAttackTurns = 
            { firstSquare, secondSquare, thirdSquare, fourthSquare, fifthSquare, sixthSquare, seventhSquare, eighthSquare };

        for (int i = 0; i < predictAttackTurns.Length; i++)
        {
            if (IsPieceStandsOnSquare(predictAttackTurns[i]) && IsPieceOnSquareHasOppositeColor(predictAttackTurns[i]))
                attackTurns.Add(predictAttackTurns[i]);
        }

        return attackTurns;
    }

    public override List<Square> GetPossibleMoveTurns(Square squareWithThis)
    {
        int x = squareWithThis.Coordinates.x;
        int y = squareWithThis.Coordinates.y;

        List<Square> turns = new List<Square>();

        Square firstSquare = _squareHandler.GetSquareWithCoordinates(x + 2, y + 1);
        Square secondSquare = _squareHandler.GetSquareWithCoordinates(x + 2, y - 1);
        Square thirdSquare = _squareHandler.GetSquareWithCoordinates(x - 2, y + 1);
        Square fourthSquare = _squareHandler.GetSquareWithCoordinates(x - 2, y - 1);
        Square fifthSquare = _squareHandler.GetSquareWithCoordinates(x + 1, y + 2);
        Square sixthSquare = _squareHandler.GetSquareWithCoordinates(x + 1, y - 2);
        Square seventhSquare = _squareHandler.GetSquareWithCoordinates(x - 1, y + 2);
        Square eighthSquare = _squareHandler.GetSquareWithCoordinates(x - 1, y - 2);

        Square[] predictTurns = { firstSquare, secondSquare, thirdSquare, fourthSquare, fifthSquare, sixthSquare, seventhSquare, eighthSquare };

        for (int i = 0; i < predictTurns.Length; i++)
        {
            if (!IsPieceStandsOnSquare(predictTurns[i]))
                turns.Add(predictTurns[i]);
        }

        return turns;
    }
}