using System.Collections.Generic;
using UnityEngine;

public class LoopObserve : MonoBehaviour
{
    List<ILoopObject> _loopEvents = new List<ILoopObject>();

    public void AddLoopEvent(ILoopObject loopEvent)
    {
        if (_loopEvents.Contains(loopEvent))
            return;
        _loopEvents.Add(loopEvent);
    }

    public void OnLoopEvent()
    {
        for (int i = 0; i < _loopEvents.Count; i++)
            _loopEvents[i].OnLoopEvent();
    }
}