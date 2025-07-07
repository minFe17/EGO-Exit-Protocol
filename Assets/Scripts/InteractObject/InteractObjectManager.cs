using System.Collections.Generic;
using UnityEngine;
using Utils;

public class InteractObjectManager : MonoBehaviour, IMediatorEvent
{
    // ╫л╠шео
    Dictionary<GameObject, IInteractable> _objectDict = new Dictionary<GameObject, IInteractable>();

    public void Init()
    {
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.Ending, this);
    }

    public void SetInteractable(GameObject key, IInteractable value)
    {
        if (_objectDict.ContainsKey(key))
            return;
        _objectDict.Add(key, value);
    }

    public void GetInteractable(out IInteractable value, GameObject key)
    {
        _objectDict.TryGetValue(key, out value);
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        _objectDict.Clear();
    }
}