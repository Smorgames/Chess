using System.Collections.Generic;

public class Knight : Piece
{
    public override PieceType MyType => PieceType.Knight;
    public override List<Square> GetPossibleAttackTurns(Square square)
    {
        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        List<Square> attackTurns = new List<Square>();

        Square firstSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 2, y + 1);
        Square secondSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 2, y - 1);
        Square thirdSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 2, y + 1);
        Square fourthSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 2, y - 1);
        Square fifthSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 1, y + 2);
        Square sixthSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 1, y - 2);
        Square seventhSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 1, y + 2);
        Square eighthSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 1, y - 2);

        Square[] predictAttackTurns = 
            { firstSquare, secondSquare, thirdSquare, fourthSquare, fifthSquare, sixthSquare, seventhSquare, eighthSquare };

        for (int i = 0; i < predictAttackTurns.Length; i++)
        {
            if (!predictAttackTurns[i].IsGhost && PieceStandsOnSquare(predictAttackTurns[i]) && IsPieceOnSquareHasOppositeColor(predictAttackTurns[i]))
                attackTurns.Add(predictAttackTurns[i]);
        }

        return attackTurns;
    }

    public override List<Square> GetPossibleMoveTurns(Square square)
    {
        int x = square.Coordinates.x;
        int y = square.Coordinates.y;

        List<Square> turns = new List<Square>();

        var firstSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 2, y + 1);
        var secondSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 2, y - 1);
        var thirdSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 2, y + 1);
        var fourthSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 2, y - 1);
        var fifthSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 1, y + 2);
        var sixthSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x + 1, y - 2);
        var seventhSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 1, y + 2);
        var eighthSquare = SingletonRegistry.Instance.Board.GetSquareWithCoordinates(x - 1, y - 2);

        Square[] predictTurns = { firstSquare, secondSquare, thirdSquare, fourthSquare, fifthSquare, sixthSquare, seventhSquare, eighthSquare };

        for (int i = 0; i < predictTurns.Length; i++)
            if (!PieceStandsOnSquare(predictTurns[i]) && !predictTurns[i].IsGhost)
                turns.Add(predictTurns[i]);
        
        var moves = new List<Square>();
        moves = _gameManager.Analyzer.GetMovesWithoutCheck(SingletonRegistry.Instance.Board, square, turns, ActionType.Movement);
        
        return moves;
    }
}