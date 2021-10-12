using UnityEngine;
using AbstractChess;

public class TestAbstractChess : MonoBehaviour
{
    public string Token;

    private string _startChessPosition =
        "07br17bk27bb37bK47bQ57bb67bk77br" +
        "06bp16bp26bp36bp46bp56bp66bp76bp" +
        "01wp11wp21wp31wp41wp51wp61wp71wp" +
        "00wr10wk20wb30wK40wQ50wb60wk70wr";
    
    private AbstractChess.Chessboard _chessboard;
    
    private void Start()
    {
        //Test1();
        Test2();
    }

    private void Test1()
    {
        var l = 3;
        var h = 3;
        
        var squares = GetSquares(l, h);
        _chessboard = new AbstractChess.Chessboard(l, h, squares);
        
        _chessboard.GhostSquare.Board = _chessboard;
        
        var blackPawn = new AbstractChess.Pawn(AbstractChess.Piece.Color.Black);
        var whiteRook = new AbstractChess.Rook(AbstractChess.Piece.Color.White);
        
        _chessboard.Squares[1, 0].PieceOnThisSquare = whiteRook;
        _chessboard.Squares[1, 1].PieceOnThisSquare = new AbstractChess.King(AbstractChess.Piece.Color.Black);
        _chessboard.Squares[1, 2].PieceOnThisSquare = blackPawn;
        
        var pm = _chessboard.Squares[1, 0].PieceOnThisSquare.PossibleMoves(_chessboard.Squares[1, 0]);
        var pam = _chessboard.Squares[1, 0].PieceOnThisSquare.PossibleAttackMoves(_chessboard.Squares[1, 0]);
        
        foreach (var m in pm)
            Debug.Log($"Move: {m}");
        
        foreach (var am in pam)
            Debug.Log($"Attack move: {am}");
    }
    
    private void Test2()
    {
        var l = 5;
        var h = 5;
        
        var squares = GetSquares(l, h);
        _chessboard = new AbstractChess.Chessboard(l, h, squares);

        var positionToken = "11wQ33bp41bK";
        //var analyzer = new CheckAnalyzer();
        
        //var blackKingCheck = analyzer.IsCheckForKing(positionToken, _chessboard, AbstractChess.Piece.Color.Black);
        _chessboard.TestLogAboutChessboard();

        //Debug.Log($"Is check for black king: '{blackKingCheck}'");

        // var pm = _chessboard.Squares[1, 1].PieceOnThisSquare.PossibleMoves(_chessboard.Squares[1, 1]);
        // var pam = _chessboard.Squares[1, 1].PieceOnThisSquare.PossibleAttackMoves(_chessboard.Squares[1, 1]);
        //
        // foreach (var m in pm)
        //     Debug.Log($"Move: {m}");
        //
        // foreach (var am in pam)
        //     Debug.Log($"Attack move: {am}");
    }

    private AbstractChess.Square[,] GetSquares(int length, int height)
    {
        var squares = new AbstractChess.Square[length, height];
        
        for (int x = 0; x < length; x++)
            for (int y = 0; y < height; y++)
                squares[x, y] = new AbstractChess.Square(x, y);

        return squares;
    }
}