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
}