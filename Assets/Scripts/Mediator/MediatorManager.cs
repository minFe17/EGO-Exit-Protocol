using System.Collections.Generic;
using UnityEngine;

public class MediatorManager : MonoBehaviour
{
    // ╫л╠шео
    Dictionary<EMediatorEventType, List<IMediatorEvent>> _eventDict = new Dictionary<EMediatorEventType, List<IMediatorEvent>>();

    public void Register(EMediatorEventType key, IMediatorEvent value)
    {
        List<IMediatorEvent> list;
        if (!_eventDict.TryGetValue(key, out list))
        {
            list = new List<IMediatorEvent> { value };
            _eventDict[key] = list;
        }
        else if (!list.Contains(value))
            list.Add(value);
    }

    public void Unregister(EMediatorEventType key, IMediatorEvent value)
    {
        if(_eventDict.TryGetValue(key, out List<IMediatorEvent> list))
        {
            list.Remove(value);
            if(list.Count == 0)
                _eventDict.Remove(key);
        }
    }

    public void Notify(EMediatorEventType key, object data = null)
    {
        if(_eventDict.TryGetValue(key, out List<IMediatorEvent> list))
        {
            for(int i=0; i<list.Count; i++)
            {
                list[i].HandleEvent(data);
            }
        }
    }
}