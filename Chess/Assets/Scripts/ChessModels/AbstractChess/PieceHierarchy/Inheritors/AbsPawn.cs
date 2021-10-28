using System.Collections.Generic;

namespace AbstractChess
{ 
    public class AbsPawn : AbsPiece, IPawn
    {
        public override string TypeCode => "p";
        public int MoveDirection => ColorCode == "w" ? 1 : -1;
        public IPiece MyIPiece => this;

        public AbsPawn(string colorCode, bool isFirstMove) : base(colorCode, isFirstMove)
        {
            
        }
        
        public override List<ISquare> GetMoves(ISquare square) => this.PawnSquaresForAction(square, ActionType.Movement);

        public override List<ISquare> GetAttacks(ISquare square) => this.PawnSquaresForAction(square, ActionType.Attack);

        public override string ToString()
        {
            var color = ColorCode == "w" ? "White" : "Black";
            return $"{color} pawn";
        }
    }
}