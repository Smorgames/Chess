using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override PieceType MyType => PieceType.Knight;
    public override string TypeCode => "k";

    public override List<IRealSquare> GetRealAttacks(IRealSquare realSquare)
    {
        var x = realSquare.Coordinates.x;
        var y = realSquare.Coordinates.y;

        var supposedAttacks = new List<IRealSquare>();
        var supposedSquaresCoordinates = new List<Vector2Int>()
        {
            new Vector2Int(x + 2, y + 1), new Vector2Int(x + 2, y - 1), new Vector2Int(x - 2, y + 1), new Vector2Int(x - 2, y - 1), 
            new Vector2Int(x + 1, y + 2), new Vector2Int(x + 1, y - 2), new Vector2Int(x - 1, y + 2), new Vector2Int(x - 1, y - 2)
        };

        foreach (var coordinates in supposedSquaresCoordinates)
        {
            var supposedAttack = realSquare.RealBoard.GetRealSquareWithCoordinates(coordinates);
            
            if (!supposedAttack.IsGhost && PieceStandsOnSquare(supposedAttack) && PieceOnSquareHasOppositeColor(supposedAttack))
                supposedAttacks.Add(supposedAttack);
        }

        return /*_gameManager.Analyzer.GetMovesWithoutCheck(square, supposedAttackMoves, ActionType.Attack);*/ supposedAttacks;
    }

    public override List<IRealSquare> GetRealMoves(IRealSquare realSquare)
    {
        var x = realSquare.Coordinates.x;
        var y = realSquare.Coordinates.y;

        var supposedMoves = new List<IRealSquare>();

        var firstSquare = realSquare.Board.GetSquareWithCoordinates(x + 2, y + 1);
        var secondSquare = realSquare.Board.GetSquareWithCoordinates(x + 2, y - 1);
        var thirdSquare = realSquare.Board.GetSquareWithCoordinates(x - 2, y + 1);
        var fourthSquare = realSquare.Board.GetSquareWithCoordinates(x - 2, y - 1);
        var fifthSquare = realSquare.Board.GetSquareWithCoordinates(x + 1, y + 2);
        var sixthSquare = realSquare.Board.GetSquareWithCoordinates(x + 1, y - 2);
        var seventhSquare = realSquare.Board.GetSquareWithCoordinates(x - 1, y + 2);
        var eighthSquare = realSquare.Board.GetSquareWithCoordinates(x - 1, y - 2);

        var predictTurns = new IRealSquare []
        { firstSquare, secondSquare, thirdSquare, fourthSquare, fifthSquare, sixthSquare, seventhSquare, eighthSquare };

        for (int i = 0; i < predictTurns.Length; i++)
            if (!PieceStandsOnSquare(predictTurns[i]) && !predictTurns[i].IsGhost)
                supposedMoves.Add(predictTurns[i]);
        
        return supposedMoves;
    }
}