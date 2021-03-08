using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu]
[System.Serializable]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise(BaseCustomData customData) {
        for (int i = 0; i < listeners.Count ; i++) {
            listeners[i].OnEventRaised(customData);
        }
    }

    public void RegisterListener(GameEventListener listener) {
        listeners.Add(listener);
    }
    public void UnregisterListener(GameEventListener listener) {
        listeners.Remove(listener);
    }
}
