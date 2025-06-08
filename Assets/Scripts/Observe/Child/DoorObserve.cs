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

    public void OnOpenEvent()
    {
        for (int i = 0; i < _doorEvent.Count; i++)
            _doorEvent[i].OnOpen();
    }

    public void OnOpenFailEvent()
    {
        for (int i = 0; i < _doorEvent.Count; i++)
            _doorEvent[i].OnOpenFail();
    }

    public void OnInteractEvent()
    { 

    }
}