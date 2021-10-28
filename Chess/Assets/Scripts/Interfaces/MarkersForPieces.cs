public interface IPieceMarker
{
    IPiece MyIPiece { get; }
}

public interface IPawn : IPieceMarker
{
    int MoveDirection { get; }
}

// Pieces which moves on maximum 1 square in some direction (King or Knight)

public interface IOneSquareMovingPiece : IPieceMarker
{
    
}

public interface IKing : IOneSquareMovingPiece
{

}

public interface IKnight : IOneSquareMovingPiece
{

}

// Pieces which moves iteratively and in some direction (for example Queen)

public interface IIterativelyMovingPiece : IPieceMarker
{
    
}

public interface IRook : IIterativelyMovingPiece
{

}

public interface IBishop : IIterativelyMovingPiece
{

}

public interface IQueen : IIterativelyMovingPiece
{

}