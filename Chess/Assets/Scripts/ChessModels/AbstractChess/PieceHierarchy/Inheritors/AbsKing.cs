using System.Collections.Generic;

namespace AbstractChess
{
    public class AbsKing : AbsPiece, IKing
    {
        public override string TypeCode => "K";
        public IPiece MyIPiece => this;

        public AbsKing(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove)
        {
            
        }
        
        public override List<ISquare> GetMoves(ISquare square) => this.KingSquaresForAction(square, ActionType.Movement);
        public override List<ISquare> GetAttacks(ISquare square) => this.KingSquaresForAction(square, ActionType.Attack);
        
        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} king";
        }
    }
}