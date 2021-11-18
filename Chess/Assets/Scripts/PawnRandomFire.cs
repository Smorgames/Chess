using System.Collections.Generic;
using UnityEngine;

public class PawnRandomFire : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pawnFirePrefabs;

    private Vector3 _offset = new Vector3(0f, 0.025f, 0f);

    private void Start()
    {
        SetRandomPawnFire();
    }

    private void SetRandomPawnFire()
    {
        var i = Random.Range(0, _pawnFirePrefabs.Count);
        var fire = Instantiate(_pawnFirePrefabs[i], Vector3.zero, Quaternion.identity);
        fire.transform.parent = gameObject.transform;
        fire.transform.localPosition = Vector3.zero + _offset;
    }
}