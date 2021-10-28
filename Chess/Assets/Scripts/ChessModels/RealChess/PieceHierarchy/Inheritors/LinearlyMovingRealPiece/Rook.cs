using System.Collections.Generic;

public class Rook : RealPiece, IRook
{
    public override string TypeCode => "r";
    public IPiece MyIPiece => this;

    public override List<ISquare> GetAttacks(ISquare square) => this.RookSquaresForAction(square, ActionType.Attack);
    
    public override List<ISquare> GetMoves(ISquare square) => this.RookSquaresForAction(square, ActionType.Movement);
}