using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CastomDataEvent : UnityEvent<BaseCustomData>
{
}

public class GameEventListener: MonoBehaviour
{
    public GameEvent Event;
    public CastomDataEvent Response;

    private void Awake() {
        if (Response == null) {
            Response = new CastomDataEvent();
        }
    }

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);    
    }

    public void OnEventRaised(BaseCustomData customData) {
        Response.Invoke(customData);
    }
}
