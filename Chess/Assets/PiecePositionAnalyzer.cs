using System;
using System.Collections.Generic;
using UnityEngine;

public class PiecePositionAnalyzer : MonoBehaviour
{
    public string TestToken; // Test
    
    [SerializeField] private GameObject _abstractPawn;
    [SerializeField] private GameObject _abstractRook;
    [SerializeField] private GameObject _abstractKnight;
    [SerializeField] private GameObject _abstractBishop;
    [SerializeField] private GameObject _abstractQueen;
    [SerializeField] private GameObject _abstractKing;

    // Test-------------------------------------------------------------------------
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Parse(TestToken);
    }
    // Test-------------------------------------------------------------------------

    private void ArrangeChessPiecesOnBoard(List<ChessPieceToken> chessPieceTokens)
    {
        
    }

    private List<ChessPieceToken> Parse(string token)
    {
        token = token.Trim(' ');
        List<ChessPieceToken> chessPieceTokens = new List<ChessPieceToken>();

        for (int i = 0; i < token.Length; i += 4)
        {
            var xString = token[i].ToString();
            var isXinteger = int.TryParse(xString, out var x);

            if (!isXinteger)
            {
                var className = nameof(PiecePositionAnalyzer);
                var methodName = nameof(Parse);
                var variableName = nameof(xString);

                Debug.LogError(
                    $"In method {className}.{methodName}() token '{variableName}' = {xString}. It's not an integer!");
                return null;
            }

            var yString = token[i + 1].ToString();
            var isYinteger = int.TryParse(yString, out var y);

            if (!isYinteger)
            {
                var className = nameof(PiecePositionAnalyzer);
                var methodName = nameof(Parse);
                var variableName = nameof(yString);

                Debug.LogError(
                    $"In method {className}.{methodName}() token '{variableName}' = {yString}. It's not an integer!");
                return null;
            }

            var colorString = token[i + 2];
            Piece.Color color;

            if (colorString == 'w')
                color = Piece.Color.White;
            else if (colorString == 'b')
                color = Piece.Color.Black;
            else
            {
                var className = nameof(PiecePositionAnalyzer);
                var methodName = nameof(Parse);
                var variableName = nameof(colorString);

                Debug.LogError(
                    $"In method {className}.{methodName}() token '{variableName}' = {colorString}. It's incorrect value!");
                return null;
            }

            var pieceString = token[i + 3].ToString();
            GameObject piece;

            switch (pieceString)
            {
                case "p":
                    piece = Instantiate(_abstractPawn, Vector3.back, Quaternion.identity);
                    piece.name = "Pawn";
                    break;
                case "r":
                    piece = Instantiate(_abstractRook, Vector3.back, Quaternion.identity);
                    piece.name = "Rook";
                    break;
                case "k":
                    piece = Instantiate(_abstractKnight, Vector3.back, Quaternion.identity);
                    piece.name = "Knight";
                    break;
                case "b":
                    piece = Instantiate(_abstractBishop, Vector3.back, Quaternion.identity);
                    piece.name = "Bishop";
                    break;
                case "Q":
                    piece = Instantiate(_abstractQueen, Vector3.back, Quaternion.identity);
                    piece.name = "Queen";
                    break;
                case "K":
                    piece = Instantiate(_abstractKing, Vector3.back, Quaternion.identity);
                    piece.name = "King";
                    break;
                default:
                    var className = nameof(PiecePositionAnalyzer);
                    var methodName = nameof(Parse);
                    var variableName = nameof(pieceString);
                    
                    Debug.LogError(
                        $"In method {className}.{methodName}() token '{variableName}' = {pieceString}. It's incorrect value!");
                    return null;
            }
            
            var chessPieceToken = new ChessPieceToken(x, y, color, piece);
            chessPieceTokens.Add(chessPieceToken);
        }

        return chessPieceTokens;
    }
}

public class ChessPieceToken
{
    private int _x;
    private int _y;

    private GameObject _abstractPiece;

    public ChessPieceToken(int x, int y, Piece.Color pieceColor, GameObject abstractPiece)
    {
        _x = x;
        _y = y;
        
        _abstractPiece = abstractPiece;
        var colorData = abstractPiece.GetComponent<Piece>().ColorData;
        colorData.Color = pieceColor;
    }

    public override string ToString() => $"Token: [{_abstractPiece.name} on square ({_x};{_y})]";
}