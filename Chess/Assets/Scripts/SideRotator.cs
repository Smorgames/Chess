using System.Collections.Generic;
using UnityEngine;

public class SideRotator : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    private bool _initializedChangeTurn = true;

    private void RotateSide(object s, TurnOrderEventArgs a)
    {
        if (_initializedChangeTurn)
        {
            _initializedChangeTurn = false;
            return;
        }

        _camera.localRotation = _camera.localRotation * Quaternion.Euler(new Vector3(0f, 0f, 180f));

        var allPieces = new List<Piece>();
        var players = Game.Instance.Players;

        foreach (var player in players)
            allPieces.AddRange(player.MyPieces);

        foreach (var piece in allPieces)
            piece.FlipPiece();
    }

    private void Start() => Game.OnTurnOrderChanged += RotateSide;
    private void OnDestroy() => Game.OnTurnOrderChanged -= RotateSide;
}