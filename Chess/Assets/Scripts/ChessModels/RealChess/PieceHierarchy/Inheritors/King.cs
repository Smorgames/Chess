using System.Collections.Generic;

public class King : RealPiece, IKing
{
    public override string TypeCode => "K";
    public IPiece MyIPiece => this;

    public override List<ISquare> GetMoves(ISquare square) => this.KingSquaresForAction(square, ActionType.Movement);
    public override List<ISquare> GetAttacks(ISquare square) => this.KingSquaresForAction(square, ActionType.Attack);
}