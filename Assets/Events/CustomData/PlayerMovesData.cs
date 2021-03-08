using System;
using UnityEngine;
using UnityEditor;

[Serializable]
public class PlayerMovesData : BaseCustomData
{
    public Vector3 testVector;
    public PlayerMovesData(Vector3 vector) {
        testVector = vector;
    }
}