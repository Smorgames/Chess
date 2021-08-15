using System.Collections;
using UnityEngine;

public class ChessPieceSetuper : MonoBehaviour
{
    public SquareHandler SquareHandler;
    public AllPiecesStorage PieceStorage;
    public Piece wq;
    public Piece bq;
    public Piece wp;
    public Piece bp;
    public Piece wk;
    public Piece bk;
    public Piece wr;
    public Piece br;

    private Vector3 _offset;

    private void Awake()
    {
        _offset = Piece.Offset;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        PieceStorage.AddPieceInArrayOfAllPieces(bq);
        PieceStorage.AddPieceInArrayOfAllPieces(wq);
        SetChessPieceOnCell(bq, 1, 1);
        SetChessPieceOnCell(wq, 4, 1);
        SetChessPieceOnCell(bp, 3, 5);
        SetChessPieceOnCell(wp, 2, 7);
        SetChessPieceOnCell(bk, 4, 4);
        SetChessPieceOnCell(wk, 5, 4);
        SetChessPieceOnCell(br, 5, 1);
        SetChessPieceOnCell(wr, 6, 2);
    }

    private void SetChessPieceOnCell(Piece chessPiece, int x, int y)
    {
        SquareHandler.GetSquareWithCoordinates(x, y).PieceOnThis = chessPiece;
        chessPiece.transform.position = SquareHandler.GetSquareWithCoordinates(x, y).transform.position + _offset;
    }
}