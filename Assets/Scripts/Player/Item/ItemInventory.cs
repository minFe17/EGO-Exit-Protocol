using System.Collections.Generic;

public class ItemInventory
{
    Dictionary<EItemType, ItemBase> _itemDict = new Dictionary<EItemType, ItemBase>();
    List<EItemType> _keys = new List<EItemType>();

    bool CheckItem()
    {
        if (_itemDict.Count >= 2)
            return false;
        return true;
    }

    public void SetItem(EItemType itemType, ItemBase itemBase)
    {
        if (!CheckItem())
            return;
        else
        {
            _itemDict.Add(itemType, itemBase);
            _keys.Add(itemType);
        }
    }

    public void GetItem(out ItemBase item, EItemType type)
    {
        _itemDict.TryGetValue(type, out item);
    }

    public void RemoveItem(EItemType type)
    {
        if (_itemDict.ContainsKey(type))
        {
            _itemDict.Remove(type);
            _keys.Remove(type);
        }
    }

    public bool HavePhone()
    {
        if (_itemDict.ContainsKey(EItemType.Phone))
            return true;
        return false;
    }

    public void UsePhone(int number)
    {
        if (_keys[number] != EItemType.Phone)
            return;
        _itemDict[EItemType.Phone].Use();
    }

    public void ClearItemDict()
    {
        _itemDict.Clear();
        _keys.Clear();
    }
}