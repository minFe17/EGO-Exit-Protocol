using UnityEngine;
using Utils;

public abstract class DoorBase : MonoBehaviour, IInteractable, ILoopObject
{
    [SerializeField] protected BoxCollider2D _collider;
    [SerializeField] DoorMemento _doorMemento;

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

    void TryUnlock()
    {
        // ���� ���ٴ� ��� ó��
        EItemType type = _doorMemento.NeedUnlockItem;
        _playerManager.ItemInventory.GetItem(out _item, type);
        if (_item != null)
            OnUnlock();
        else
            OnUnlockFail();
    }

    void OnUnlock()
    {
        // ���� ���� �� ��� ó��
        _currentLock = false;
        _item.Use();
        InteractDoor();
    }

    void OnUnlockFail()
    {
        // ���� ������ ���
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