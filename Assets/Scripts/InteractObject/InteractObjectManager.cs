using System.Collections.Generic;
using UnityEngine;

public class InteractObjectManager : MonoBehaviour
{
    // ╫л╠шео

    Dictionary<GameObject, IInteractable> _objectDict = new Dictionary<GameObject, IInteractable>();

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
}