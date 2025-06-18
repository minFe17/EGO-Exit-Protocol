using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PlayerManager : MonoBehaviour, ILoopObject
{
    // ╫л╠шео
    Player _player;
    MementoManager _mementoManager;
    PrefabManager _prefabManager;
    ItemInventory _itemInventory = new ItemInventory();

    public Player Player { get => _player; }
    public ItemInventory ItemInventory { get => _itemInventory; }

    public void Init()
    {
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        _prefabManager = GenericSingleton<PrefabManager>.Instance;
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
        SpawnPlayer();
        OnLoopEvent();
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        _player.transform.position = pos;
    }

    public void SpawnPlayer()
    {
        GameObject temp = Instantiate(_prefabManager.PlayerPrefabLoad.PlayerPrefab);
        _player = temp.GetComponent<Player>();
    }

    #region Interface
    public void OnLoopEvent()
    {
        SetPlayerPosition(_mementoManager.PlayerMemento.PlayerStartPos);
        _itemInventory.ClearItemDict();
    }
    #endregion
}