using System.Collections.Generic;

namespace AbstractChess
{
    public class AbsBishop : AbsPiece, IBishop
    {        
        public override string TypeCode => "b";
        public IPiece MyIPiece => this;

        public AbsBishop(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove)
        {
            
        }

        public override List<ISquare> GetAttacks(ISquare square) => this.BishopSquaresForAction(square, ActionType.Attack);
        public override List<ISquare> GetMoves(ISquare square) => this.BishopSquaresForAction(square, ActionType.Movement);

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} bishop";
        }
    }
}