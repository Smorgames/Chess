using System.Collections.Generic;

namespace AbstractChess
{
    public class AbsRook : AbsPiece, IRook
    {
        public override string TypeCode => "r";
        public IPiece MyIPiece => this;

        public AbsRook(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove)
        {
            
        }

        public override List<ISquare> GetAttacks(ISquare square) => this.RookSquaresForAction(square, ActionType.Attack);
    
        public override List<ISquare> GetMoves(ISquare square) => this.RookSquaresForAction(square, ActionType.Movement);
        
        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} rook";
        }
    }
}