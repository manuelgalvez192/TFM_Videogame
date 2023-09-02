using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Scriptables/GameEvents")]
public class GameEvent : ScriptableObject
{
    List<EventListener> listeners = new List<EventListener>();

    public void Raise()
    {
        foreach(EventListener listener in listeners)
        {
            listener.OnEventRaised();
        }
    }
    public void RegisterListener(EventListener listener)
    {
        listeners.Add(listener);
    }
    public void UnregisterListener(EventListener listener)
    {
        listeners.Remove(listener);
    }
}
