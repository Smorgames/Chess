using UnityEngine;

public class Chessboard : MonoBehaviour
{
    public static Chessboard Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public int Length => _length;
    private int _length;
    public int Height => _height;
    private int _height;

    public Square[,] Squares => _squares;
    private Square[,] _squares;

    public void InitializeChessboard(int length, int height)
    {
        _length = length;
        _height = height;

        _squares = new Square[_length, _height];
    }
}