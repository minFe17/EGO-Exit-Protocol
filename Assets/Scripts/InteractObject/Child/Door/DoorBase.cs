using System;
using NaughtyAttributes;
using UnityEngine;
using Utils;

public abstract class DoorBase : MonoBehaviour, IInteractable, ILoopObject
{
    [SerializeField] protected BoxCollider2D _collider;
    [SerializeField] DoorMemento _doorMemento;
    [SerializeField] MemoryObject _memory;
    [SerializeField] bool _isTrap;

    [ShowIf("_isTrap")]
    [SerializeField] Vector3 _researcherSpawnPos;

    protected CameraManager _cameraManager;
    protected PlayerManager _playerManager;

    ObserveManager _observerManager;
    InteractObjectManager _interactObjectManager;
    ItemBase _item;
    bool _currentLock;

    public abstract void OnInteract();

    void Start()
    {
        Init();
        OnLoopEvent();
    }

    protected virtual void Init()
    {
        _observerManager = GenericSingleton<ObserveManager>.Instance;
        _observerManager.LoopObserve.AddLoopEvent(this);
        _cameraManager = GenericSingleton<CameraManager>.Instance;
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _interactObjectManager.SetInteractable(gameObject, this);
        _playerManager = GenericSingleton<PlayerManager>.Instance;
    }

    #region NaughtyAttributes
    protected bool IsNotTrapDoor() => !_isTrap;
    #endregion

    void TryUnlock()
    {
        // 문이 잠겼다는 대사 처리
        EItemType type = _doorMemento.NeedUnlockItem;
        if (type != EItemType.Max)
            _playerManager.ItemInventory.GetItem(out _item, type);

        if (_isTrap)
            TrapDoor();

        if (_item != null)
            OnUnlock();
        else
            OnUnlockFail();
    }

    void OnUnlock()
    {
        // 열쇠 있을 때 대사 처리
        _currentLock = false;
        _item.Use();
        InteractDoor();
    }

    void OnUnlockFail()
    {
        // 열쇠 없을때 대사
    }

    void TrapDoor()
    {
        GenericSingleton<MediatorManager>.Instance.Notify(EMediatorEventType.SpawnResearcher, _researcherSpawnPos);
        if (_memory != null)
            _memory.AddMemory();
    }

    protected virtual void InteractDoor()
    {
        OnInteract();
    }

    #region Interface
    void IInteractable.Interact()
    {
        if (_currentLock)
            TryUnlock();
        else
            InteractDoor();
    }

    GameObject IInteractable.GetGameObject()
    {
        return gameObject;
    }

    public virtual void OnLoopEvent()
    {
        _currentLock = _doorMemento.IsLock;
        _collider.isTrigger = _doorMemento.IsTrigger;
    }
    #endregion
}