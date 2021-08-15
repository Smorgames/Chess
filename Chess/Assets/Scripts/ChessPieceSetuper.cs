using System.Collections;
using UnityEngine;

public class ChessPieceSetuper : MonoBehaviour
{
    public SquareHandler SquareHandler;
    public AllPiecesStorage PieceStorage;
    public Piece Black;
    public Piece White;

    private Vector3 _offset;

    private void Awake()
    {
        _offset = Piece.Offset;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        PieceStorage.AddPieceInArrayOfAllPieces(White);
        PieceStorage.AddPieceInArrayOfAllPieces(Black);
        SetChessPieceOnCell(White, 1, 1);
        SetChessPieceOnCell(Black, 2, 2);
    }

    private void SetChessPieceOnCell(Piece chessPiece, int x, int y)
    {
        SquareHandler.GetSquareWithCoordinates(x, y).PieceOnThis = chessPiece;
        chessPiece.transform.position = SquareHandler.GetSquareWithCoordinates(x, y).transform.position + _offset;
    }
}