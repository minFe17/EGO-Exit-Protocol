using NaughtyAttributes;
using UnityEngine;
using Utils;

public abstract class DoorBase : MonoBehaviour, IInteractable, ILoopObject
{
    [SerializeField] DoorMemento _doorMemento;
    [SerializeField] MemoryObject _memory;
    [SerializeField] bool _isMainGate;

    [ShowIf("_isMainGate")]
    [SerializeField] Vector3 _researcherSpawnPos;

    protected BoxCollider2D _collider;
    protected CameraManager _cameraManager;
    protected PlayerManager _playerManager;
    protected MediatorManager _mediatorManager;

    ObserveManager _observerManager;
    InteractObjectManager _interactObjectManager;
    ItemBase _item;
    bool _currentLock;

    public abstract void OnInteract();

    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        Init();
        OnLoopEvent();
    }

    protected virtual void Init()
    {
        _mediatorManager = GenericSingleton<MediatorManager>.Instance;
        _observerManager = GenericSingleton<ObserveManager>.Instance;
        _observerManager.LoopObserve.AddLoopEvent(this);
        _cameraManager = GenericSingleton<CameraManager>.Instance;
        _interactObjectManager = GenericSingleton<InteractObjectManager>.Instance;
        _interactObjectManager.SetInteractable(gameObject, this);
        _playerManager = GenericSingleton<PlayerManager>.Instance;
    }

    #region NaughtyAttributes
    protected bool IsNotTrapDoor() => !_isMainGate;
    #endregion

    void TryUnlock()
    {
        _mediatorManager.Notify(EMediatorEventType.Dialog, EDialogType.Door);
        EItemType type = _doorMemento.NeedUnlockItem;
        if (type != EItemType.Max)
            _playerManager.ItemInventory.GetItem(out _item, type);

        if (_isMainGate)
            TrapDoor();

        if (_item != null)
            OnUnlock();
        else
            OnUnlockFail();
    }

    void OnUnlock()
    {
        switch(_doorMemento.NeedUnlockItem)
        {
            case EItemType.Key:
                _mediatorManager.Notify(EMediatorEventType.Dialog, EDialogType.DoorHasKey);
                break;
            case EItemType.BoltCutter:
                _mediatorManager.Notify(EMediatorEventType.Dialog, EDialogType.DoorHasBoltCutter);

                break;
        }

        _currentLock = false;
        _item.Use();
        InteractDoor();
    }

    void OnUnlockFail()
    {
        _mediatorManager.Notify(EMediatorEventType.Dialog, EDialogType.DoorNoItem);
    }

    void TrapDoor()
    {
        _mediatorManager.Notify(EMediatorEventType.Dialog, EDialogType.DoorMainGate);
        _mediatorManager.Notify(EMediatorEventType.SpawnResearcher, _researcherSpawnPos);
    }

    protected virtual void InteractDoor()
    {
        OnInteract();
    }

    #region Interface
    void IInteractable.Interact()
    {
        if (_memory != null)
            _memory.AddMemory();

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