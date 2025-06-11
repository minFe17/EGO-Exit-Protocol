using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour, ILoopObject
{
    Dictionary<EItemType, ItemBase> _itemDict = new Dictionary<EItemType, ItemBase>();

    public void SetItem(EItemType itemType, ItemBase itemBase)
    {
        _itemDict.Add(itemType, itemBase);
    }

    public void GetItem(out ItemBase item, EItemType type)
    {
        _itemDict.TryGetValue(type, out item);
    }

    public void RemoveItem(EItemType type)
    {
        if (_itemDict.ContainsKey(type))
            _itemDict.Remove(type);

    }

    void ILoopObject.OnLoopEvent()
    {
        _itemDict.Clear();
    }
}