using UnityEngine;
using Utils;

public class GiveKeyDeadBody : DeadBody, IItemHolder
{
    [SerializeField] MemoryObject _memoryObject;

    MementoManager _mementoManager;
    ItemBase _key = new Key();

    protected override void Init()
    {
        base.Init();
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        _mementoManager.ItemMemento[EItemType.Key].ItemHolder = this;
    }

    public override void Interact()
    {
        if (_key == null)
            return;
        _key.Set();
        _memoryObject.AddMemory();
        _key = null;
    }

    void IItemHolder.SetItem(ItemBase item)
    {
        _key = item;
    }
}