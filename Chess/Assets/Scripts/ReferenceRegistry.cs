using UnityEngine;

public class ReferenceRegistry : MonoBehaviour
{
    public static ReferenceRegistry Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    public PrefabsStorage PrefabsStorage => _prefabsStorage;
    [SerializeField] private PrefabsStorage _prefabsStorage;
}