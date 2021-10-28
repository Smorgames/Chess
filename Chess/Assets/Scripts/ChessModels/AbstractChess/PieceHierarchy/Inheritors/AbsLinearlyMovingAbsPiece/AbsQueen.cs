using System.Collections.Generic;

namespace AbstractChess
{
    public class AbsQueen : AbsPiece, IQueen
    {        
        public override string TypeCode => "Q";
        public IPiece MyIPiece => this;

        public AbsQueen(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove)
        {
            
        }

        public override List<ISquare> GetAttacks(ISquare square) => this.QueenSquaresForAction(square, ActionType.Attack);
        public override List<ISquare> GetMoves(ISquare square) => this.QueenSquaresForAction(square, ActionType.Movement);

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} queen";
        }
    }
}