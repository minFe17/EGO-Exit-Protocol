using Utils;

public class GiveBoltCutterCabinet : Cabinet, IItemHolder, IMediatorEvent
{
    MementoManager _mementoManager;
    ItemBase _boltCutter = new BoltCutter();

    protected override void Init()
    {
        base.Init();
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        _mementoManager.ItemMemento[EItemType.BoltCutter].ItemHolder = this;
        GenericSingleton<MediatorManager>.Instance.Register(EMediatorEventType.CompleteDial, this);
    }

    public override void Interact()
    {
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.UseDial);
    }

    void IItemHolder.SetItem(ItemBase item)
    {
        _boltCutter = item;
    }

    void IMediatorEvent.HandleEvent(object data)
    {
        if (_boltCutter == null)
            return;
        _boltCutter.Set();
        _boltCutter = null;
    }
}