using UnityEngine;
using Utils;

public class GivePhoneDesk : Desk, IItemHolder
{
    [SerializeField] MemoryObject _memoryObject;

    MementoManager _mementoManager;
    ItemBase _phone = new Phone();

    protected override void Init()
    {
        base.Init();
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        _mementoManager.ItemMemento[EItemType.Phone].ItemHolder = this;
    }

    public override void InteractEvent()
    {
        if (_phone == null)
            return;
        _phone.Set();
        _memoryObject.AddMemory();
        _phone = null;
    }

    void IItemHolder.SetItem(ItemBase item)
    {
        _phone = item;
    }
}