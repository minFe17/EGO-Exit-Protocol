using UnityEngine;
using UnityEngine.U2D;
using Utils;

public class ItemBase : ILoopObject
{
    ItemInventory _inventory;
    MementoManager _mementoManager;
    SpriteAtlas _itemIconAtlas;
    Sprite _sprite;


    protected EItemType _itemType;

    protected virtual void Init()
    {
        PlayerManager playerManager = GenericSingleton<PlayerManager>.Instance;
        _inventory = playerManager.ItemInventory;
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
        _itemIconAtlas = GenericSingleton<PrefabManager>.Instance.GetPrefabLoad(EPrefabType.ItemIcon).GetPrefab<SpriteAtlas>();
        _sprite = _itemIconAtlas.GetSprite($"{_itemType}");
    }

    public virtual void Use()
    {
        Remove();
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.UseItem, _sprite);
    }

    public void Set()
    {
        if (_inventory == null)
            Init();
        _inventory.SetItem(_itemType, this);
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.GetItem, _sprite);
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