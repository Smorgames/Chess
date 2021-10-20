using UnityEngine;

public class ChessboardBuilder : MonoBehaviour
{
    public Chessboard BuildMainChessboard()
    {
        var centerCoord = new Vector2(0, 0);
        var boardSize = new Vector2Int(8, 8);
        return BuildArbitraryChessboard(centerCoord, boardSize);
    }

    public Chessboard BuildArbitraryChessboard(Vector2 boardCenterCoordinate, Vector2Int boardSize)
    {
        var board = CreateChessboardTemplate(boardSize);
        
        var squarePref = SingletonRegistry.Instance.PrefabsStorage.SquaresPrefabs[0];
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

                var square = CreateAndInitializeSquare(gameCoord, boardMatrixCoord, squareNum, board);
                
                square.transform.parent = board.transform;
                board.Squares[x, y] = square;
            }
        }

        return board;
    }

    private Chessboard CreateChessboardTemplate(Vector2Int boardSize)
    {
        var board = new GameObject("Chessboard");
        var chessboardComponent = board.AddComponent<Chessboard>();

        chessboardComponent.InitializeChessboard(boardSize);
        var ghostSquare = Instantiate(SingletonRegistry.Instance.PrefabsStorage.GhostSquare, Vector3.zero, Quaternion.identity);
        chessboardComponent.GhostSquare = ghostSquare;
        
        board.transform.position = Vector3.zero;
        return board.GetComponent<Chessboard>();
    }

    private Square CreateAndInitializeSquare(Vector2 gameCoordinates, Vector2Int boardMatrixCoordinates, int squareNumber, Chessboard board)
    {
        var squares = SingletonRegistry.Instance.PrefabsStorage.SquaresPrefabs;
        
        var index = (boardMatrixCoordinates.x + boardMatrixCoordinates.y) % squares.Count;
        var square = Instantiate(squares[index], transform.position, Quaternion.identity).GetComponent<Square>();
        square.transform.position = gameCoordinates;
        
        var squareName = $"Square {squareNumber}";
        square.name = squareName;

        square.Initialize(boardMatrixCoordinates, board);

        return square;
    }
}