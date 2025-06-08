using UnityEngine;
using Utils;

public class PlayerManager : MonoBehaviour, ILoopObject
{
    // ╫л╠шео
    Player _player;
    MementoManager _mementoManager;

    public void Init(Player player)
    {
        _mementoManager = GenericSingleton<MementoManager>.Instance;
        GenericSingleton<ObserveManager>.Instance.LoopObserve.AddLoopEvent(this);
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        _player.transform.position = pos;
    }

    #region Interface
    void ILoopObject.OnLoopEvent()
    {
        if(_mementoManager == null)
            _player.transform.position = _mementoManager.PlayerMemento.PlayerStartPos;
    }
    #endregion
}