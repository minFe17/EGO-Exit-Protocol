using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    Dictionary<EItemType, ItemBase> _itemDict = new Dictionary<EItemType, ItemBase>();

    bool CheckItem()
    {
        if (_itemDict.Count >= 2)
            return false;
        return true;
    }

    void ChangeItem(EItemType itemType, ItemBase itemBse)
    {
        // UI에서 3개 보여주기?
        // 한개는 버려야 함
    }

    public void SetItem(EItemType itemType, ItemBase itemBase)
    {
        if (!CheckItem())
            ChangeItem(itemType, itemBase);
        else
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

    public void ClearItemDict()
    {
        _itemDict.Clear();
    }
}