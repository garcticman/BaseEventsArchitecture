using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public void ShowVector(BaseCustomData baseCustomData) {
        if (baseCustomData is PlayerMovesData data) {
            Debug.Log(data.testVector);
        }
    }
}
