using UnityEngine;
using Utils;

public class GiveKeyBundleCabinet : Cabinet, IItemHolder
{
    MementoManager _mementoManager;
    ItemBase _keyBundle = new KeyBundle();

    protected override void Init()
    {
        base.Init();
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        _mementoManager.ItemMemento[EItemType.KeyBundle].ItemHolder = this;
    }

    public override void Interact()
    {
        if (_keyBundle == null)
            return;
        _keyBundle.Set();
        _keyBundle = null;
    }

    void IItemHolder.SetItem(ItemBase item)
    {
        _keyBundle = item;
    }
}