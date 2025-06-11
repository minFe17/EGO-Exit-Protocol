using Utils;

public class ItemBase : ILoopObject
{
    ItemInventory _inventory;
    MementoManager _mementoManager;

    protected EItemType _itemType;

    protected virtual void Init()
    {
        PlayerManager playerManager = GenericSingleton<PlayerManager>.Instance;
        _inventory = playerManager.ItemInventory;
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
    }

    public virtual void Use()
    {
        Remove();
    }

    public void Set()
    {
        if (_inventory == null)
            Init();
        _inventory.SetItem(_itemType, this);
    }

    public void Remove()
    {
        _inventory.RemoveItem(_itemType);
    }

    void ILoopObject.OnLoopEvent()
    {
        Remove();
        _mementoManager.ItemMemento[_itemType].ItemHolder.SetItem(this);
    }
}