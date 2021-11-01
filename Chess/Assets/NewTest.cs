using UnityEngine;

public class NewTest : MonoBehaviour
{
    private void Start()
    {
        var board = ChessboardBuilder.BuildStandardChessboard();

        var kingPref = ReferenceRegistry.Instance.PrefabsStorage.King;
        var rookPref = ReferenceRegistry.Instance.PrefabsStorage.Rook;
        
        var king = Instantiate(kingPref, transform.position, Quaternion.identity);
        king.Init(NewPieceColor.Black, NewPieceType.King, true);
        var king1 = Instantiate(kingPref, transform.position, Quaternion.identity);
        king1.Init(NewPieceColor.White, NewPieceType.King, true);
        
        var rook1 = Instantiate(rookPref, transform.position, Quaternion.identity);
        rook1.Init(NewPieceColor.White, NewPieceType.Rook, true);
        var rook2 = Instantiate(rookPref, transform.position, Quaternion.identity);
        rook2.Init(NewPieceColor.White, NewPieceType.Rook, true);

        board.Squares[3, 0].PieceOnIt = king;
        king.transform.position = board.Squares[3, 0].transform.position;
        board.Squares[4, 5].PieceOnIt = rook1;
        rook1.transform.position = board.Squares[4, 5].transform.position;
        board.Squares[2, 6].PieceOnIt = rook2;
        rook2.transform.position = board.Squares[2, 6].transform.position;
        board.Squares[7, 7].PieceOnIt = king1;
        king1.transform.position = board.Squares[7, 7].transform.position;

        king.UpdateSupposedMoves(board.Squares[3, 0]);
        rook1.UpdateSupposedMoves(board.Squares[4, 5]);
        rook2.UpdateSupposedMoves(board.Squares[2, 6]);
        king1.UpdateSupposedMoves(board.Squares[7, 7]);

        var mate = NewAnalyzer.MateForKing(board, NewPieceColor.Black);
        var kingMoves = NewAnalyzer.MovesWithoutCheckForKing(board.Squares[3, 0], king.SupposedMoves);
    }
}