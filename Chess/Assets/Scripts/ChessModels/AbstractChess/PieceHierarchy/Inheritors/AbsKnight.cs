using System.Collections.Generic;

namespace AbstractChess
{
    public class AbsKnight : AbsPiece, IKnight
    {
        public override string TypeCode => "k";
        public IPiece MyIPiece => this;


        public AbsKnight(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove)
        {
            
        }

        public override List<ISquare> GetMoves(ISquare square) => this.KnightSquaresForAction(square, ActionType.Movement);

        public override List<ISquare> GetAttacks(ISquare square) => this.KnightSquaresForAction(square, ActionType.Attack);

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} knight";
        }

    }
}