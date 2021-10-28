using System.Collections.Generic;

public class Queen : RealPiece, IQueen
{
    public override string TypeCode => "Q";
    public IPiece MyIPiece => this;

    public override List<ISquare> GetAttacks(ISquare square) => this.QueenSquaresForAction(square, ActionType.Attack);

    public override List<ISquare> GetMoves(ISquare square) => this.QueenSquaresForAction(square, ActionType.Movement);
}