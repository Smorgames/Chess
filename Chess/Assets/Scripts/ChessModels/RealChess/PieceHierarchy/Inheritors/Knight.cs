using System.Collections.Generic;

public class Knight : Piece
{
    public override PieceType MyType => PieceType.Knight;
    public override string TypeCode => "k";

    public override List<IRealSquare> GetAttacks(IRealSquare square)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;

        var supposedAttackMoves = new List<IRealSquare>();

        var firstSquare = square.Board.GetSquareWithCoordinates(x + 2, y + 1);
        var secondSquare = square.Board.GetSquareWithCoordinates(x + 2, y - 1);
        var thirdSquare = square.Board.GetSquareWithCoordinates(x - 2, y + 1);
        var fourthSquare = square.Board.GetSquareWithCoordinates(x - 2, y - 1);
        var fifthSquare = square.Board.GetSquareWithCoordinates(x + 1, y + 2);
        var sixthSquare = square.Board.GetSquareWithCoordinates(x + 1, y - 2);
        var seventhSquare = square.Board.GetSquareWithCoordinates(x - 1, y + 2);
        var eighthSquare = square.Board.GetSquareWithCoordinates(x - 1, y - 2);

        var predictAttackTurns = new IRealSquare[] 
        { firstSquare, secondSquare, thirdSquare, fourthSquare, fifthSquare, sixthSquare, seventhSquare, eighthSquare };

        for (int i = 0; i < predictAttackTurns.Length; i++)
        {
            if (!predictAttackTurns[i].IsGhost && PieceStandsOnSquare(predictAttackTurns[i]) && PieceOnSquareHasOppositeColor(predictAttackTurns[i]))
                supposedAttackMoves.Add(predictAttackTurns[i]);
        }

        return /*_gameManager.Analyzer.GetMovesWithoutCheck(square, supposedAttackMoves, ActionType.Attack);*/ supposedAttackMoves;
    }

    public override List<IRealSquare> GetMoves(IRealSquare square)
    {
        var x = square.Coordinates.x;
        var y = square.Coordinates.y;

        var supposedMoves = new List<IRealSquare>();

        var firstSquare = square.Board.GetSquareWithCoordinates(x + 2, y + 1);
        var secondSquare = square.Board.GetSquareWithCoordinates(x + 2, y - 1);
        var thirdSquare = square.Board.GetSquareWithCoordinates(x - 2, y + 1);
        var fourthSquare = square.Board.GetSquareWithCoordinates(x - 2, y - 1);
        var fifthSquare = square.Board.GetSquareWithCoordinates(x + 1, y + 2);
        var sixthSquare = square.Board.GetSquareWithCoordinates(x + 1, y - 2);
        var seventhSquare = square.Board.GetSquareWithCoordinates(x - 1, y + 2);
        var eighthSquare = square.Board.GetSquareWithCoordinates(x - 1, y - 2);

        var predictTurns = new IRealSquare []
        { firstSquare, secondSquare, thirdSquare, fourthSquare, fifthSquare, sixthSquare, seventhSquare, eighthSquare };

        for (int i = 0; i < predictTurns.Length; i++)
            if (!PieceStandsOnSquare(predictTurns[i]) && !predictTurns[i].IsGhost)
                supposedMoves.Add(predictTurns[i]);
        
        return supposedMoves;
    }
}