using AnalysisOfChessState;
using UnityEngine;

public class SingletonRegistry : MonoBehaviour
{
    public static SingletonRegistry Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public PrefabsStorage PrefabsStorage => _prefabsStorage;
    [SerializeField] private PrefabsStorage _prefabsStorage;

    public Chessboard Board
    {
        get => _board;
        set
        {
            if (!_isBoardInit)
            {
                _board = value;
                _isBoardInit = true;
            }
        }
    }
    [SerializeField] private Chessboard _board;
    private bool _isBoardInit;

    public ChessboardBuilder Builder => _builder;
    [SerializeField] private ChessboardBuilder _builder;
}