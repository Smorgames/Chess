using System.Collections.Generic;

public class Bishop : RealPiece, IBishop
{
    public override string TypeCode => "b";
    public IPiece MyIPiece => this;

    public override List<ISquare> GetAttacks(ISquare square) => this.BishopSquaresForAction(square, ActionType.Attack);
    public override List<ISquare> GetMoves(ISquare square) => this.BishopSquaresForAction(square, ActionType.Movement);
}