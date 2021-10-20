using System.Collections.Generic;

public class Knight : Piece
{
    public override PieceType MyType => PieceType.Knight;
    public override string TypeCodeValue => "k";

    public override List<Square> GetPossibleAttackTurns(Square square)
    {
        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        List<Square> supposedAttackMoves = new List<Square>();

        var firstSquare = square.Board.GetSquareWithCoordinates(x + 2, y + 1);
        var secondSquare = square.Board.GetSquareWithCoordinates(x + 2, y - 1);
        var thirdSquare = square.Board.GetSquareWithCoordinates(x - 2, y + 1);
        var fourthSquare = square.Board.GetSquareWithCoordinates(x - 2, y - 1);
        var fifthSquare = square.Board.GetSquareWithCoordinates(x + 1, y + 2);
        var sixthSquare = square.Board.GetSquareWithCoordinates(x + 1, y - 2);
        var seventhSquare = square.Board.GetSquareWithCoordinates(x - 1, y + 2);
        var eighthSquare = square.Board.GetSquareWithCoordinates(x - 1, y - 2);

        Square[] predictAttackTurns = 
            { firstSquare, secondSquare, thirdSquare, fourthSquare, fifthSquare, sixthSquare, seventhSquare, eighthSquare };

        for (int i = 0; i < predictAttackTurns.Length; i++)
        {
            if (!predictAttackTurns[i].IsGhost && PieceStandsOnSquare(predictAttackTurns[i]) && PieceOnSquareHasOppositeColor(predictAttackTurns[i]))
                supposedAttackMoves.Add(predictAttackTurns[i]);
        }

        return _gameManager.Analyzer.GetMovesWithoutCheck(square, supposedAttackMoves, ActionType.Attack);
    }

    public override List<Square> GetPossibleMoveTurns(Square square)
    {
        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        List<Square> turns = new List<Square>();

        var firstSquare = square.Board.GetSquareWithCoordinates(x + 2, y + 1);
        var secondSquare = square.Board.GetSquareWithCoordinates(x + 2, y - 1);
        var thirdSquare = square.Board.GetSquareWithCoordinates(x - 2, y + 1);
        var fourthSquare = square.Board.GetSquareWithCoordinates(x - 2, y - 1);
        var fifthSquare = square.Board.GetSquareWithCoordinates(x + 1, y + 2);
        var sixthSquare = square.Board.GetSquareWithCoordinates(x + 1, y - 2);
        var seventhSquare = square.Board.GetSquareWithCoordinates(x - 1, y + 2);
        var eighthSquare = square.Board.GetSquareWithCoordinates(x - 1, y - 2);

        Square[] predictTurns = { firstSquare, secondSquare, thirdSquare, fourthSquare, fifthSquare, sixthSquare, seventhSquare, eighthSquare };

        for (int i = 0; i < predictTurns.Length; i++)
            if (!PieceStandsOnSquare(predictTurns[i]) && !predictTurns[i].IsGhost)
                turns.Add(predictTurns[i]);
        
        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetMovesWithoutCheck(square, turns, ActionType.Movement);
        
        return moves;
    }
}