using System.Collections;
using UnityEngine;

public class ChessPieceSetuper : MonoBehaviour
{
    public SquareHandler SquareHandler;
    public AllPiecesStorage PieceStorage;
    public Pawn BlackPawn;
    public Pawn WhitePawn;

    private Vector3 _offset;

    private void Awake()
    {
        _offset = Piece.Offset;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        PieceStorage.AddPieceInArrayOfAllPieces(WhitePawn);
        PieceStorage.AddPieceInArrayOfAllPieces(BlackPawn);
        SetChessPieceOnCell(WhitePawn, 1, 1);
        SetChessPieceOnCell(BlackPawn, 2, 2);
    }

    private void SetChessPieceOnCell(Piece chessPiece, int x, int y)
    {
        SquareHandler.GetSquareWithCoordinates(x, y).PieceOnThis = chessPiece;
        chessPiece.transform.position = SquareHandler.GetSquareWithCoordinates(x, y).transform.position + _offset;
    }
}