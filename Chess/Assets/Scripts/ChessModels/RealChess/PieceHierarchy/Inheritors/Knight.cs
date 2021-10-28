using System.Collections.Generic;

public class Knight : RealPiece, IKnight
{
    public override string TypeCode => "k";
    public IPiece MyIPiece => this;

    public override List<ISquare> GetAttacks(ISquare square) => this.KnightSquaresForAction(square, ActionType.Attack);
    
    public override List<ISquare> GetMoves(ISquare square) => this.KnightSquaresForAction(square, ActionType.Movement);
}