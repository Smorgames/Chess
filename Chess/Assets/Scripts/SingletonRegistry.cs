using UnityEngine;

public class SingletonRegistry : MonoBehaviour
{
    public static SingletonRegistry Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public RealPiecePrefabsStorage RealPiecePrefabsStorage => _realPiecePrefabsStorage;
    [SerializeField] private RealPiecePrefabsStorage _realPiecePrefabsStorage;

    public Chessboard Board => _board;
    [SerializeField] private Chessboard _board;
}