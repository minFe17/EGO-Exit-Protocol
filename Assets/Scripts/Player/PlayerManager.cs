using UnityEngine;
using Utils;

public class PlayerManager : MonoBehaviour, ILoopObject
{
    // ╫л╠шео
    Player _player;
    MementoManager _mementoManager;
    ItemInventory _itemInventory = new ItemInventory();

    public Player Player { get => _player; }
    public ItemInventory ItemInventory { get => _itemInventory; }

    public void Init(Player player)
    {
        _player = player;
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        _mementoManager.PlayerMemento.PlayerStartPos = _player.transform.position;
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        _player.transform.position = pos;
    }

    #region Interface
    void ILoopObject.OnLoopEvent()
    {
        SetPlayerPosition(_mementoManager.PlayerMemento.PlayerStartPos);
        _itemInventory.ClearItemDict();
    }
    #endregion
}