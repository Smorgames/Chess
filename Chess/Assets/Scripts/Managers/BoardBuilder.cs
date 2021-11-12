using UnityEngine;

public class BoardBuilder
{
    public static Board BuildStandardChessboard()
    {
        var centerCoord = new Vector2(0, 0);
        var boardSize = new Vector2Int(8, 8);
        return BuildArbitraryChessBoard(centerCoord, boardSize);
    }

    public static Board BuildArbitraryChessBoard(Vector2 boardCenterCoordinate, Vector2Int boardSize)
    {
        var board = CreateSettledBoard(boardSize);
        
        var squarePref = ReferenceRegistry.Instance.MyPrefabsStorage.SquaresPrefabs[0];
        var squarePrefSize = squarePref.GetComponent<SpriteRenderer>().sprite.bounds.size;

        var centerX = -((boardSize.x - 1) * squarePrefSize.x) / 2;
        var centerY = -((boardSize.y - 1) * squarePrefSize.y) / 2;
        
        var startPoint = new Vector2(centerX, centerY);

        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                var gameCoord = new Vector2(startPoint.x + x * squarePrefSize.x, startPoint.y + y * squarePrefSize.y) + boardCenterCoordinate;
                var boardMatrixCoord = new Vector2Int(x, y);
                var squareNum = x * boardSize.x + y;

                var square = CreateSettledSquare(gameCoord, boardMatrixCoord, squareNum, board);
                
                square.transform.parent = board.transform;
                board.Squares[x, y] = square;
            }
        }

        return board;
    }

    private static Board CreateSettledBoard(Vector2Int boardSize)
    {
        var board = new GameObject("Board");
        var chessboardComponent = board.AddComponent<Board>();
        chessboardComponent.Initialize(boardSize);
        board.transform.position = Vector3.zero;
        return board.GetComponent<Board>();
    }

    private static Square CreateSettledSquare(Vector2 gameCoordinates, Vector2Int boardMatrixCoordinates, int squareNumber, Board board)
    {
        var squaresPrefabs = ReferenceRegistry.Instance.MyPrefabsStorage.SquaresPrefabs;
        
        var index = (boardMatrixCoordinates.x + boardMatrixCoordinates.y) % squaresPrefabs.Count;
        var square = Object.Instantiate(squaresPrefabs[index], Vector3.zero, Quaternion.identity).GetComponent<Square>();
        var squareName = $"Square[{boardMatrixCoordinates.x},{boardMatrixCoordinates.y}]";

        square.transform.position = gameCoordinates;
        square.Initialize(boardMatrixCoordinates, board, squareName);

        return square;
    }
}