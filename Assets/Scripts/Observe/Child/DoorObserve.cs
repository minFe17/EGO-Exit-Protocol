using System.Collections.Generic;
using UnityEngine;

public class DoorObserve : MonoBehaviour
{
    List<IDoorEvent> _doorEvent = new List<IDoorEvent>();

    public void AddDoorEvent(IDoorEvent doorEvent)
    {
        if (_doorEvent.Contains(doorEvent))
            return;
        _doorEvent.Add(doorEvent);
    }

    public void OnUnlockEvent()
    {
        for (int i = 0; i < _doorEvent.Count; i++)
            _doorEvent[i].OnUnlock();
    }

    public void OnUnlockFailEvent()
    {
        for (int i = 0; i < _doorEvent.Count; i++)
            _doorEvent[i].OnUnlockFail();
    }

    public void OnInteractEvent()
    { 

    }
}