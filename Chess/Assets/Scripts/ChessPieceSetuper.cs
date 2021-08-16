using System.Collections;
using UnityEngine;

public class ChessPieceSetuper : MonoBehaviour
{
    [SerializeField] private SquareHandler _suareHandler;
    [SerializeField] private PiecesStorage _pieceStorage;

    [Header("White chess pieces")]
    [SerializeField] private Piece _firstWhPawn;
    [SerializeField] private Piece _secondWhPawn;
    [SerializeField] private Piece _thirdWhPawn;
    [SerializeField] private Piece _fourthWhPawn;
    [SerializeField] private Piece _fifthWhPawn;
    [SerializeField] private Piece _sixthWhPawn;
    [SerializeField] private Piece _seventhWhPawn;
    [SerializeField] private Piece _eighthWhPawn;

    [SerializeField] private Piece _firstWhRook;
    [SerializeField] private Piece _secondWhRook;

    [SerializeField] private Piece _firstWhKnight;
    [SerializeField] private Piece _secondWhKnight;

    [SerializeField] private Piece _firstWhBishop;
    [SerializeField] private Piece _secondWhBishop;

    [SerializeField] private Piece _whQueen;
    [SerializeField] private Piece _whKing;

    [Header("Black chess pieces")]
    [SerializeField] private Piece _firstBlPawn;
    [SerializeField] private Piece _secondBlPawn;
    [SerializeField] private Piece _thirdBlPawn;
    [SerializeField] private Piece _fourthBlPawn;
    [SerializeField] private Piece _fifthBlPawn;
    [SerializeField] private Piece _sixthBlPawn;
    [SerializeField] private Piece _seventhBlPawn;
    [SerializeField] private Piece _eighthBlPawn;

    [SerializeField] private Piece _firstBlRook;
    [SerializeField] private Piece _secondBlRook;

    [SerializeField] private Piece _firstBlKnight;
    [SerializeField] private Piece _secondBlKnight;

    [SerializeField] private Piece _firstBlBishop;
    [SerializeField] private Piece _secondBlBishop;

    [SerializeField] private Piece _blQueen;
    [SerializeField] private Piece _blKing;

    private Vector3 _offset;

    private void Awake()
    {
        InitializeFields();
    }

    private void InitializeFields()
    {
        _offset = Piece.Offset;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        SetWhitePieces();
        SetWBlackPieces();
    }

    private void SetWhitePieces()
    {
        PutPieceOnSquare(_firstWhPawn, 0, 1);
        PutPieceOnSquare(_secondWhPawn, 1, 1);
        PutPieceOnSquare(_thirdWhPawn, 2, 1);
        PutPieceOnSquare(_fourthWhPawn, 3, 1);
        PutPieceOnSquare(_fifthWhPawn, 4, 1);
        PutPieceOnSquare(_sixthWhPawn, 5, 1);
        PutPieceOnSquare(_seventhWhPawn, 6, 1);
        PutPieceOnSquare(_eighthWhPawn, 7, 1);
        PutPieceOnSquare(_firstWhRook, 0, 0);
        PutPieceOnSquare(_secondWhRook, 7, 0);
        PutPieceOnSquare(_firstWhKnight, 1, 0);
        PutPieceOnSquare(_secondWhKnight, 6, 0);
        PutPieceOnSquare(_firstWhBishop, 2, 0);
        PutPieceOnSquare(_secondWhBishop, 5, 0);
        PutPieceOnSquare(_whQueen, 3, 0);
        PutPieceOnSquare(_whKing, 4, 0);
    }

    private void SetWBlackPieces()
    {
        PutPieceOnSquare(_firstBlPawn, 0, 6);
        PutPieceOnSquare(_secondBlPawn, 1, 6);
        PutPieceOnSquare(_thirdBlPawn, 2, 6);
        PutPieceOnSquare(_fourthBlPawn, 3, 6);
        PutPieceOnSquare(_fifthBlPawn, 4, 6);
        PutPieceOnSquare(_sixthBlPawn, 5, 6);
        PutPieceOnSquare(_seventhBlPawn, 6, 6);
        PutPieceOnSquare(_eighthBlPawn, 7, 6);
        PutPieceOnSquare(_firstBlRook, 0, 7);
        PutPieceOnSquare(_secondBlRook, 7, 7);
        PutPieceOnSquare(_firstBlKnight, 1, 7);
        PutPieceOnSquare(_secondBlKnight, 6, 7);
        PutPieceOnSquare(_firstBlBishop, 2, 7);
        PutPieceOnSquare(_secondBlBishop, 5, 7);
        PutPieceOnSquare(_blQueen, 3, 7);
        PutPieceOnSquare(_blKing, 4, 7);
    }

    private void PutPieceOnSquare(Piece piece, int x, int y)
    {
        _suareHandler.GetSquareWithCoordinates(x, y).PieceOnThis = piece;
        piece.transform.position = _suareHandler.GetSquareWithCoordinates(x, y).transform.position + _offset;

        AddPieceInAllPieceArray(piece);
    }

    private void AddPieceInAllPieceArray(Piece piece)
    {
        _pieceStorage.AddPieceInArrayOfAllPieces(piece);
    }
}