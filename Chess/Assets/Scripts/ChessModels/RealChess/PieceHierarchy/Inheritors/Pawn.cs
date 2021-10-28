using System.Collections.Generic;

public class Pawn : RealPiece, IPawn
{
    public override string TypeCode => "p";
    public int MoveDirection => ColorCode == "w" ? 1 : -1;
    public IPiece MyIPiece => this;

    public override List<ISquare> GetAttacks(ISquare square) => this.PawnSquaresForAction(square, ActionType.Attack);

    public override List<ISquare> GetMoves(ISquare square) => this.PawnSquaresForAction(square, ActionType.Movement);
}