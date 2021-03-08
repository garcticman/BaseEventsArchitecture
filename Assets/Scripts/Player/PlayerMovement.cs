using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameEvent playerMoves;

    private Vector3 _lastPlayerPos;

    private void Update() {
        if (transform.position != _lastPlayerPos) {
            _lastPlayerPos = transform.position;
            playerMoves.Raise(new PlayerMovesData(_lastPlayerPos));
        }
    }
}
